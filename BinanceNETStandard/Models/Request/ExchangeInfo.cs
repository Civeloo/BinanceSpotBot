using System;
using System.Runtime.Serialization;
using BinanceNETStandard.API.Converter;
using BinanceNETStandard.API.Models.Request.Interfaces;
using Newtonsoft.Json;

namespace BinanceNETStandard.API.Models.Request
{
    /// <summary>
    /// Request object used to retrieve exchange information
    /// </summary>
    [DataContract]
    public class ExchangeInfo : IRequest
    {
    }
}
