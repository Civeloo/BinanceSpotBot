
using BinanceNETStandard.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceSpotWFA
{
    public partial class BinanceSpotForm : Form
    {
        BinanceNETStandard.App bs;
        bool connected;
        string loadingText;
        public BinanceSpotForm()
        {        
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {            
            tbApiKey.Text = MyConfiguration.ReadSetting("ApiKey");
            tbSecretKey.Text = MyConfiguration.ReadSetting("SecretKey");
        }

        private async void bGetAllOrders_Click(object sender, EventArgs e)
        {
            bGetAllOrders.Text = LoadingText(bGetAllOrders.Text);
            try
            {
                Connect();
                var o = await bs.GetAllOrders(tbSymbol.Text + tbPair.Text, 10);// Convert.ToInt32(tbVariation.Text));

                var item =  o.ToList();
                //IEnumerable<OrderResponse> item = (IEnumerable<OrderResponse>) o;
                //var _bind = from i in item
                //            select new { i };
                //            {
                //                ClientOrderId = i[0],// i.ClientOrderId,
                //                CummulativeQuoteQuantity = i.CummulativeQuoteQuantity,
                //                ExecutedQuantity = i.ExecutedQuantity,
                //                IcebergQuantity = i.IcebergQuantity,
                //                IsWorking = i.IsWorking,
                //                OrderId = i.OrderId,
                //                OriginalQuantity = i.OriginalQuantity,
                //                Price = i.Price,
                //                Side = i.Side,
                //                Status = i.Status,
                //                StopPrice = i.StopPrice,
                //                Symbol = i.Symbol,
                //                Time = i.Time,
                //                TimeInForce = i.TimeInForce,
                //                Type = i.Type

                //            };
                //dgvOrder.DataSource = _bind;

                dgvOrder.DataSource = item;
                //dynamic i = item[0];
                //tbOrderId.Text = i.OrderId.ToString();
                //tbQuantity.Text = i.OriginalQuantity.ToString();
                //tbPrice.Text = i.Price.ToString();
                //cbSide.Text = i.Side.ToString();
                //tbStopPrice.Text = i.StopPrice.ToString();
                //tbSymbol.Text = i.Symbol.ToString();
                //cbType.Text = i.Type.ToString();
                //tbStatus.Text = i.Status.ToString();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
            bGetAllOrders.Text = LoadingText(bGetAllOrders.Text);
        }

        private async void bCreateOrder_Click(object sender, EventArgs e)
        {
            bCreateOrder.Text = LoadingText(bCreateOrder.Text);
            Connect();
            var order = new Order()
            {
                //IcebergQuantity = 100,
                Price = Convert.ToDecimal(tbPrice.Text),//1800,
                StopPrice = Convert.ToDecimal(tbStopPrice.Text),//1800,
                Quantity = Convert.ToDecimal(tbQuantity.Text),//0.6m,
                Side = cbSide.Text,//"SELL",
                Symbol = tbSymbol.Text + tbPair.Text,//"ETHBUSD",
                Type = cbType.Text,//"TAKE_PROFIT_LIMIT","MARKET","STOP_LOSS","STOP_LOSS_LIMIT","TAKE_PROFIT","TAKE_PROFIT_LIMIT","LIMIT_MAKER",
            };
            var o = await bs.CreateOrder(order);
            bCreateOrder.Text = LoadingText(bCreateOrder.Text);
        }

        public async void Connect()
        {
            if (!connected)
            {
                bs = new BinanceNETStandard.App(tbApiKey.Text, tbSecretKey.Text);
                try
                {
                    await bs.GetExchangeInfo();
                    connected = true;
                }                
                catch (Exception ex)
                {
                    ShowException(ex);
                }
            }
        }

        private async void bGetDailyTicker_Click(object sender, EventArgs e)
        {
            try
            {
                bGetDailyTicker.Text = LoadingText(bGetDailyTicker.Text);
                Connect();
                dynamic o = await bs.GetDailyTicker(tbSymbol.Text + tbPair.Text);
                tbLastPrice.Text = o.LastPrice.ToString();
                List<object> item = new List<object>();
                item.Add(o);
                dgvOrder.DataSource = item;                
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
            bGetDailyTicker.Text = LoadingText(bGetDailyTicker.Text);
        }
        public string LoadingText(string text)
        {
            var t = "Loading...";
            if (text == t)
            {
                text = loadingText;
            }
            else
            {
                loadingText = text;
                text = t;
            }
            return text;
        }

        private async void bCancelOrder_Click(object sender, EventArgs e)
        {
            bCancelOrder.Text = LoadingText(bCancelOrder.Text);
            Connect();
            await bs.CancelOrder(tbSymbol.Text, Convert.ToInt32(tbOrderId.Text));
            bCancelOrder.Text = LoadingText(bCancelOrder.Text);
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            
            try
            {
                Connect();
                dynamic order = await bs.TradingBot(tbSymbol.Text, tbPair.Text, Convert.ToDecimal(tbVariation.Text), cbBuy.Checked, cbSell.Checked, cbPositive.Checked);
                if (order != null) {
                    List<object> orderList = new List<object>();
                    orderList.Add(order);
                    dgvOrder.DataSource = orderList;
                    dgvOrder.Refresh();
                }                
            }
            catch (Exception ex)
            {
                //MessageBox.Show( ex.Message.ToString() );
            }
        }

        private void bStopLoss_Click(object sender, EventArgs e)
        {
            int ms = (Convert.ToInt32(tbTime.Text) * 60000);
            timer1.Interval = ms;
            if (timer1.Enabled == false)
            {                      
                timer1.Start();                
                bBot.Text = "STOP";
            }
            else
            {
                timer1.Stop();
                bBot.Text = "PLAY";
            }
        }

        private void bSaveKey_Click(object sender, EventArgs e)
        {
            MyConfiguration.AddUpdateAppSettings("ApiKey", tbApiKey.Text);
            MyConfiguration.AddUpdateAppSettings("SecretKey", tbSecretKey.Text);
        }

        private async void bGetLastFilledOrder_Click(object sender, EventArgs e)
        {
            bGetLastFilledOrder.Text = LoadingText(bGetLastFilledOrder.Text);
            try
            {
                Connect();
                dynamic order = await bs.GetFilledOrder(tbSymbol.Text + tbPair.Text);
                if (order != null)
                {
                    List<object> orderList = new List<object>();
                    orderList.Add(order);
                    dgvOrder.DataSource = orderList;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
            bGetLastFilledOrder.Text = LoadingText(bGetLastFilledOrder.Text);
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            openWeb("https://accounts.binance.com/es/register?ref=ZUJUQTMS");
        }

        private void bHelp_Click(object sender, EventArgs e)
        {
            openWeb("https://www.binance.com/es/support/faq/360002502072");
        }

        public void openWeb(string url)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        
        void ShowException(Exception ex)
        {
            MessageBox.Show("Error :" + ex.Message.ToString());
        }
    }
}
