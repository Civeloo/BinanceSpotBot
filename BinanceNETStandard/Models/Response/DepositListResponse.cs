using System.Runtime.Serialization;
using System.Collections.Generic;
using BinanceNETStandard.API.Models.Response.Interfaces;

namespace BinanceNETStandard.API.Models.Response
{
    [DataContract]
    public class DepositListResponse : IConfirmationResponse
    {
        [DataMember(Order = 1)]
        public List<DepositListItem> DepositList { get; set; }

        [DataMember(Order = 2)]
        public bool Success { get; set; }
    }
}