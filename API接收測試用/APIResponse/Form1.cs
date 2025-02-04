using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Diagnostics;
using Newtonsoft.Json;

namespace APIResponse
{
    public partial class Form1 : Form
    {
        private const string AuthorizationKey = "09a2815ab6814ea3a87166d2bf866085";
        private HttpListener listener;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void start_response_Click(object sender, EventArgs e)
        {
            try
            {
                listener = new HttpListener();
                listener.Prefixes.Add("http://+:8070/");
                listener.Start();
                listener.BeginGetContext(OnRequestReceived, null);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void stop_response_Click(object sender, EventArgs e)
        {
            if(listener != null && listener.IsListening)
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

                string authHeader = request.Headers["Authorization"];
                Debug.WriteLine(authHeader);

                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    RespondWithJson(context, 401, "授權失敗：缺少或無效了Token");
                    return;
                }

                string token = authHeader.Substring("Bearer ".Length).Trim();

                // 验证 Token
                if (!ValidateToken(token))
                {
                    RespondWithJson(context, 401, "授權失敗：Token 無效或已過期");
                    return;
                }
                else
                {
                    //Console.WriteLine()
                    RespondWithJson(context, 200, "連線成功");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void RespondWithJson(HttpListenerContext context, int statusCode, string message)
        {
            var response = context.Response;

            var jsonResponse = new Dictionary<string, object>//測試用
            {
                { "Code", "1" },
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
                Debug.WriteLine("Token 驗證成功");
                return true;
            }

            Debug.WriteLine("Token 驗證失敗");
            return false;
        }
    }
}
