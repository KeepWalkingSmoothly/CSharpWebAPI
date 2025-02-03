using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MesWebAPIBuiltIn.JsonData;

namespace MesWebAPIBuiltIn
{
    public class PublicFunction
    {
        #region ASCII 轉碼
        public int StringDecodeFromPLC(long[] DecValue, Boolean PLC_From_HighToLow, out string Result_Final)
        {
            int Rtn;
            string I_Result_Final = "";
            int I_ArrSize = DecValue.Length;
            for (int i = 0; i < I_ArrSize; i++)
            {
                Rtn = LongToASCII(DecValue[i], PLC_From_HighToLow, out string Result_Single);

                if(Rtn == 0)
                {
                    I_Result_Final = I_Result_Final + Result_Single;
                }
                else
                {

                }
            }
            Result_Final = I_Result_Final.Trim();
            return 0;
        }

        public int LongToASCII(long DecValue, Boolean PLC_From_HighToLow, out string Result)
        {
            long high = DecValue / 256;
            long low = DecValue % 256;

            if(((low < 32) || (low > 126)) && ((high < 32) || (high > 126)))
            {
                Result = "";
                return -1;
            }

            Result = "";

            if(PLC_From_HighToLow == true) // 反轉
            {
                if ((low >= 32) && (low <= 126))
                {
                    Result = Convert.ToString((char)low);
                }
                
                if ((high >= 32) && (high <= 126))
                {
                    Result = Result + Convert.ToString((char)high);
                }
            }
            else
            {
                if ((high >= 32) && (high <= 126))
                {
                    Result = Convert.ToString((char)high);
                }
                
                if ((low >= 32) && (low <= 126))
                {
                    Result = Result + Convert.ToString((char)low);
                }
            }

            return 0;
        }
        #endregion
    }
}
