using System.Collections.Generic;

namespace BinanceNETStandard.API.Models.WebSocket
{
    public class DepthCacheObject
    {
        public Dictionary<decimal, decimal> Asks { get; set; }
        public Dictionary<decimal, decimal> Bids { get; set; }
    }

}