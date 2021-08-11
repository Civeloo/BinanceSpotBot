﻿using System.Runtime.Serialization;

namespace BinanceNETStandard.API.Models.Response
{
    [DataContract]
    public class ExchangeInfoSymbolFilterExchangeMaxNumOrders : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public int Limit { get; set; }
    }
}