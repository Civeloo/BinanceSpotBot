using System.Runtime.Serialization;
using BinanceNETStandard.API.Models.Request.Interfaces;

namespace BinanceNETStandard.API.Models.Request
{
    [DataContract]
    public class DepositAddressRequest: IRequest
    {
        [DataMember(Order = 1)]
        public string Asset { get; set; }
    }
}