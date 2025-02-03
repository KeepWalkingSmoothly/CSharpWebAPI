using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MesWebAPIBuiltIn.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static MesWebAPIBuiltIn.JsonData;
using System.Diagnostics;

namespace MesWebAPIBuiltIn
{
    public partial class Form1 : Form
    {
        // ----------------------------PLC使用--------------------------------
        private ActUtlTypeLib.ActUtlType AUTLO;
        private int OPLite;
        private bool PLC_Connect_Start = false;
        private bool PLC_Connect_Auto = false; //PLC自動連線
        private PublicFunction publicFunction = new PublicFunction();
        private Common.PLC_SV plc_sv = new Common.PLC_SV();
        // --------------------------Web通訊使用------------------------------
        private bool HOST_Connect_Start = false;
        private bool HOST_Connect_Auto = false;
        private string url;
        private string authorization = "自行輸入";
        private CheckPlaCode_Post checkPlaCode_Post;
        private SendWipOut_Post sendWipOut_Post;
        private bool host_tf = false; //判斷連線是否成功
        private int host_Connect_Count = 0;
        private string request;
        //private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        // -------------------------------------------------------------------

        public Form1()
        {
            AUTLO = new ActUtlTypeLib.ActUtlType();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (Settings.Default.URL != "")
                    URL.Text = Settings.Default.URL;
                PLC_Connect_Auto = Settings.Default.PLC_Cn;
                if (PLC_Connect_Auto)
                    PLC_Connect_Start = true;
                else
                    PLC_Connect_Start = false;
                if (PLC_Connect_Auto)
                    PLC_Auto_Connect.BackColor = Color.Aquamarine;
                else
                    PLC_Auto_Connect.BackColor = Color.LightCoral;
                HOST_Connect_Auto = Settings.Default.HOST_Cn;
                if (HOST_Connect_Auto)
                {
                    HOST_Connect_Start = true;
                    HOST_Connect.Start();
                }
                else
                    HOST_Connect_Start = false;
                if (HOST_Connect_Auto)
                    HOST_Auto_Connect.BackColor = Color.Aquamarine;
                else
                    HOST_Auto_Connect.BackColor = Color.LightCoral;
                Connect_Combo.SelectedIndex = Settings.Default.Combo;
            }
            catch(Exception ex)
            {
                MessageBox.Show("初始化异常: " + ex.ToString());
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            AUTLO.Close();
            Settings.Default.URL = URL.Text;
            Settings.Default.Combo = Connect_Combo.SelectedIndex;
            Settings.Default.PLC_Cn = PLC_Connect_Auto;
            Settings.Default.HOST_Cn = HOST_Connect_Auto;
            Settings.Default.Save();
        }

