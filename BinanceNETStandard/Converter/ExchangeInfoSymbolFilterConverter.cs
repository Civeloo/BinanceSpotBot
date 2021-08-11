﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using BinanceNETStandard.API.Models.Response;
using BinanceNETStandard.API.Models.Response.Error;
using BinanceNETStandard.API.Enums;

namespace BinanceNETStandard.API.Converter
{
    public class ExchangeInfoSymbolFilterConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType) => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            ExchangeInfoSymbolFilter item = null;
            try
            {
                JObject jObject = JObject.Load(reader);
                var value = jObject.ToObject<ExchangeInfoSymbolFilter>();

                switch (value.FilterType)
                {
                    case ExchangeInfoSymbolFilterType.PriceFilter:
                        item = new ExchangeInfoSymbolFilterPrice();
                        break;
                    case ExchangeInfoSymbolFilterType.PercentPrice:
                        item = new ExchangeInfoSymbolFilterPercentPrice();
                        break;
                    case ExchangeInfoSymbolFilterType.LotSize:
                        item = new ExchangeInfoSymbolFilterLotSize();
                        break;
                    case ExchangeInfoSymbolFilterType.MinNotional:
                        item = new ExchangeInfoSymbolFilterMinNotional();
                        break;
                    case ExchangeInfoSymbolFilterType.MaxNumOrders:
                        item = new ExchangeInfoSymbolFilterMaxNumOrders();
                        break;
                    case ExchangeInfoSymbolFilterType.MaxNumAlgoOrders:
                        item = new ExchangeInfoSymbolFilterMaxNumAlgoOrders();
                        break;
                    case ExchangeInfoSymbolFilterType.MarketLotSize:
                        item = new ExchangeInfoSymbolFilterMarketLotSize();
                        break;
                    case ExchangeInfoSymbolFilterType.ExchangeMaxNumOrders:
                        item = new ExchangeInfoSymbolFilterExchangeMaxNumOrders();
                        break;
                    case ExchangeInfoSymbolFilterType.ExchangeMaxNumAlgoOrders:
                        item = new ExchangeInfoSymbolFilterExchangeMaxNumAlgoOrders();
                        break;
                    case ExchangeInfoSymbolFilterType.MaxNumIcebergOrders:
                        item = new ExchangeInfoSymbolFilterMaxNumIcebergOrders();
                        break;
                    case ExchangeInfoSymbolFilterType.PercentagePrice:
                        item = new ExchangeInfoSymbolFilterPercentagePrice();
                        break;
                    case ExchangeInfoSymbolFilterType.IcebergParts:
                        item = new ExchangeInfoSymbolFilterIcebergParts();
                        break;

                    case ExchangeInfoSymbolFilterType.MaxPosition:
                        item = new ExchangeInfoSymbolFilterMaxPosition();
                        break;

                }
                serializer.Populate(jObject.CreateReader(), item);
            } 
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            
            return item;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
