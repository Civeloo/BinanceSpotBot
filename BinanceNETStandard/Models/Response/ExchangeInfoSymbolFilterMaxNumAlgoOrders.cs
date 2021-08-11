using System;
using System.Runtime.Serialization;

namespace BinanceNETStandard.API.Models.Response
{
    [DataContract]
    public class ExchangeInfoSymbolFilterMaxNumAlgoOrders : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public Decimal MaxNumAlgoOrders { get; set; }
    }
}