        private async void HOST_Connect_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                HOST_Connect.Stop();
                HOST_Connect_Start = false;
                HOST_Connect_Label.Text = "连线失败";
                HOST_Connect_Label.BackColor = Color.Red;
                MessageBox.Show("URL 不能为空，请输入有效地址。");
            }
            else if (HOST_Connect_Start && PLC_Connect_Start && host_tf)
            {
                if (plc_sv.W400 == 1) //基板碼校驗
                {
                    request = await MesWebApi(1);

                    if (!string.IsNullOrEmpty(request))
                    {
                        try
                        {
                            CheckPlaCode_Request response = JsonConvert.DeserializeObject<CheckPlaCode_Request>(request);

                            if (response != null && response.Code == 0)
                            {
                                // 轉換成功 通知PLC
                                AUTLO.WriteDeviceBlock("W300", 1, 1);
                            }
                            else if (response.Code != 0)
                            {
                                AUTLO.WriteDeviceBlock("W300", 1, 2);
                                string message = response.Message;
                                for (int i = 0; i < message.Length && i < 60; i++)
                                {
                                    int asciiValue = (int)message[i];
                                    string hexAddress = (784 + i).ToString("X"); // 将地址转换为 16 进制格式
                                    AUTLO.WriteDeviceBlock($"W{hexAddress}", 1, asciiValue);
                                }
                            }
                            else
                            {
                                MessageBox.Show("反序列化失败：" + response.ToString());
                            }
                        }
                        catch (JsonException ex)
                        {
                            MessageBox.Show("解析 JSON 出错：" + ex.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("未收到服务器响应");
                    }
                }

                if (plc_sv.W410 == 1) // 發送在製品出站
                {
                    request = await MesWebApi(2);

                    if (!string.IsNullOrEmpty(request))
                    {
                        try
                        {
                            SendWipOut_Request response = JsonConvert.DeserializeObject<SendWipOut_Request>(request);

                            if (response != null && response.Code == 0)
                            {
                                // 轉換成功 通知PLC
                                AUTLO.WriteDeviceBlock("W500", 1, 1);
                            }
                            else if (response.Code != 0)
                            {
                                AUTLO.WriteDeviceBlock("W500", 1, 2);
                                string message = response.Message;
                                for (int i = 0; i < message.Length && i < 60; i++)
                                {
                                    int asciiValue = (int)message[i];
                                    string hexAddress = (1296 + i).ToString("X"); // 将地址转换为 16 进制格式
                                    AUTLO.WriteDeviceBlock($"W{hexAddress}", 1, asciiValue);
                                }
                            }
                            else
                            {
                                MessageBox.Show("反序列化失败：" + response.ToString());
                            }
                        }
                        catch (JsonException ex)
                        {
                            MessageBox.Show("解析 JSON 出错：" + ex.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("未收到服务器响应");
                    }
                }
            }
            else if (HOST_Connect_Start && PLC_Connect_Start && !host_tf)
            {
                _ = await MesWebApi(0);

                if (host_Connect_Count <= 30) // 持續30秒
                {
                    host_Connect_Count += 1;
                }
                else
                {
                    host_Connect_Count = 0;
                    HOST_Connect_Start = false;
                    HOST_Connect_Label.Text = "连线失败";
                    HOST_Connect_Label.BackColor = Color.Red;
                    HOST_Connect.Stop();
                }
            }
            else if (HOST_Connect_Start)
            {
                HOST_Connect_Start = false;
                MessageBox.Show("PLC 尚未连线", "操作顺序错误");
                HOST_Connect.Stop();
            }
            else
            {
                HOST_Connect.Stop();
            }
        }

        private void PLC_Connect_Tick(object sender, EventArgs e)
        {
            if (PLC_Connect_Start)
            {
                MXComponent();
                _ = ReadPLCSV();
            }
        }

        #region API
        private async Task<string> MesWebApi(int post)
        {
            string result = "";
            if (post == 0 && !host_tf) //連線使用
            {
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    HOST_Connect.Stop();
                    HOST_Connect_Start = false;
                    HOST_Connect_Label.Text = "连线失败";
                    HOST_Connect_Label.BackColor = Color.Red;
                    MessageBox.Show("输入的 URL 格式无效，请检查后重试。");
                    return "";
                }

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization); //單純 Token 認證方式
                        HttpResponseMessage testResponse = await client.GetAsync(url);
                        //Console.WriteLine("StatusCode" + testResponse.StatusCode.ToString());

                        if (testResponse.IsSuccessStatusCode)
                        {
                            host_tf = true;
                            HOST_Connect_Label.Text = "已连线";
                            HOST_Connect_Label.BackColor = Color.GreenYellow;
                        }
                        else
                        {
                            string errorDetails = await testResponse.Content.ReadAsStringAsync();
                            Console.WriteLine($"傳輸失敗：{testResponse.StatusCode}，錯誤詳情：{errorDetails}");
                            return "";
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        return "";
                    }
                    catch (HttpRequestException)
                    {
                        HOST_Connect_Start = false;
                        HOST_Connect_Label.Text = "连线失败";
                        HOST_Connect_Label.BackColor = Color.Red;
                        HOST_Connect.Stop();
                        return "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("HOST 连线错误：" + ex.ToString());
                        HOST_Connect_Start = false;
                        HOST_Connect.Stop();
                        return "";
                    }
                }
            }
            else if (post == 1 && host_tf) //基板碼校驗
            {
                checkPlaCode_Post = new CheckPlaCode_Post
                {
                    StationCode = "",
                    LineCdoe = "YL01",
                    ProcessCode = "",
                    WipNo = plc_sv.W0_1D
                };

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization); //單純 Token 認證方式

                        string jsonPayload = JsonConvert.SerializeObject(checkPlaCode_Post);
                        HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync(url, content);
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseContent);

