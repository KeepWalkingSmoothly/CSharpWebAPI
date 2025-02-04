using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using static MesWebAPITest.JsonData;
using Newtonsoft.Json.Linq;

namespace MesWebAPITest
{
    public partial class Form1 : Form
    {
        private HttpListener listener;
        private CheckPlaCode_Post checkPlaCode_Post;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Start_Click(object sender, EventArgs e)
        {
            try
            {
                listener = new HttpListener();
                listener.Prefixes.Add("http://+:8070/");
                listener.Start();
                listener.BeginGetContext(OnRequestReceived, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            if (listener != null && listener.IsListening)
            {
                listener.Stop();
                listener.Close();
            }
        }

        private void OnRequestReceived(IAsyncResult result)
        {
            if (listener == null || !listener.IsListening)
                return;

            try
            {
                var context = listener.EndGetContext(result);
                listener.BeginGetContext(OnRequestReceived, null);
                var request = context.Request;

                // **輸出請求基本資訊**
                Console.WriteLine($"[請求] {request.HttpMethod} {request.Url}");

                // **讀取 Authorization 標頭**
                string authHeader = request.Headers["Authorization"];
                Console.WriteLine($"[Authorization] {authHeader}");

                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    RespondWithJson(context, 401, "授權失敗：缺少或無效的 Token");
                    return;
                }

                string token = authHeader.Substring("Bearer ".Length).Trim();

                // **驗證 Token**
                if (!ValidateToken(token))
                {
                    RespondWithJson(context, 401, "授權失敗：Token 無效或已過期");
                    return;
                }

                // **如果是 GET 或 沒有 Body 的 POST，直接回傳成功**
                if (request.HttpMethod == "GET" || !request.HasEntityBody)
                {
                    RespondWithJson(context, 200, "連線成功");
                    return;
                }

                // **處理 POST 且有 Body 的請求**
                if (request.HttpMethod == "POST" && request.HasEntityBody)
                {
                    using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string body = reader.ReadToEnd(); // 讀取請求內容
                        Console.WriteLine($"[正文] {body}");

                        // **解析 JSON**
                        try
                        {
                            var receivedData = JsonConvert.DeserializeObject<JObject>(body);
                            Console.WriteLine($"[解析後]\n {receivedData}");

                            // 這裡可以進一步處理 `receivedData`

                            RespondWithJson(context, 200, "數據接收成功");
                        }
                        catch (Exception jsonEx)
                        {
                            Console.WriteLine($"[JSON 解析錯誤] {jsonEx}");
                            RespondWithJson(context, 400, "JSON 格式錯誤");
                        }
                    }
                }
                else
                {
                    RespondWithJson(context, 405, "只支援 GET / POST 請求");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[錯誤] {ex}");
                MessageBox.Show(ex.ToString());
            }
        }

        private void RespondWithJson(HttpListenerContext context, int statusCode, string message)
        {
            var response = context.Response;

            var jsonResponse = new Dictionary<string, object>//測試用
            {
                { "Code", "0" },
                { "Message", ""},
                { "Data", "" }
            };

            string jsonString = JsonConvert.SerializeObject(jsonResponse, Formatting.Indented);
            byte[] buffer = Encoding.UTF8.GetBytes(jsonString);
            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            response.StatusCode = statusCode;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.Close();
        }

        private bool ValidateToken(string token)
        {
            // 預設的正確 Token（這裡應該是你的伺服器設定的 Token）
            string validToken = "09a2815ab6814ea3a87166d2bf866085";

            // 直接比對收到的 Token
            if (token == validToken)
            {
                Console.WriteLine("Token 驗證成功");
                return true;
            }

            Console.WriteLine("Token 驗證失敗");
            return false;
        }
    }
}
