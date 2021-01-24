using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYSEPro
{
    class jsonResult
    {
            public DateTime date { get; set; }
            public string StockSymbol { get; set; }
            public float StockPriceOpen { get; set; }
            public float StockPriceClose { get; set; }
            public float StockPriceIo { get; set; }
            public float StockPriceHigh { get; set; }
            public float StockPriceAdjClose { get; set; }
            public int StockVolume { get; set; }
            public string StockExchange { get; set; }

        
}
}
