﻿
namespace BinanceNETStandard.API.Models.Response.Interfaces
{
    public interface IConfirmationResponse : IResponse
    {
        bool Success { get; set; }
    }
}