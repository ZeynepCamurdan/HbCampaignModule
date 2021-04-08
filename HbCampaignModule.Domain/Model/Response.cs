using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.Model
{
    public class Response<T>
    {
        public T Data { get; set; }
        public ResponseType ResponseType { get; set; }
        public string Message { get; set; }
        public int? StatusCode { get; set; }
    }

    public enum ResponseType
    {
        Fail = 0,
        Success = 1
    }
}
