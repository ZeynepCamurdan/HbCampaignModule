using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.Constants
{
    public static class CampaignConstants
    {
        public static string GET_CAMPAING_MESSAGE = "Campaign {0}, info; Status {1}, Target Sales {2},Total Sales {3}, Turnover {4}, Average Item Price {5}";
        public static string ACTIVE = "Active";
        public static string ENDED = "Ended";
        public static string PRODUCT_EXIST_MESSAGE = "There is no product for this campaign";
        public static string CAMPAING_EXISTS = "A new one cannot be created since it is available";
        public static string CHECHK_CAMPAIGN = "There are no campaign with this campaign name";
        public static string CHECHK_CAMPAIGN_PRODUCT = "There is no product for this campaign";
        public static string CREATE_ORDER = "Campaign created; name {0}, product {1}, duration {2}, limit {3}, target sales count {4}";
        public static string INCREASE_TIME = "Increase time: {0} ";
    }
}
