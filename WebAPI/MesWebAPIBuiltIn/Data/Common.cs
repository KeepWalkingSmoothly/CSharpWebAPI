using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesWebAPIBuiltIn
{
    public class Common
    {
        public struct PLC_SV
        {
            public string W0_1D;        //WipNo 基板碼
            public string W100_10E;     //Recipe Name
            public float W110;          //印刷速度 0.1mm/sec 100~7000
            public float W111;          //回墨速度 0.1mm/sec 100~7000
            public float W112;          //印完延遲刮上 0.1sec 0~999
            public int W113;            //刮印次數 次 1~10
            public float W114;          //基板長 0.1mm 3000~4000
            public float W115;          //基板寬 0.1mm 3000~4000
            public float W116;          //基板治具厚度 0.1mm 0~20
            public float W117;          //需求駕空 0.1mm 0~120
            public float W118;          //印離板距離 0.1mm 0~140
            public float W119;          //左刮深度 0.1mm -99~99
            public float W11A;          //右刮深度 0.1mm -99~99
            public float W11B;          //左墨深度 0.1mm -99~99
            public float W11C;          //右墨深度 0.1mm -99~99
            public int W11D;            //左刮刀壓力 Kg 10~60
            public int W11E;            //右刮刀壓力 Kg 10~60
            public string W130_139;     //設備編號

            public int W400;            //PLC通知 1=通知 基板校驗碼
            public int W410;            //PLC通知 1=通知 參數
            public int W500;            //PC回覆  1=OK 2=NG
            public string W510_54B;     //Message NG訊息

            public int W400_old;
            public int W410_old;
        }
    }
}
