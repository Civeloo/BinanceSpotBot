using System.Runtime.Serialization;
using BinanceNETStandard.API.Converter;
using Newtonsoft.Json;
using System;
using BinanceNETStandard.API.Enums;
using BinanceNETStandard.API.Models.Response.Interfaces;

namespace BinanceNETStandard.API.Models.Response
{

    [DataContract]
    public class DepositListItem : IResponse
    {
        [DataMember(Order = 1)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime InsertTime { get; set; }

        [DataMember(Order = 2)]
        public decimal Amount { get; set; }

        [DataMember(Order = 3)]
        public string Symbol { get; set; }

        [DataMember(Order = 4)]
        public string Address { get; set; }

        [DataMember(Order = 5)]
        [JsonProperty(PropertyName ="txId")]
        public string TransactionId { get; set; }

        [DataMember(Order = 6)]
        public DepositHistoryStatus Status { get; set; }
    }
}