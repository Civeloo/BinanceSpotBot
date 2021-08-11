namespace BinanceNETStandard.API.Models.ResultSets
{
    public class BuySellDepthVolume
    {
        public decimal BidBase { get; set; }
        public decimal AskBase { get; set; }
        public decimal BidQuantity { get; set; }
        public decimal AskQuantity { get; set; }
    }
}