                        if (response.IsSuccessStatusCode)
                        {
                            result = await response.Content.ReadAsStringAsync();
                        }
                        else
                        {
                            MessageBox.Show("传输失败，状态码：" + response.StatusCode.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误：" + ex.ToString());
                    }
                }
            }
            else if (post == 2 && host_tf)  //發送在製品出站
            {
                DateTime dateTime = DateTime.Now;

                sendWipOut_Post = new SendWipOut_Post
                {
                    StationCode = "",
                    LineCode = "YL01",
                    ProcessCode = "",
                    WipNo = plc_sv.W0_1D,
                    EqptNo = plc_sv.W130_139,
                    EndTime = dateTime,
                    UserId = plc_sv.W130_139,
                    QaStatus = 0,
                    OnTrayQty = 1,

                    ResultDatas = new List<ResultDatas>
                    {
                        new ResultDatas
                        {
                            ItemName = "印刷设备参数",
                            ItemParamName = "印刷速度",
                            ItemParamValue = plc_sv.W110.ToString(),
                            ItemParamUnit = "0.1mm/sec",
                            ItemParamLimt = "100~7000",
                            ItemAppName = plc_sv.W100_10E,
                            ItemParamResult = "OK"
                        },
                        new ResultDatas
                        {
                            ItemName = "印刷设备参数",
                            ItemParamName = "回墨速度",
                            ItemParamValue = plc_sv.W111.ToString(),
                            ItemParamUnit = "0.1mm/sec",
                            ItemParamLimt = "100~7000",
                            ItemAppName = plc_sv.W100_10E,
                            ItemParamResult = "OK"
                        },
                        new ResultDatas
                        {
                            ItemName = "印刷设备参数",
                            ItemParamName = "印完延迟上",
                            ItemParamValue = plc_sv.W112.ToString(),
                            ItemParamUnit = "0.1sec",
                            ItemParamLimt = "0~999",
                            ItemAppName = plc_sv.W100_10E,
                            ItemParamResult = "OK"
                        },
                        new ResultDatas
                        {
                            ItemName = "印刷设备参数",
                            ItemParamName = "刮印次数",
                            ItemParamValue = plc_sv.W113.ToString(),
                            ItemParamUnit = "次",
                            ItemParamLimt = "1~10",
                            ItemAppName = plc_sv.W100_10E,
                            ItemParamResult = "OK",
                        },
                        new ResultDatas
                        {
                            ItemName = "印刷设备参数",
                            ItemParamName = "基板长",
                            ItemParamValue = plc_sv.W114.ToString(),
                            ItemParamUnit = "0.1mm",
                            ItemParamLimt = "3000~4000",
                            ItemAppName = plc_sv.W100_10E,
                            ItemParamResult = "OK"
                        },
                        new ResultDatas
                        {
                            ItemName = "印刷设备参数",
                            ItemParamName = "基板宽",
                            ItemParamValue = plc_sv.W115.ToString(),
                            ItemParamUnit = "0.1mm",
                            ItemParamLimt = "3000~4000",
                            ItemAppName = plc_sv.W100_10E,
                            ItemParamResult = "OK"
                        },
                        new ResultDatas
                        {
                            ItemName = "印刷设备参数",
                            ItemParamName = "基板治具厚度",
                            ItemParamValue = plc_sv.W116.ToString(),
                            ItemParamUnit = "0.1mm",
                            ItemParamLimt = "0~20",
                            ItemAppName = plc_sv.W100_10E,
                            ItemParamResult = "OK",
                        },
                        new ResultDatas
                        {
                            ItemName = "印刷设备参数",
                            ItemParamName = "需求驾空",
                            ItemParamValue = plc_sv.W117.ToString(),
                            ItemParamUnit = "0.1mm",
                            ItemParamLimt = "0~120",
                            ItemAppName = plc_sv.W100_10E,
                            ItemParamResult = "OK"
                        },
                        new ResultDatas
                        {
                            ItemName = "印刷设备参数",
                            ItemParamName = "印离板距离",
                            ItemParamValue = plc_sv.W118.ToString(),
                            ItemParamUnit = "0.1mm",
                            ItemParamLimt = "0~140",
                            ItemAppName = plc_sv.W100_10E,
                            ItemParamResult = "OK"
                        },
                        new ResultDatas
                        {
                            ItemName = "印刷设备参数",
                            ItemParamName = "左刮深度",
                            ItemParamValue = plc_sv.W119.ToString(),
                            ItemParamUnit = "0.1mm",
                            ItemParamLimt = "-99~99",
                            ItemAppName = plc_sv.W100_10E,
                            ItemParamResult = "OK"
                        },
                        new ResultDatas
                        {
                            ItemName = "印刷设备参数",
                            ItemParamName = "右刮深度",
                            ItemParamValue = plc_sv.W11A.ToString(),
                            ItemParamUnit = "0.1mm",
                            ItemParamLimt = "-99~99",
                            ItemAppName = plc_sv.W100_10E,
                            ItemParamResult = "OK"
                        },
                        new ResultDatas
                        {
                            ItemName = "印刷设备参数",
                            ItemParamName = "左墨深度",
                            ItemParamValue = plc_sv.W11B.ToString(),
                            ItemParamUnit = "0.1mm",
                            ItemParamLimt = "-99~99",
                            ItemAppName = plc_sv.W100_10E,
                            ItemParamResult = "OK"
                        },
                        new ResultDatas
                        {
                            ItemName = "印刷设备参数",
                            ItemParamName = "右墨深度",
                            ItemParamValue = plc_sv.W11C.ToString(),
                            ItemParamUnit = "0.1mm",
                            ItemParamLimt = "-99~99",
                            ItemAppName = plc_sv.W100_10E,
                            ItemParamResult = "OK"
                        },
                        new ResultDatas
                        {
                            ItemName = "印刷设备参数",
                            ItemParamName = "左刮刀压力",
                            ItemParamValue = plc_sv.W11D.ToString(),
                            ItemParamUnit = "Kg",
                            ItemParamLimt = "10~60",
                            ItemAppName = plc_sv.W100_10E,
                            ItemParamResult = "OK"
                        },
                        new ResultDatas
                        {
                            ItemName = "印刷设备参数",
                            ItemParamName = "右刮刀压力",
                            ItemParamValue = plc_sv.W11E.ToString(),
                            ItemParamUnit = "Kg",
                            ItemParamLimt = "10~60",
                            ItemAppName = plc_sv.W100_10E,
                            ItemParamResult = "OK"
                        }
                    }
                };

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization); //單純 Token 認證方式

                        string jsonPayload = JsonConvert.SerializeObject(sendWipOut_Post);
                        HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync(url, content);

                        if (response.IsSuccessStatusCode)
                        {
                            result = await response.Content.ReadAsStringAsync();
                        }
                        else
                        {
                            MessageBox.Show("传输失败，状态码：" + response.StatusCode.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误：" + ex.ToString());
                    }
                }
            }


            return result;
        }
        #endregion

        #region MX連線
        public void MXComponent()
        {
            try
            {
                AUTLO.Close();
                OPLite = AUTLO.Open();

                if (OPLite == 0)
                {
                    PLC_Connnect_Label.Text = "已连线";
                    PLC_Connnect_Label.BackColor = Color.GreenYellow;
                }
                else if(OPLite != 0)
                {
                    PLC_Connnect_Label.Text = "已断线";
                    PLC_Connnect_Label.BackColor = Color.Red;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("PLC 连线异常");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AUTLO.ActLogicalStationNumber = Connect_Combo.SelectedIndex + 1;
        }
        #endregion

        #region PLC參數
        public bool ReadPLCSV()
        {
            int nResult = 0;
            int nValue = 0;
            float fValue = 0;
            object objTemp;
            string result;

            short[] svPLC_W0_To_W1F = new short[32]; // Dec 0 to 31
            short[] svPLC_W100_To_W140 = new short[65]; //Dec 256 to 320
            short[] svPLC_W400_To_W419 = new short[26]; //Dec 1024 to 1049

            nResult = AUTLO.ReadDeviceBlock2("W0", 32, out svPLC_W0_To_W1F[0]);

            if (nResult == 0)
            {
                long[] wipNo = new long[30];
                for (int i = 0; i < 30; i++)
                {
                    wipNo[i] = svPLC_W0_To_W1F[i];
                }
                _ = publicFunction.StringDecodeFromPLC(wipNo, true, out result);
                plc_sv.W0_1D = result;
            }

            nResult = AUTLO.ReadDeviceBlock2("W100", 65, out svPLC_W100_To_W140[0]);

            if (nResult == 0)
            {
                long[] recipe_Name = new long[15];
                for (int i = 0; i < 15; i++)
                {
                    recipe_Name[i] = svPLC_W100_To_W140[i];
                }

                _ = publicFunction.StringDecodeFromPLC(recipe_Name, true, out result);
                plc_sv.W100_10E = result;

                fValue = (float)svPLC_W100_To_W140[16] / 10;
                objTemp = (object)fValue;
                plc_sv.W110 = (float)objTemp;

                fValue = (float)svPLC_W100_To_W140[17] / 10;
                objTemp = (object)fValue;
                plc_sv.W111 = (float)objTemp;

                fValue = (float)svPLC_W100_To_W140[18] / 10;
                objTemp = (object)fValue;
                plc_sv.W112 = (float)objTemp;

                nValue = (int)svPLC_W100_To_W140[19];
                objTemp = (object)nValue;
                plc_sv.W113 = (int)objTemp;

                fValue = (float)svPLC_W100_To_W140[20] / 10;
                objTemp = (object)fValue;
                plc_sv.W114 = (float)objTemp;

                fValue = (float)svPLC_W100_To_W140[21] / 10;
                objTemp = (object)fValue;
                plc_sv.W115 = (float)objTemp;

                fValue = (float)svPLC_W100_To_W140[22] / 10;
                objTemp = (object)fValue;
                plc_sv.W116 = (float)objTemp;

                fValue = (float)svPLC_W100_To_W140[23] / 10;
                objTemp = (object)fValue;
                plc_sv.W117 = (float)objTemp;

                fValue = (float)svPLC_W100_To_W140[24] / 10;
                objTemp = (object)fValue;
                plc_sv.W118 = (float)objTemp;

                fValue = (float)svPLC_W100_To_W140[25] / 10;
                objTemp = (object)fValue;
                plc_sv.W119 = (float)objTemp;

                fValue = (float)svPLC_W100_To_W140[26] / 10;
                objTemp = (object)fValue;
                plc_sv.W11A = (float)objTemp;

                fValue = (float)svPLC_W100_To_W140[27] / 10;
                objTemp = (object)fValue;
                plc_sv.W11B = (float)objTemp;

                fValue = (float)svPLC_W100_To_W140[28] / 10;
                objTemp = (object)fValue;
                plc_sv.W11C = (float)objTemp;

                nValue = (int)svPLC_W100_To_W140[29];
                objTemp = (object)nValue;
                plc_sv.W11D = (int)objTemp;

                nValue = (int)svPLC_W100_To_W140[30];
                objTemp = (object)nValue;
                plc_sv.W11E = (int)objTemp;

                long[] eqpNo = new long[10];

                for (int i = 0; i < 10; i++)
                {
                    eqpNo[i] = svPLC_W100_To_W140[i+48];
                }

                _ = publicFunction.StringDecodeFromPLC(eqpNo, true, out result);
                plc_sv.W130_139 = result;

            }
            else
            {
                return false;
            }

            nResult = AUTLO.ReadDeviceBlock2("W400", 26, out svPLC_W400_To_W419[0]);

            if (nResult == 0)
            {
                nValue = (int)svPLC_W400_To_W419[0];
                objTemp = (object)nValue;
                plc_sv.W400 = (int)objTemp;

                nValue = (int)svPLC_W400_To_W419[16];
                objTemp = (object)nValue;
                plc_sv.W410 = (int)objTemp;
            }
            
            return true;
        }
        #endregion

        #region Host控制
        private void HOST_Connect_Button_Click(object sender, EventArgs e)
        {
            HOST_Connect_Start = true;
            url = URL.Text;
            URL.Enabled = false;
            HOST_Connect_Label.Text = "连线中";
            HOST_Connect_Label.BackColor = Color.Yellow;
            host_Connect_Count = 0;
            HOST_Connect.Start();
        }

        private void HOST_Disconnect_Button_Click(object sender, EventArgs e)
        {
            //_cancellationTokenSource.Cancel();
            //_cancellationTokenSource = new CancellationTokenSource();
            HOST_Connect_Start = false;
            HOST_Connect_Label.Text = "已断线";
            HOST_Connect_Label.BackColor = Color.Red;
            URL.Enabled = true;
            host_tf = false;
            Settings.Default.URL = URL.Text;
            Settings.Default.Save();
        }

        private void HOST_Auto_Connect_Click(object sender, EventArgs e)
        {
            if (HOST_Connect_Auto == false)
            {
                Settings.Default.HOST_Cn = true;
                HOST_Auto_Connect.BackColor = Color.Aquamarine;
                HOST_Connect_Auto = true;
                Settings.Default.Save();
            }
            else
            {
                Settings.Default.HOST_Cn = false;
                HOST_Auto_Connect.BackColor = Color.LightCoral;
                HOST_Connect_Auto = false;
                Settings.Default.Save();
            }
        }
        #endregion

        #region PLC控制按鈕
        private void PLC_Connect_Button_Click(object sender, EventArgs e)
        {
            PLC_Connect_Start = true;
        }

        private void PLC_Disconnect_Button_Click(object sender, EventArgs e)
        {
            PLC_Connect_Start = false;
            PLC_Connnect_Label.Text = "已断线";
            PLC_Connnect_Label.BackColor = Color.Red;
            AUTLO.Close();
            Settings.Default.Combo = Connect_Combo.SelectedIndex;
            Settings.Default.Save();
        }

        private void PLC_Auto_Connect_Click(object sender, EventArgs e)
        {
            if (PLC_Connect_Auto == false)
            {
                Settings.Default.PLC_Cn = true;
                PLC_Auto_Connect.BackColor = Color.Aquamarine;
                PLC_Connect_Auto = true;
                Settings.Default.Save();
            }
            else
            {
                Settings.Default.PLC_Cn = false;
                PLC_Auto_Connect.BackColor = Color.LightCoral;
                PLC_Connect_Auto = false;
                Settings.Default.Save();
            }
        }
        #endregion
    }
}
