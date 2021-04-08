using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.Constants
{
    public static class ProductConstants
    {
        public static string CREATE_PRODUCT = "Product created; code {0} ,price {1}, stock {2}";
        public static string GET_PRODUCT_INFO = "Product info; price {0} , stock {1}";
        public static string PRODUCT_EXISTS = "A new one cannot be created since it is available";
        public static string CHECK_PRODUCT = "There are no products with this product code";
            
           
           
    }
}
