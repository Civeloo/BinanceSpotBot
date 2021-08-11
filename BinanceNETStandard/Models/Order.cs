using BinanceNETStandard.API.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinanceNETStandard.Models
{
    public class Order
    {
        public string Symbol { get; set; }
        public long OrderId { get; set; }
        public string ClientOrderId { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalQuantity { get; set; }
        public decimal ExecutedQuantity { get; set; }
        public OrderStatus Status { get; set; }
        public TimeInForce TimeInForce { get; set; }
        public string Type { get; set; }
        public string Side { get; set; }
        public decimal StopPrice { get; set; }
        public decimal IcebergQuantity { get; set; }
        public DateTime Time { get; set; }
        public bool IsWorking { get; set; }
        public decimal CummulativeQuoteQuantity { get; set; }
        public decimal Quantity { get; set; }
        public string NewClientOrderId { get; set; }
    }
}
