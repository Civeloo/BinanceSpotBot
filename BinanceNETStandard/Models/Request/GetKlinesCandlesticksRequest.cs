using System;
using System.Runtime.Serialization;
using BinanceNETStandard.API.Converter;
using BinanceNETStandard.API.Enums;
using BinanceNETStandard.API.Models.Request.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BinanceNETStandard.API.Models.Request
{
    /// <summary>
    /// Request object used to get Klines/Candlesticks
    /// </summary>
    [DataContract]
    public class GetKlinesCandlesticksRequest: IRequest
    {
        [DataMember(Order = 1)]
        public string Symbol { get; set; }

        [DataMember(Order = 2)]
        [JsonConverter(typeof(StringEnumConverter))]
        public KlineInterval Interval { get; set; }

        [DataMember(Order = 3)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime? StartTime { get; set; }

        [DataMember(Order = 4)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime? EndTime { get; set; }

        [DataMember(Order = 5)]
        public int? Limit { get; set; }

    }
}
