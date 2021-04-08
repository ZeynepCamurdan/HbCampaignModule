using HbCampaignModule.Domain.Model;
using HbCampaignModule.Domain.ResultsIF;

namespace HbCampaignModule.Domain.Interfaces.ModelIF
{
    public interface ICampaignRepository
    {
        IDataResult<CampaignDto> CreateCampaign(CampaignDto campaign);
        IDataResult<CampaignDto> GetCampaignInfo(string campaignName);
        IResult IncreaseTime(int hour);
        IResult CampaignExists(CampaignDto campaign);
        IResult ProductExists(string productCode);
        IResult CheckCampaign(string campaignName);
    }
}
