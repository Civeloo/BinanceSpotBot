using System.Collections.Generic;

namespace BinanceNETStandard.API.Models.WebSocket
{
    public class KlineIntervalCacheObject
    {
        public Dictionary<long, KlineCandleStick> TimeKlineDictionary { get; set; }
    }
}