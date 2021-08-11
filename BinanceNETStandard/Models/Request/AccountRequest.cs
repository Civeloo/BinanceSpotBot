using System;
using System.Runtime.Serialization;
using BinanceNETStandard.API.Converter;
using BinanceNETStandard.API.Models.Request.Interfaces;
using Newtonsoft.Json;

namespace BinanceNETStandard.API.Models.Request
{
    /// <summary>
    /// Request object used to retrieve current account information
    /// </summary>
    [DataContract]
    public class AccountRequest : IRequest
    {
        [DataMember(Order = 1)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime TimeStamp { get; set; }
    }
}