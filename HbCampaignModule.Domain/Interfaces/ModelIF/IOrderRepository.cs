using HbCampaignModule.Domain.Model;
using HbCampaignModule.Domain.ResultsIF;

namespace HbCampaignModule.Domain.Interfaces.ModelIF
{
    public interface IOrderRepository
    {
        IDataResult<OrderDto> CreateOrder(OrderDto order);
        IResult CheckStock(OrderDto order);
       // IResult ProductExists(string productCode);
        IResult CheckProduct(string productCode);
    }
}
