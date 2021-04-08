using HbCampaignModule.Domain.ResultsIF;

namespace HbCampaignModule.Domain.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message, HttpResponse httpCode) :this(success)
        {
            Message = message;
            HttpResponse = httpCode;
        }

        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
        public HttpResponse HttpResponse { get; }
    }

    public enum HttpResponse
    {
        Ok=200,
        NotFound=404,
        Conflict=409,

    }
}
