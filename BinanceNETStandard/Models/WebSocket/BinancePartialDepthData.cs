using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BinanceNETStandard.API.Converter;
using BinanceNETStandard.API.Models.Response;
using Newtonsoft.Json;
using BinanceNETStandard.API.Models.WebSocket.Interfaces;

namespace BinanceNETStandard.API.Models.WebSocket
{
    [DataContract]
    public class BinancePartialDepthData : IWebSocketResponse
    {
        [DataMember(Order = 1)]
        [JsonProperty(PropertyName = "stream")]
        public string Stream { get; set; }

        [DataMember(Order = 2)]
        [JsonProperty(PropertyName = "data")]
        public BinancePartialData Data { get; set; }


        public string EventType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime EventTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
