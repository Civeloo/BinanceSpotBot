using System.Collections.Generic;
using System.Runtime.Serialization;
using BinanceNETStandard.API.Models.Response.Interfaces;

namespace BinanceNETStandard.API.Models.Response
{
    [DataContract]
    public class WithdrawListResponse : IConfirmationResponse
    {
        [DataMember(Order = 1)]
        public List<WithdrawListItem> WithdrawList { get; set; }

        [DataMember(Order = 2)]
        public bool Success { get; set; }
    }
}