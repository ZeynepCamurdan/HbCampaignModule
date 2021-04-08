using AutoMapper;
using HbCampaignModule.Domain.Constants;
using HbCampaignModule.Domain.Entities;
using HbCampaignModule.Domain.Interfaces;
using HbCampaignModule.Domain.Interfaces.ModelIF;
using HbCampaignModule.Domain.Model;
using HbCampaignModule.Domain.Results;
using HbCampaignModule.Domain.ResultsIF;
using HbCampaignModule.Infrastructure.Context;
using System.Linq;

namespace HbCampaignModule.Infrastructure.Repository.ModelRepos
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly PostgreSqlDbContext _context;
        private readonly IMapper _mapper;
        IBaseRepository<Campaign> _campaignRepository;
        IBaseRepository<Product> _productRepository;

        public CampaignRepository(PostgreSqlDbContext context,IMapper mapper, IBaseRepository<Campaign> campaignRepository, IBaseRepository<Product> productRepository)
        {
            this._context = context;
            this._mapper = mapper;
            this._campaignRepository = campaignRepository;
            this._productRepository = productRepository;
        }
        public IDataResult<CampaignDto> CreateCampaign(CampaignDto campaign)
        {
            Product product = GetProductData(campaign.ProductCode);
            campaign.ProductId = product.Id;
            Campaign campaignData = _mapper.Map<Campaign>(campaign);
            _campaignRepository.Add(campaignData);
            _campaignRepository.Commit();
            return new SuccessDataResult<CampaignDto>(_mapper.Map<CampaignDto>(campaignData), string.Format(CampaignConstants.CREATE_ORDER, campaign.Name, campaign.ProductCode, campaign.Duration, campaign.PriceManipulationLimit, campaign.TargetSalesCount), HttpResponse.Ok);
        }

        public IDataResult<CampaignDto> GetCampaignInfo(string campaignName)
        {
            Campaign campaignData = CheckAllCampaigns(campaignName);
            Campaign data = _mapper.Map<Campaign>(campaignData);
            string status = campaignData.IsActive ? CampaignConstants.ACTIVE : CampaignConstants.ENDED;
            int turnover = campaignData.TotalSales * campaignData.AverageItemPrice;

              return new SuccessDataResult<CampaignDto>(_mapper.Map<CampaignDto>(campaignData), string.Format(CampaignConstants.GET_CAMPAING_MESSAGE, campaignName, status, campaignData.TargetSalesCount, campaignData.TotalSales, turnover, campaignData.AverageItemPrice), HttpResponse.Ok); ;
        }

        public IResult IncreaseTime(int hour)
        {
            Response<int> response = new Response<int>();
            var campaignList = _campaignRepository
                .AllIncludingAsQueryable(o => o.Product, o => o.Orders).Where(x=>x.IsActive).ToList();

            foreach (var campaignItem in campaignList)
            {
                var increaseHour = campaignItem.Duration - hour;
                if (increaseHour > 0) 
                { 
                    campaignItem.Product.Price -= campaignItem.Product.Price * campaignItem.PriceManipulationLimit / 100;
                    campaignItem.Duration = increaseHour;
                    campaignItem.TotalSales = campaignItem.Orders.Sum(co => co.Quantity);
                    campaignItem.Turnover = (int)campaignItem.Orders.Sum(co => (co.Quantity * co.Quantity));

                    if (campaignItem.TotalSales != 0)
                        campaignItem.AverageItemPrice = campaignItem.Turnover / campaignItem.TotalSales;
                }
                else
                {
                    campaignItem.Duration = 0;
                    campaignItem.IsActive = false;
                }
                _campaignRepository.UpdateWithCommit(campaignItem);
                _campaignRepository.Commit();

                _productRepository.UpdateWithCommit(campaignItem.Product);
                _campaignRepository.Commit();

            }
            return new SuccessResult(string.Format(CampaignConstants.INCREASE_TIME, hour), HttpResponse.Ok);          
        }

        public IResult ProductExists(string productCode)
        {
            Product result = GetProductData(productCode);
            if (result == null)
            {
                return new ErrorResult(CampaignConstants.PRODUCT_EXIST_MESSAGE, HttpResponse.NotFound);
            }
            return new SuccessResult();
        }

        public IResult CampaignExists(CampaignDto campaign)
        {
            Campaign result = GetCampaignData(campaign.Name);
            if (result != null)
            {
                return new ErrorResult(CampaignConstants.CAMPAING_EXISTS, HttpResponse.Conflict);
            }
            return new SuccessResult();
        }

        public Product GetProductData(string productCode)
        {
            return _productRepository.GetSingle(p => p.ProductCode == productCode);
        }

        public Campaign GetCampaignData(string name)
        {
            return _campaignRepository.GetSingle(c => c.Name == name);
        }

        public Campaign CheckAllCampaigns(string campaignName)
        {
            return _campaignRepository.AllIncludingAsQueryable(c => c.Product).FirstOrDefault(c => c.Name == campaignName);
        }

        public IResult CheckCampaign(string campaignName)
        {
            Campaign campaignData = CheckAllCampaigns(campaignName);
            if (campaignData == null)
            {
                return new ErrorResult(CampaignConstants.CHECHK_CAMPAIGN, HttpResponse.NotFound);
            }
            var product = ProductExists(campaignData.ProductCode);
            if (product == null)
            {
                return new ErrorResult(CampaignConstants.CHECHK_CAMPAIGN_PRODUCT, HttpResponse.NotFound);
            }
            return new SuccessResult();
        }
    }
}
