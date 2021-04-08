using HbCampaignModule.Domain.ResultsIF;

namespace HbCampaignModule.Domain.Results
{
    public class DataResult<T>:Result,IDataResult<T>
    {
        public DataResult(T data, bool success, string message, HttpResponse httpResponse) : base(success,message, httpResponse)
        {
            Data = data;
        }
        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }
        public T Data { get; }
    }
}
