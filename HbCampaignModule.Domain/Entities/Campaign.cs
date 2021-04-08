using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.Entities
{
    public class Campaign : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public int PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }
        public int TotalSales { get; set; }
        public int Turnover { get; set; }
        public int AverageItemPrice { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Order> Orders { get; set; }


    }
}
