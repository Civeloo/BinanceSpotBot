using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using BinanceNETStandard.API.Converter;
using BinanceNETStandard.API.Models.Response.Interfaces;

namespace BinanceNETStandard.API.Models.Response
{  
    [DataContract]
    public class ExchangeInfoSymbolFilterMaxPosition : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public Decimal MaxPosition { get; set; }
    }
}