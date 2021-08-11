using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BinanceNETStandard.API.Converter;
using BinanceNETStandard.API.Models.Response;
using BinanceNETStandard.API.Models.WebSocket.Interfaces;
using Newtonsoft.Json;

namespace BinanceNETStandard.API.Models.WebSocket
{
    [DataContract]
    public class BinanceDataCombined : IWebSocketResponse
    {
        [DataMember(Order = 1)]
        [JsonProperty(PropertyName = "e")]
        public string EventType { get; set; }

        [DataMember(Order = 2)]
        [JsonProperty(PropertyName = "E")]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime EventTime { get; set; }

        [DataMember(Order = 3)]
        [JsonProperty(PropertyName = "s")]
        public string Symbol { get; set; }

        [DataMember(Order = 4)]
        [JsonProperty(PropertyName = "u")]
        public long UpdateId { get; set; }

        [DataMember(Order = 5)]
        [JsonProperty(PropertyName = "b")]
        [JsonConverter(typeof(TraderPriceConverter))]
        public List<TradeResponse> BidDepthDeltas { get; set; }

        [DataMember(Order = 6)]
        [JsonProperty(PropertyName = "a")]
        [JsonConverter(typeof(TraderPriceConverter))]
        public List<TradeResponse> AskDepthDeltas { get; set; }
    }
}
