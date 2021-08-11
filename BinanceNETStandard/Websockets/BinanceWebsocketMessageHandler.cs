using BinanceNETStandard.API.Models.WebSocket.Interfaces;

namespace BinanceNETStandard.API.Websockets
{
    public delegate void BinanceWebSocketMessageHandler<in T>(T data) where T: IWebSocketResponse;
}