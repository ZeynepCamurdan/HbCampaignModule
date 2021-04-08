using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.Model
{
    public class OrderDto : BaseDto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string? CampaignCode { get; set; }
        public string ProductCode { get; set; }
        public int SoldPrice { get; set; }
    }
}
