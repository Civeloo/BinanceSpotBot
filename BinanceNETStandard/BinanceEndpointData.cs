using System;
using BinanceNETStandard.API.Enums;

namespace BinanceNETStandard.API
{
    public class BinanceEndpointData
    {
        public Uri Uri;
        public EndpointSecurityType SecurityType;
        public bool UseCache { get; }

        public BinanceEndpointData(Uri uri, EndpointSecurityType securityType, bool useCache = false)
        {
            Uri = uri;
            SecurityType = securityType;
            UseCache = useCache;
        }

        public override string ToString()
        {
            return Uri.AbsoluteUri;
        }
    }
}