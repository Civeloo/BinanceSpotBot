using System.Runtime.Serialization;
using BinanceNETStandard.API.Models.Response.Abstract;

namespace BinanceNETStandard.API.Models.Response
{
    /// <summary>
    /// Acknowledge Response following a call to the Create Order endpoint
    /// </summary>
    [DataContract]
    public class AcknowledgeCreateOrderResponse : BaseCreateOrderResponse
    {
    }
}