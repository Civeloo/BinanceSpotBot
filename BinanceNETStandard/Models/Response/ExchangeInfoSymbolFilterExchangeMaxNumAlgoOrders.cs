﻿using System.Runtime.Serialization;

namespace BinanceNETStandard.API.Models.Response
{
    [DataContract]
    public class ExchangeInfoSymbolFilterExchangeMaxNumAlgoOrders : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public int Limit { get; set; }
    }
}