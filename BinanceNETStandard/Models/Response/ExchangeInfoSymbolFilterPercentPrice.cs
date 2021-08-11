using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using BinanceNETStandard.API.Converter;
using BinanceNETStandard.API.Models.Response.Interfaces;

namespace BinanceNETStandard.API.Models.Response
{
   [DataContract]
    public class ExchangeInfoSymbolFilterPercentPrice : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public Decimal MultiplierUp { get; set; }

        [DataMember(Order = 2)]
        public Decimal MultiplierDown { get; set; }

        [DataMember(Order = 3)]
        public Decimal AvgPriceMins { get; set; }
    }
}
