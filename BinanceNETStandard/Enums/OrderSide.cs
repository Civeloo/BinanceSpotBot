using System.Runtime.Serialization;

namespace BinanceNETStandard.API.Enums
{
    public enum OrderSide
    {
        [EnumMember(Value = "BUY")]
        Buy,
        [EnumMember(Value = "SELL")]
        Sell,
    }
}