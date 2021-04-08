using AutoMapper;
using AutoMapper.Configuration;
using HbCampaignModule.Domain.Interfaces.ExceptionIF;
using HbCampaignModule.Domain.Interfaces.ModelIF;
using HbCampaignModule.Domain.Model;
using HbCampaignModule.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HbCampaignModule.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly PostgreSqlDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IHandleException _handleException;

        public CampaignController(PostgreSqlDbContext context,
           IProductRepository productRepository,
           IMapper mapper,
           IOrderRepository orderRepository,
           //ILogger<CampaignService> logger,
           IConfiguration configuration,
           ICampaignRepository campaignRepository)
        {
            this._context = context;
            this._mapper = mapper;
            //this._logger = logger;
            this._configuration = configuration;
            this._campaignRepository = campaignRepository;
            this._productRepository = productRepository;
            this._orderRepository = orderRepository;
        }

        //[HttpGet("GetProductInfo")]
        //public Product GetProductInfo(ProductDto product)
        //{
        //    var serviceResult = _campaignAlgorithmService.GetProduct(model);

        //    if (serviceResult.ResultType == ServiceResultType.Fail)
        //        return BadRequest(serviceResult.Message);

        //    return Ok(serviceResult.Data);

        //    var productItem = _productRepository.GetSingle(p => p.ProductCode == product.ProductCode);
        //    return productItem;
        //}
        [HttpGet("test")]
        [Route("test")]
        public string test()
        {
            return string.Empty;
        }
        [HttpPost("Zeyzeynepnep")]
        public Response<ProductDto> CreateProduct(ProductDto product)
        {
            return _productRepository.CreateProduct(product);
        }

        //[HttpGet("GetProduct")]
        //public Product CreateOrder(int productCode)
        //{
        //    var productItem = _productRepository.GetSingle(p => p.ProductCode == productCode);
        //    return productItem;
        //}
    }
}
