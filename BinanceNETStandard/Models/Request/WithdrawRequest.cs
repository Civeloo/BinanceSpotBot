using System.Runtime.Serialization;
using BinanceNETStandard.API.Converter;
using BinanceNETStandard.API.Models.Request.Interfaces;
using Newtonsoft.Json;

namespace BinanceNETStandard.API.Models.Request
{
    [DataContract]
    public class WithdrawRequest : IRequest
    {
        [DataMember(Order = 1)]
        public string Asset { get; set; }

        [DataMember(Order = 2)]
        public string Address { get; set; }

        /// <summary>
        /// Secondary Identifier for coins, like XRP, XMR etc
        /// </summary>
        [DataMember(Order = 3)]
        public string AddressTag { get; set; }

        [DataMember(Order = 4)]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal Amount { get; set; }

        /// <summary>
        /// Description of the address
        /// </summary>
        [DataMember(Order = 5)]
        public string Name { get; set; }
    }
}