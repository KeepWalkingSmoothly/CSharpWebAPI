using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesWebAPIBuiltIn
{
    public class JsonData
    {
        public class CheckPlaCode_Post
        {
            public string StationCode { get; set; }
            public string LineCdoe { get; set; } // YL01
            public string ProcessCode { get; set; }
            public string WipNo { get; set; } // W0_1D
        }

        public class CheckPlaCode_Request
        {
            public int Code { get; set; } // 0=成功 其餘失敗
            public string Message { get; set; } // 成功=空白
            public string Data { get; set; }
        }

        public class SendWipOut_Post
        {
            public string StationCode { get; set; }
            public string LineCode { get; set; } // YL01
            public string ProcessCode { get; set; }
            public string WipNo { get; set; } // W0_1D
            public string EqptNo { get; set; } // W130_139
            public DateTime EndTime { get; set; } // DateTime.now
            public string UserId { get; set; } // W130_139
            public int QaStatus { get; set; }
            public int OnTrayQty { get; set; } // 1
            public List<ResultDatas> ResultDatas { get; set; }
        } 

        public class SendWipOut_Request
        {
            public int Code { get; set; } // 0=成功 其餘失敗
            public string Message { get; set; }
            public string Data { get; set; }
        }

        public class ResultDatas
        {
            public string ItemName { get; set; } // 固定"印刷設備參數"
            public string ItemParamName { get; set; } // sv name
            public string ItemParamValue { get; set; } // sv
            public string ItemParamUnit { get; set; } // 單位
            public string ItemParamLimt { get; set; } // 上下限
            public DateTime ItemParamTime { get; set; }
            public string ItemParamCond { get; set; }
            public string ItemAppName { get; set; } // W100_10E
            public string ItemParamResult { get; set; } // 固定"OK"
            public string ItemParamResultDec { get; set; }
            public Decimal ItemParamLimtDown { get; set; }
            public Decimal ItemParamLimtUp { get; set; }
            public string ChidWipNo { get; set; }
        }
    }
}
