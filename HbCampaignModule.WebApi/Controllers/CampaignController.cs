using HbCampaignModule.Domain.Constants;
using HbCampaignModule.Domain.Interfaces.ModelIF;
using HbCampaignModule.Domain.Model;
using HbCampaignModule.Domain.ResultsIF;
using HbCampaignModule.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HbCampaignModule.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : Controller
    {
        private readonly PostgreSqlDbContext _context;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CampaignController> _logger;
        public CampaignController(PostgreSqlDbContext context,
           IProductRepository productRepository,
           IOrderRepository orderRepository,
           ICampaignRepository campaignRepository,
           ILogger<CampaignController> logger)
        {
            this._context = context;
            this._campaignRepository = campaignRepository;
            this._productRepository = productRepository;
            this._orderRepository = orderRepository;
            this._logger = logger;
        }

        [HttpPost("CreateProduct")]
        public ActionResult CreateProduct(ProductDto product)
        {
            IResult productExists = _productRepository.ProductExists(product.ProductCode);
            if (!productExists.Success)
            {
                _logger.LogError(LogConstants.FAIL_CREATE_PRODUCT, productExists.Message);
                return BadRequest(productExists);
            }
            IDataResult<ProductDto> productCreated = _productRepository.CreateProduct(product);
            if (productCreated.Success) 
            {
                _logger.LogInformation(LogConstants.SUCCESS_CREATE_PRODUCT);
                return Ok(productCreated);
            }
            _logger.LogError(LogConstants.ERROR_CREATE_PRODUCT);
            return BadRequest(productCreated);
        }

        [HttpGet("GetProductInfo")]
        public ActionResult GetProductInfo(string productCode)
        {
            IResult productExists = _productRepository.CheckProduct(productCode);
            if (!productExists.Success)
            {
                _logger.LogError(LogConstants.FAIL_GET_PRODUCT, productExists.Message);
                return BadRequest(productExists);
            }
            IDataResult<ProductDto> productInfo = _productRepository.GetProductInfo(productCode);
            if (productInfo.Success)
            {
                _logger.LogInformation(LogConstants.SUCCESS_GET_PRODUCT);
                return Ok(productInfo);
            }
            _logger.LogError(LogConstants.ERROR_GET_PRODUCT);
            return BadRequest(productInfo);
        }

        [HttpPost("CreateOrder")]
        public ActionResult CreateOrder(OrderDto order)
        {
            IResult productExists = _orderRepository.CheckProduct(order.ProductCode);
            if (!productExists.Success)
            {
                _logger.LogError(LogConstants.FAIL_CREATE_ORDER, productExists.Message);
                return BadRequest(productExists);
            }
            IResult stockExists = _orderRepository.CheckStock(order);
            if (!stockExists.Success)
            {
                _logger.LogError(LogConstants.FAIL_CREATE_ORDER, stockExists.Message);
                return BadRequest(stockExists);
            }
            IDataResult<OrderDto> orderData = _orderRepository.CreateOrder(order);
            if (orderData.Success)
            {
                _logger.LogInformation(LogConstants.SUCCESS_CREATE_ORDER);
                return Ok(orderData);
            }
            _logger.LogError(LogConstants.ERROR_CREATE_ORDER);
            return BadRequest(orderData);
        }

        [HttpPost("CreateCampaign")]
        public IActionResult CreateCampaign(CampaignDto campaign)
        {
            IResult campaignExists = _campaignRepository.CampaignExists(campaign);
            if (!campaignExists.Success)
            {
                _logger.LogError(LogConstants.FAIL_CREATE_CAMPAIGN, campaignExists.Message);
                return BadRequest(campaignExists);
            }
            IResult productExists = _campaignRepository.ProductExists(campaign.ProductCode);
            if (!productExists.Success)
            {
                _logger.LogError(LogConstants.FAIL_CREATE_CAMPAIGN, productExists.Message);
                return BadRequest(productExists);
            }
            var campaignData = _campaignRepository.CreateCampaign(campaign);
            if (campaignData.Success)
            {
                _logger.LogInformation(LogConstants.SUCCESS_CREATE_CAMPAIGN);
                return Ok(campaignData);
            }
            _logger.LogError(LogConstants.ERROR_CREATE_CAMPAIGN);
            return BadRequest(campaignData);
        }

        [HttpGet("GetCampaignInfo")]
        public IActionResult GetCampaignInfo(string campaignName)
        {
            IResult campaignExists = _campaignRepository.CheckCampaign(campaignName);
            if (!campaignExists.Success)
            {
                _logger.LogError(LogConstants.FAIL_GET_CAMPAIGN, campaignExists.Message);
                return BadRequest(campaignExists);
            }
            IResult campaignInfo = _campaignRepository.GetCampaignInfo(campaignName);
            if (campaignInfo.Success)
            {
                _logger.LogInformation(LogConstants.SUCCESS_GET_CAMPAIGN);
                return Ok(campaignInfo);
            }
            _logger.LogError(LogConstants.ERROR_GET_CAMPAIGN);
            return BadRequest(campaignInfo);
        }

        [HttpGet("IncreaseTime")]
        public IActionResult IncreaseTime(int hour)
        {
            IResult campaignExists = _campaignRepository.IncreaseTime(hour);
            if (campaignExists.Success)
            {
                _logger.LogInformation(LogConstants.SUCCESS_INCREASE_TIME);
                return Ok(campaignExists);
            }
            _logger.LogError(LogConstants.FAIL_INCREASE_TIME);
            return BadRequest(campaignExists);
        }
    }
}
