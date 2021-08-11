using System;
using System.Runtime.Serialization;
using BinanceNETStandard.API.Converter;
using BinanceNETStandard.API.Models.Request.Interfaces;
using Newtonsoft.Json;

namespace BinanceNETStandard.API.Models.Request
{
    /// <summary>
    /// Request object used to get compressed aggregatae trades
    /// </summary>
    [DataContract]
    public class GetCompressedAggregateTradesRequest : IRequest
    {
        [DataMember(Order = 1)]
        public string Symbol { get; set; }

        [DataMember(Order = 2)]
        public string FromId { get; set; }

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