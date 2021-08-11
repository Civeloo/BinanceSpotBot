using System;

namespace BinanceNETStandard.API.Models.WebSocket.Interfaces
{
    public interface IWebSocketResponse
    {
        string EventType { get; set; }

        DateTime EventTime { get; set; }
    }
}