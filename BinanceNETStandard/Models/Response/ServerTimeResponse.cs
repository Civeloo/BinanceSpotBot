using System;
using System.Runtime.Serialization;
using BinanceNETStandard.API.Converter;
using BinanceNETStandard.API.Models.Response.Interfaces;
using Newtonsoft.Json;

namespace BinanceNETStandard.API.Models.Response
{
    /// <summary>
    /// The current server time
    /// </summary>
    [DataContract]
    public class ServerTimeResponse: IResponse
    {
        [DataMember(Order = 1)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime ServerTime { get; set; }
    }
}