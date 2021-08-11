using System.Runtime.Serialization;

namespace BinanceNETStandard.API.Enums
{
    public enum NewOrderResponseType
    {
        [EnumMember(Value = "RESULT")]
        Result,
        [EnumMember(Value = "ACK")]
        Acknowledge,
        [EnumMember(Value = "FULL")]
        Full,
    }
}