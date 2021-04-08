using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.Constants
{
    public static class OrderConstants
    {
        public static string CHECK_STOCK = "There are no products with this product code";
        public static string CHECK_STOCK_NOT_ENOUGH = "There is not enough stock for the product";
        public static string SUCCESS_CREATE = "Order created; product {0}, quantity {1}";
        public static string CHECK_PRODUCT = "There are no products with this product code";
    }
}
