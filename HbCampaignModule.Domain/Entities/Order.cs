using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HbCampaignModule.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            Campaign = new Campaign();
        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public int SoldPrice { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        //public string? CampaignCode { get; set; }
        [ForeignKey("CampaignId")]
        public Campaign Campaign { get; set; }
    }
}
