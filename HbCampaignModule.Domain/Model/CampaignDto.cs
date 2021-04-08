namespace HbCampaignModule.Domain.Model
{
    public class CampaignDto : BaseDto
    {
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int Duration { get; set; }
        public int PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }
        public string ProductCode { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
