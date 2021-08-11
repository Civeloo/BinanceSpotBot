using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using BinanceNETStandard.API.Converter;
using BinanceNETStandard.API.Models.Response.Interfaces;

namespace BinanceNETStandard.API.Models.Response
{
    [DataContract]
    public class ExchangeInfoRateLimit
    {
        [DataMember(Order = 1)]
        public string RateLimitType { get; set; }

        [DataMember(Order = 2)]
        public string Interval { get; set; }

        [DataMember(Order = 3)]
        public int Limit { get; set; }
    }
}
