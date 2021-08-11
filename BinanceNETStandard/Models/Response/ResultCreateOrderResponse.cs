using System.Runtime.Serialization;
using BinanceNETStandard.API.Enums;
using BinanceNETStandard.API.Models.Response.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BinanceNETStandard.API.Models.Response
{
    /// <summary>
    /// Result Response following a call to the Create Order endpoint
    /// </summary>
    [DataContract]
    public partial class ResultCreateOrderResponse : BaseCreateOrderResponse
    {
        [DataMember(Order = 4)]
        public decimal Price { get; set; }

        [DataMember(Order = 5)]
        [JsonProperty(PropertyName = "origQty")]
        public decimal OriginalQuantity { get; set; }

        [DataMember(Order = 6)]
        [JsonProperty(PropertyName = "executedQty")]
        public decimal ExecutedQuantity { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Order = 7)]
        public OrderStatus Status { get; set; }

        [DataMember(Order = 8)]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSide Side { get; set; }

        [DataMember(Order = 9)]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType Type { get; set; }

        [DataMember(Order = 10)]
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeInForce? TimeInForce { get; set; }

    }
}