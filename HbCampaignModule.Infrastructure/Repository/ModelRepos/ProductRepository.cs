using AutoMapper;
using HbCampaignModule.Domain.Constants;
using HbCampaignModule.Domain.Entities;
using HbCampaignModule.Domain.Interfaces;
using HbCampaignModule.Domain.Interfaces.ModelIF;
using HbCampaignModule.Domain.Model;
using HbCampaignModule.Domain.Results;
using HbCampaignModule.Domain.ResultsIF;
using HbCampaignModule.Infrastructure.Context;


namespace HbCampaignModule.Infrastructure.Repository.ModelRepos
{
    public class ProductRepository : IProductRepository
    {
        private readonly PostgreSqlDbContext _context;
        private readonly IMapper _mapper;
        IBaseRepository<Product> _productRepository;

        public ProductRepository() { }
        public ProductRepository(PostgreSqlDbContext context, IMapper mapper, IBaseRepository<Product> productRepository)
        {
            _context = context;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public IDataResult<ProductDto> CreateProduct(ProductDto product)
        {
          
            Product data = _mapper.Map<Product>(product);
            _productRepository.Add(data);
            _productRepository.Commit();
            return new SuccessDataResult<ProductDto>(_mapper.Map<ProductDto>(data), string.Format(ProductConstants.CREATE_PRODUCT, product.ProductCode, product.Price, product.Stock), HttpResponse.Ok);
        }

        public IDataResult<ProductDto> GetProductInfo(string productCode)
        {
            Product result = GetProductData(productCode);
            Product data = _mapper.Map<Product>(result);
            return new SuccessDataResult<ProductDto>(_mapper.Map<ProductDto>(data), string.Format(ProductConstants.GET_PRODUCT_INFO, result.Price, result.Stock), HttpResponse.Ok);
        }
        public IResult ProductExists(string productCode)
        {
            Product result =  GetProductData(productCode);

            if (result != null)
            {
                return new ErrorResult(ProductConstants.PRODUCT_EXISTS, HttpResponse.Conflict);
            }
            return new SuccessResult();
        }

        public IResult CheckProduct(string productCode)
        {
            Product result = GetProductData(productCode);
            if (result == null)
            {
                return new ErrorResult(ProductConstants.CHECK_PRODUCT, HttpResponse.NotFound);
            }
            return new SuccessResult();
        }

        public Product GetProductData(string productCode)
        {
            return _productRepository.GetSingle(p => p.ProductCode == productCode);
        }
    }
}
