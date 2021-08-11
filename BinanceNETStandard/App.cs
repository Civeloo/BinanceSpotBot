using BinanceNETStandard.API.Client;
using BinanceNETStandard.API.Enums;
using BinanceNETStandard.API.Market;
using BinanceNETStandard.API.Models.Request;
using BinanceNETStandard.API.Models.Response.Error;
using BinanceNETStandard.API.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BinanceNETStandard.Models;
using BinanceNETStandard.API.Models.Response;

namespace BinanceNETStandard
{
    public class App
    {
        BinanceClient client;
        public BinanceNETStandard.API.Models.Response.ExchangeInfoResponse exchangeInfo;
        decimal priceFilterTickSize;
        decimal lootStepSize;
        int recvWindow = 5000;
        //public bool connected;
        public App(string apiKey, string secretKey)
        {
            try {
                //Provide your configuration and keys here, this allows the client to function as expected.
                //string apiKey = "YOUR_API_KEY";
                //string secretKey = "YOUR_SECRET_KEY";

                //System.Console.WriteLine("--------------------------");
                //System.Console.WriteLine("BinanceExchange API - Tester");
                //System.Console.WriteLine("--------------------------");

                ////Building a test logger
                //var exampleProgramLogger = LogManager.GetLogger(typeof(App));
                //exampleProgramLogger.Debug("Logging Test");

                //Initialise the general client client with config
                client = new BinanceClient(new ClientConfiguration()
                {
                    ApiKey = apiKey,
                    SecretKey = secretKey,
                    //Logger = exampleProgramLogger,
                });
                Test();
                //System.Console.WriteLine("Interacting with Binance...");

                // Test the Client
                //var test = client.TestConnectivity().Result;
            }
            catch (BinanceBadRequestException badRequestException)
            {

            }
            catch (BinanceServerException serverException)
            {

            }
            catch (BinanceTimeoutException timeoutException)
            {

            }
            catch (BinanceException unknownException)
            {

            }
        }

        public async Task<string> Test() 
        {
            try 
            {
                var tc = await client.TestConnectivity();
                await GetExchangeInfo();
                var s = await client.GetServerTime();
                var sT = new DateTimeOffset(s.ServerTime).ToUnixTimeMilliseconds();
                var dL = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(); 
                recvWindow = Convert.ToInt32((dL) - sT);
                await GetAccountInformation();
                return "OK";
            }
            catch (BinanceBadRequestException badRequestException)
            {
                return badRequestException.ToString();
            }
            catch (BinanceServerException serverException)
            {
                return serverException.ToString();
            }
            catch (BinanceTimeoutException timeoutException)
            {
                return timeoutException.ToString();
            }
            catch (BinanceException unknownException)
            {
                return unknownException.ToString();
            }
        }        

        // Get All Orders
        public async Task<IEnumerable<object>> GetAllOrders(string symbol, int limit)
        {
            IEnumerable<object> allOrders = null;
            try
            {
                var allOrdersRequest = new BinanceNETStandard.API.Models.Request.AllOrdersRequest()
                {
                    Symbol = symbol,//"BNBBUSD",//
                    Limit = limit,//1,//
                };
                //allOrdersRequest = new AllOrdersRequest()
                //{
                //    Symbol = TradingPairSymbols.BTCPairs.ETH_BTC,
                //    Limit = 5,
                //};
                // Get All Orders
                var ao = await client.GetAllOrders(allOrdersRequest);
                allOrders = (IEnumerable<object>)ao.ToArray();
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }            
            return allOrders;
        }

