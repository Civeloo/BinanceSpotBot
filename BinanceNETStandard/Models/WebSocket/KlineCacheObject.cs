using System.Collections.Generic;
using BinanceNETStandard.API.Enums;

namespace BinanceNETStandard.API.Models.WebSocket
{
    public class KlineCacheObject
    {
        public Dictionary<KlineInterval, KlineIntervalCacheObject> KlineInterDictionary { get; set; }   
    }
}