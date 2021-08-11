using System.Runtime.Serialization;
using BinanceNETStandard.API.Enums;
using BinanceNETStandard.API.Models.Response.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BinanceNETStandard.API.Models.Response
{
    [DataContract]
    public class SystemStatusResponse : IResponse
    {
        [DataMember(Order = 1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public SystemStatus Status { get; set; }

        [DataMember(Order = 2)]
        [JsonProperty(PropertyName = "msg")]
        public string Message { get; set; }
    }
}