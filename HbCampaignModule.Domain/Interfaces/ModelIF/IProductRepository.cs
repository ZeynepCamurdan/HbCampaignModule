using HbCampaignModule.Domain.Entities;
using HbCampaignModule.Domain.Model;
using HbCampaignModule.Domain.ResultsIF;

namespace HbCampaignModule.Domain.Interfaces.ModelIF
{
    public interface IProductRepository  {
        IDataResult<ProductDto> GetProductInfo(string productCode);
        IDataResult<ProductDto> CreateProduct(ProductDto model);
        IResult ProductExists(string productCode);
        public Product GetProductData(string productCode);
        IResult CheckProduct(string productCode);
    }
    
}
