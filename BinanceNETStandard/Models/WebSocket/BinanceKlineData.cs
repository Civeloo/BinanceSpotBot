using System;
using System.Runtime.Serialization;
using BinanceNETStandard.API.Converter;
using BinanceNETStandard.API.Models.WebSocket.Interfaces;
using Newtonsoft.Json;

namespace BinanceNETStandard.API.Models.WebSocket
{
    /// <summary>
    /// Data returned from the Binance WebSocket Kline endpoint
    /// </summary>
    [DataContract]
    public class BinanceKlineData: ISymbolWebSocketResponse
    {
        [JsonProperty(PropertyName = "e")]
        [DataMember(Order = 1)]
        public string EventType { get; set; }

        [JsonProperty(PropertyName = "E")]
        [DataMember(Order = 2)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime EventTime { get; set; }

        [JsonProperty(PropertyName = "s")]
        [DataMember(Order = 3)]
        public string Symbol { get; set; }

        [JsonProperty(PropertyName = "K")]
        [DataMember(Order = 4)]
        public BinanceKline Kline { get; set; }
    }
}