        // Get Filled Order
        public async Task<object> GetFilledOrder(string symbol)
        {
            BinanceNETStandard.API.Models.Response.OrderResponse orderFilled = null;
            try
            {
                // GetOrder
                var allOrdersRequest = new BinanceNETStandard.API.Models.Request.AllOrdersRequest()
                {
                    Symbol = symbol,
                    Limit = 1000,
                };
                var orders = await client.GetAllOrders(allOrdersRequest, recvWindow);
                foreach(var order in orders)   
                    if (order.Status == OrderStatus.Filled) orderFilled = order;                                       
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            return orderFilled;
        }

        // Get the order book, and use the cache
        public async Task<object> GetOrderBook(string symbol)
        {
            return await client.GetOrderBook(symbol, true);
        }

        // Cancel an order
        public async Task<object> CancelOrder(string symbol, long id)
        {
            return await client.CancelOrder(new CancelOrderRequest()
            {
                //NewClientOrderId = "123456",
                OrderId = id,//523531,
                //OriginalClientOrderId = "789",
                Symbol = symbol,//"ETHBTC",
            }, recvWindow);
        }

        public OrderSide GetOrderSide(string orderSide)
        {
            var side = OrderSide.Buy;
            if ((orderSide == "SELL") || (orderSide == "Sell")) side = OrderSide.Sell;
            return side;
        }

        public OrderType GetOrderType(string orderType)
        {
            var type = OrderType.Market;
            //"MARKET","STOP_LOSS","STOP_LOSS_LIMIT","TAKE_PROFIT","TAKE_PROFIT_LIMIT","LIMIT_MAKER",
            switch (orderType)
            {
                case "MARKET":
                    type = OrderType.Market;
                    break;
                case "Market":
                    type = OrderType.Market;
                    break;
                case "STOP_LOSS":
                    type = OrderType.StopLoss;
                    break;
                case "StopLoss":
                    type = OrderType.StopLoss;
                    break;
                case "STOP_LOSS_LIMIT":
                    type = OrderType.StopLossLimit;
                    break;
                case "StopLossLimit":
                    type = OrderType.StopLossLimit;
                    break;
                case
                    "TAKE_PROFIT":
                    type = OrderType.TakeProfit;
                    break;
                case
                    "TakeProfit":
                    type = OrderType.TakeProfit;
                    break;
                case
                    "TAKE_PROFIT_LIMIT":
                    type = OrderType.TakeProfitLimit;
                    break;
                case
                    "TakeProfitLimit":
                    type = OrderType.TakeProfitLimit;
                    break;
                case
                    "LIMIT_MAKER":
                    type = OrderType.LimitMaker;
                    break;
                case
                    "LimitMaker":
                    type = OrderType.LimitMaker;
                    break;
            }
            return type;
        }

        public async Task<bool> GetExchangeInfo()
        {
            exchangeInfo = await client.GetExchangeInfo();
            return true;// (exchangeInfo != null);
        }

        // Create an order with varying options
        public async Task<object> CreateOrder(Models.Order order)
        {
            var quantity = order.Quantity;
            var symbol = order.Symbol;
            if (quantity > 0)
            {
                GetPriceLootSize(symbol);
                //quantity = (quantity* 0.75M);//75%
                //decimal lootStepSize = Convert.ToDecimal(1 / Math.Pow(10, 5));// 0.1M;
                decimal loots = quantity / lootStepSize;
                loots = Decimal.Truncate(loots);
                quantity = (loots * lootStepSize);
                //    decimal decimals = Convert.ToDecimal(Math.Pow(10, 2));
                decimal decimals = 1 / priceFilterTickSize;
                var newOrder = new Order()
                {
                    //IcebergQuantity = 100,
                    Price = (Math.Truncate(decimals * order.Price) / decimals),//Math.Round(price, 5),//1800,
                    StopPrice = (Math.Truncate(decimals * order.StopPrice) / decimals),//Math.Round(stopPrice, 5),//1800,
                    Quantity = quantity,//0.6m,
                    Side = order.Side,//"SELL",
                    Symbol = symbol,//"ETHBUSD",
                    Type = order.Type,//"TAKE_PROFIT_LIMIT","MARKET","STOP_LOSS","STOP_LOSS_LIMIT","TAKE_PROFIT","TAKE_PROFIT_LIMIT","LIMIT_MAKER",
                };
                //orderCreated = await CreateOrder(newOrder);
                order = newOrder;
            }            
            return await client.CreateOrder(new BinanceNETStandard.API.Models.Request.CreateOrderRequest()
            {
                //IcebergQuantity = order.IcebergQuantity,//100,
                Price = order.Price,//230,
                Quantity = order.Quantity,//0.6m,
                Side = GetOrderSide(order.Side),//OrderSide.Buy,
                Symbol = order.Symbol,//"ETHBTC",
                Type = GetOrderType(order.Type),
                StopPrice = order.StopPrice,
                TimeInForce = TimeInForce.GTC,
            });
        }
        // Get account information
        public async Task<object> GetAccountInformation()
        {
            return await client.GetAccountInformation(recvWindow);
        }

        // Get account trades
        public async Task<object> GetAccountTrades(string symbol, int id)
        {
            return await client.GetAccountTrades(new AllTradesRequest()
            {
                FromId = id,//352262,
                Symbol = symbol,//"ETHBTC",
            }, recvWindow);
        }

        // Get a list of Compressed aggregate trades with varying options
        public async Task<object> GetCompressedAggregateTrades(string symbol)
        {
            return await client.GetCompressedAggregateTrades(new GetCompressedAggregateTradesRequest()
            {
                StartTime = DateTime.UtcNow.AddDays(-1),
                Symbol = symbol,//"ETHBTC",
            });
        }

        // Get current open orders for the specified symbol
        public async Task<object> GetCurrentOpenOrders(string symbol)
        {
            return await client.GetCurrentOpenOrders(new CurrentOpenOrdersRequest()
            {
                Symbol = symbol,//"ETHBTC",
            }, recvWindow);
        }

        // Get daily ticker
        public async Task<object> GetDailyTicker(string symbol)
        {
            return await client.GetDailyTicker(symbol);
        }

        // Get Symbol Order Book Ticket
        public async Task<object> GetSymbolOrderBookTicker()
        {
            return await client.GetSymbolOrderBookTicker();
        }

        // Get Symbol Order Price Ticker
        public async Task<object> GetSymbolsPriceTicker()
        {
            return await client.GetSymbolsPriceTicker();
        }

        // Query a specific order on Binance
        public async Task<object> QueryOrder(string symbol, int id)
        {
            return await client.QueryOrder(new QueryOrderRequest()
            {
                OrderId = id,//5425425,
                Symbol = symbol,//"ETHBTC",
            }, recvWindow);
        }
        public async Task<object> TradingBot(string assetBuy, string assetSell, decimal variation, bool buy, bool sell, bool positive)
        {
            object orderCreated = null;
            var symbol = assetBuy + assetSell;
            long orderId = 0;
            decimal orderQuantity = 0;
            decimal quantity = 0;
            decimal orderPrice = 0;
            string side = "";
            decimal orderStopPrice = 0;
            string orderSymbol = "";
            string type = "";
            string orderStatus = "";
            bool statusCancelled = false;
            bool statusFilled = false;
            bool statusNew = false;
            bool satusNotOrder = true;
            decimal filledBuy = 0;
            decimal filledSell = 0;
            decimal filledPrice = 0;
            string filledSide = "";
            try
            {
                
                try
                {
                    // GetOrder
                    var allOrdersRequest = new BinanceNETStandard.API.Models.Request.AllOrdersRequest()
                    {
                        Symbol = symbol,
                        Limit = 1,
                    };
                    var orderList = await client.GetAllOrders(allOrdersRequest, recvWindow);
                    if (orderList!=null)
                    { 
                        var order = orderList[0];
                        orderId = order.OrderId;
                        orderQuantity = order.OriginalQuantity;
                        quantity = orderQuantity;
                        orderPrice = order.Price;
                        var orderSide = order.Side;
                        side = orderSide.ToString();
                        orderStopPrice = order.StopPrice;
                        orderSymbol = order.Symbol;
                        var orderType = order.Type;
                        type = orderType.ToString();
                        orderStatus = order.Status.ToString();
                        statusCancelled = (orderStatus == "Cancelled");
                        statusFilled = (orderStatus == "Filled");
                        statusNew = (orderStatus == "New");
                        satusNotOrder = (statusFilled || statusCancelled);
                    }

                    filledBuy = 0;
                    filledSell = 0;
                    filledPrice = 0;
                    filledSide = "";                    
                    if (!statusFilled)
                    {
                        dynamic filled = await GetFilledOrder(symbol);
                        filledSide = filled.Side.ToString();
                        filledPrice = filled.Price;
                    } 
                    else
                    {
                        filledSide = side;
                        filledPrice = orderPrice;                    
                    }
                    if (filledSide == "Buy") filledBuy = filledPrice;
                    else filledSell = filledPrice;

                }
                catch (Exception e)
                {
                    Console.Write(e.ToString());
                }
               
                //GetDailyTicker
                dynamic dailyTicker = await GetDailyTicker(symbol);
                var tickerLastPrice = dailyTicker.LastPrice;
                var tickerPriceChange = dailyTicker.PriceChange;
                var price = tickerLastPrice;
                var stopPrice = price - 10;
                var diff = (tickerLastPrice - orderPrice);//48-49=-1
                var diffAbs = Math.Abs(Math.Floor(diff));
                decimal variationSell = 0;
                decimal variationBuy = 0;
                decimal variationMin = 0;
                variationMin = 5;
                //if (diff > 0)//
                if (tickerPriceChange > 0)
                {
                    variationSell = 1;
                    variationBuy = 1;//variationMin;
                }
                else
                {
                    variationSell = 1;// variationMin;
                    variationBuy = 1;
                }

                var variationAbs = Math.Abs(variation);
              
                if (variationAbs == 0)
                {
                    variationAbs = tickerPriceChange;
                    decimal priceVarMin = price * 0.02M;
                    if (variationAbs < priceVarMin) variationAbs = priceVarMin;
                }

                var lastPriceSell = (tickerLastPrice - (variationAbs / variationSell));
                var lastPriceBuy = (tickerLastPrice + (variationAbs / variationBuy));
                //variation = variationAbs;
                bool sellUpPriceOk = ( (filledBuy != 0) && ( filledBuy < lastPriceSell ) );
                bool sellPriceOk = ( satusNotOrder || ( (side == "Sell") && (lastPriceSell > orderPrice) ) );
                bool isSellOk = (sell && sellPriceOk);                
                
                bool buyLowPriceOk = ((filledSell > lastPriceBuy));// || (filledSell == 0) );
                bool buyPriceOk = ( satusNotOrder || ( (side == "Buy") && (lastPriceBuy < orderPrice) ) );
                bool isBuyOk = ( buy && buyPriceOk );

                if (positive)
                {
                    isSellOk = (isSellOk && sellUpPriceOk);
                    isBuyOk = (isBuyOk && buyLowPriceOk);
                }

                if ( (satusNotOrder || (diffAbs > (variationAbs / variationMin)))  )//20.09>20
                {
                    if (statusNew && (isSellOk || isBuyOk))//129--8>145
                    {
                        await CancelOrder(symbol, orderId);
                        satusNotOrder = true;
                    }
                    if (satusNotOrder)
                    {
                        //GetBalances{
                        decimal assetBuyQuantity = 0;
                        decimal assetSellQuantity = 0;
                        var accountInformation = await client.GetAccountInformation(recvWindow);
                        var balances = accountInformation.Balances;
                        foreach (var balance in balances)
                        {
                            if (assetBuy == balance.Asset) assetBuyQuantity = balance.Free;
                            else if (assetSell == balance.Asset) assetSellQuantity = balance.Free;
                        }
                        bool isSell = ((assetBuyQuantity * tickerLastPrice) > assetSellQuantity);
                        assetSellQuantity = assetSellQuantity / lastPriceBuy;// (tickerLastPrice + variationAbs);
                        //
                        quantity = 0;
                        if (isSell && isSellOk)
                        {
                            price = lastPriceSell;//tickerLastPrice - (variationAbs / variationSell);
                            stopPrice = price + (variationAbs / 10);
                            quantity = assetBuyQuantity;
                            type = "STOP_LOSS_LIMIT";
                            side = "SELL";
                        }
                        else
                        {
                            if (!isSell && isBuyOk)
                            {
                                price = lastPriceBuy;//tickerLastPrice + (variationAbs / variationBuy);
                                stopPrice = price - (variationAbs / 10);
                                quantity = assetSellQuantity;
                                type = "STOP_LOSS_LIMIT";
                                side = "BUY";
                            }
                        }

                        //if (diff < 0)//Price below market price: STOP_LOSS SELL, TAKE_PROFIT BUY
                        //{
                        //    if (isSell)
                        //    {
                        //        price = tickerLastPrice - variation;
                        //            stopPrice = price + (variation / 4);
                        //        quantity = assetBuyQuantity;
                        //        type = "STOP_LOSS_LIMIT";
                        //        side = "SELL";
                        //    }
                        //    else
                        //    {
                        //        price = tickerLastPrice + variation;
                        //            stopPrice = price - (variation/4);
                        //        quantity = assetSellQuantity;
                        //        type = "TAKE_PROFIT_LIMIT";
                        //        side = "BUY";
                        //    }
                        //}
                        //else//Price above market price: STOP_LOSS BUY, TAKE_PROFIT SELL 
                        //{
                        //    if (isSell)
                        //    {
                        //        price = tickerLastPrice - variation;
                        //            stopPrice = price + (variation / 4);
                        //        quantity = assetBuyQuantity;
                        //        type = "TAKE_PROFIT_LIMIT";
                        //        side = "SELL";
                        //    }
                        //    else
                        //    {
                        //        price = tickerLastPrice + variation;
                        //            stopPrice = price - (variation / 4);
                        //        quantity = assetSellQuantity;
                        //        type = "STOP_LOSS_LIMIT";
                        //        side = "BUY";
                        //    }
                        //}
                        if (quantity > 0)
                        {
                            GetPriceLootSize(symbol);
                            //    //quantity = (quantity* 0.75M);//75%
                            //decimal lootStepSize = Convert.ToDecimal(1 / Math.Pow(10, 5));// 0.1M;
                            decimal loots = quantity / lootStepSize;
                            loots = Decimal.Truncate(loots);
                            quantity = (loots * lootStepSize);
                            //    decimal decimals = Convert.ToDecimal(Math.Pow(10, 2));
                            decimal decimals = 1 / priceFilterTickSize;

                            var newOrder = new Order()
                            {
                                //IcebergQuantity = 100,
                                Price = (Math.Truncate(decimals * price) / decimals),//Math.Round(price, 5),//1800,
                                StopPrice = (Math.Truncate(decimals * stopPrice) / decimals),//Math.Round(stopPrice, 5),//1800,
                                Quantity = quantity,//0.6m,
                                Side = side,//"SELL",
                                Symbol = symbol,//"ETHBUSD",
                                Type = type,//"TAKE_PROFIT_LIMIT",//"MARKET","STOP_LOSS","STOP_LOSS_LIMIT","TAKE_PROFIT","TAKE_PROFIT_LIMIT","LIMIT_MAKER",
                            };
                            orderCreated = await CreateOrder(newOrder);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return orderCreated;
        }

        public void GetPriceLootSize(string symbol)
        {
            var exchangeInfoSymbols = exchangeInfo.Symbols;
            //foreach (var eis in exchangeInfoSymbols)
            //{
            //    eis.Symbol==symbol
            //}
            var exchangeInfoSymbol = from i in exchangeInfoSymbols
                                     where i.Symbol == symbol
                                     select i;
            var eisFilters = exchangeInfoSymbol.FirstOrDefault().Filters;

            var eisfLoot = from i in eisFilters
                           where i.FilterType.ToString() == "LotSize"
                           select i;
            dynamic eisfLootList = eisfLoot.ToList();
            lootStepSize = eisfLootList[0].StepSize;
            /*
            {
                "filterType": "PRICE_FILTER",
                "minPrice": "0.00000100",
                "maxPrice": "100000.00000000",
                "tickSize": "0.00000100"
            },
             */
            var eisfPrice = from i in eisFilters
                            where i.FilterType.ToString() == "PriceFilter"
                            select i;
            dynamic eisfPriceList = eisfPrice.ToList();
            priceFilterTickSize = eisfPriceList[0].TickSize;
        }

    }
}
