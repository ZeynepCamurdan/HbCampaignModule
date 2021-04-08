
namespace HbCampaignModule.Domain.Model
{
    public class ProductDto : BaseDto
    {
        public string ProductCode { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }
}
