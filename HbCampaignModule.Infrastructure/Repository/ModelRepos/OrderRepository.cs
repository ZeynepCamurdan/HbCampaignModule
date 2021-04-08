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
    public class OrderRepository : IOrderRepository
    {
        private readonly PostgreSqlDbContext _context;
        private readonly IMapper _mapper;
        IBaseRepository<Order> _orderRepository;
        IBaseRepository<Campaign> _campaignRepository;
        IBaseRepository<Product> _productRepository;

        public OrderRepository(PostgreSqlDbContext context, IMapper mapper, IBaseRepository<Order> orderRepository, IBaseRepository<Campaign> campaignRepository, IBaseRepository<Product> productRepository)
        {
            _context = context;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _campaignRepository = campaignRepository;
            _productRepository = productRepository;
        }
        public IDataResult<OrderDto> CreateOrder(OrderDto order)
        {
            var campaign = _campaignRepository
                   .AllIncludingAsQueryable(o => o.Orders, o => o.Product).FirstOrDefault(c => c.Product.ProductCode == order.ProductCode);
            Product product = GetProductData(order.ProductCode);
            order.ProductId = product.Id;
            product.Stock -= order.Quantity;

            Order data = _mapper.Map<Order>(order);
            data.Campaign.Id = campaign.Id;
            _orderRepository.Add(data);
            _orderRepository.Commit();

            Product p = _mapper.Map<Product>(product);
            _productRepository.UpdateWithCommit(p);
            _productRepository.Commit();

            //totalsales olayından campaign de kaydedilecek

            return new SuccessDataResult<OrderDto>(_mapper.Map<OrderDto>(data), string.Format(OrderConstants.SUCCESS_CREATE, order.ProductCode, order.Quantity), HttpResponse.Ok);
 
        }

        public IResult CheckStock(OrderDto order)
        {
            Product product = GetProductData(order.ProductCode);
            if (product == null)
            {
                return new ErrorResult(OrderConstants.CHECK_STOCK, HttpResponse.NotFound);
            }
            if (order.Quantity > product.Stock)
            {
                return new ErrorResult(OrderConstants.CHECK_STOCK_NOT_ENOUGH, HttpResponse.Conflict);
              
            }
            return new SuccessResult();
        }

        public Product GetProductData(string productCode)
        {
            return _productRepository.GetSingle(p => p.ProductCode == productCode);
        }

        public IResult CheckProduct(string productCode)
        {
            Product result = GetProductData(productCode);
            if (result == null)
            {
                return new ErrorResult(OrderConstants.CHECK_PRODUCT, HttpResponse.NotFound);
            }
            return new SuccessResult();
        }

    }
}
