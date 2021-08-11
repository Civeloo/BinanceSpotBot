using System.Runtime.Serialization;
using Newtonsoft.Json;
using BinanceNETStandard.API.Enums;
using Newtonsoft.Json.Converters;

namespace BinanceNETStandard.API.Models.Response
{
    [DataContract]
    public class ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ExchangeInfoSymbolFilterType FilterType { get; set; }
    }
}
