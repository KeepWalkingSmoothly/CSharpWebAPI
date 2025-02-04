namespace APIResponse
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.start_response = new System.Windows.Forms.Button();
            this.stop_response = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // start_response
            // 
            this.start_response.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.start_response.Location = new System.Drawing.Point(27, 35);
            this.start_response.Name = "start_response";
            this.start_response.Size = new System.Drawing.Size(136, 60);
            this.start_response.TabIndex = 0;
            this.start_response.Text = "Start";
            this.start_response.UseVisualStyleBackColor = true;
            this.start_response.Click += new System.EventHandler(this.start_response_Click);
            // 
            // stop_response
            // 
            this.stop_response.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stop_response.Location = new System.Drawing.Point(27, 129);
            this.stop_response.Name = "stop_response";
            this.stop_response.Size = new System.Drawing.Size(136, 60);
            this.stop_response.TabIndex = 0;
            this.stop_response.Text = "Stop";
            this.stop_response.UseVisualStyleBackColor = true;
            this.stop_response.Click += new System.EventHandler(this.stop_response_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.stop_response);
            this.Controls.Add(this.start_response);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button start_response;
        private System.Windows.Forms.Button stop_response;
    }
}

