namespace MesWebAPIBuiltIn
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.URL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.HOST_Connect_Label = new System.Windows.Forms.Label();
            this.PLC_Connnect_Label = new System.Windows.Forms.Label();
            this.HOST_Connect_Button = new System.Windows.Forms.Button();
            this.PLC_Connect_Button = new System.Windows.Forms.Button();
            this.HOST_Auto_Connect = new System.Windows.Forms.Button();
            this.PLC_Auto_Connect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Connect_Combo = new System.Windows.Forms.ComboBox();
            this.HOST_Disconnect_Button = new System.Windows.Forms.Button();
            this.PLC_Disconnect_Button = new System.Windows.Forms.Button();
            this.PLC_Connect = new System.Windows.Forms.Timer(this.components);
            this.HOST_Connect = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(177, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "网址";
            // 
            // URL
            // 
            this.URL.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.URL.Location = new System.Drawing.Point(241, 24);
            this.URL.Name = "URL";
            this.URL.Size = new System.Drawing.Size(662, 36);
            this.URL.TabIndex = 1;
            this.URL.Text = "http://192.168.1.60:8080/Service/ApiService";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(37, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 40);
            this.label2.TabIndex = 2;
            this.label2.Text = "HOST";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(50, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 40);
            this.label3.TabIndex = 2;
            this.label3.Text = "PLC";
            // 
            // HOST_Connect_Label
            // 
            this.HOST_Connect_Label.BackColor = System.Drawing.Color.Red;
            this.HOST_Connect_Label.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.HOST_Connect_Label.Location = new System.Drawing.Point(25, 81);
            this.HOST_Connect_Label.Name = "HOST_Connect_Label";
            this.HOST_Connect_Label.Size = new System.Drawing.Size(220, 75);
            this.HOST_Connect_Label.TabIndex = 3;
            this.HOST_Connect_Label.Text = "未连线";
            this.HOST_Connect_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PLC_Connnect_Label
            // 
            this.PLC_Connnect_Label.BackColor = System.Drawing.Color.Red;
            this.PLC_Connnect_Label.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.PLC_Connnect_Label.Location = new System.Drawing.Point(25, 252);
            this.PLC_Connnect_Label.Name = "PLC_Connnect_Label";
            this.PLC_Connnect_Label.Size = new System.Drawing.Size(220, 75);
            this.PLC_Connnect_Label.TabIndex = 3;
            this.PLC_Connnect_Label.Text = "未连线";
            this.PLC_Connnect_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HOST_Connect_Button
            // 
            this.HOST_Connect_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.HOST_Connect_Button.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.HOST_Connect_Button.Location = new System.Drawing.Point(271, 81);
            this.HOST_Connect_Button.Name = "HOST_Connect_Button";
            this.HOST_Connect_Button.Size = new System.Drawing.Size(142, 75);
            this.HOST_Connect_Button.TabIndex = 4;
            this.HOST_Connect_Button.Text = "开始连线";
            this.HOST_Connect_Button.UseVisualStyleBackColor = false;
            this.HOST_Connect_Button.Click += new System.EventHandler(this.HOST_Connect_Button_Click);
            // 
            // PLC_Connect_Button
            // 
            this.PLC_Connect_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PLC_Connect_Button.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.PLC_Connect_Button.Location = new System.Drawing.Point(271, 252);
            this.PLC_Connect_Button.Name = "PLC_Connect_Button";
            this.PLC_Connect_Button.Size = new System.Drawing.Size(142, 75);
            this.PLC_Connect_Button.TabIndex = 4;
            this.PLC_Connect_Button.Text = "开始连线";
            this.PLC_Connect_Button.UseVisualStyleBackColor = false;
            this.PLC_Connect_Button.Click += new System.EventHandler(this.PLC_Connect_Button_Click);
            // 
            // HOST_Auto_Connect
            // 
            this.HOST_Auto_Connect.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.HOST_Auto_Connect.Location = new System.Drawing.Point(584, 81);
            this.HOST_Auto_Connect.Name = "HOST_Auto_Connect";
            this.HOST_Auto_Connect.Size = new System.Drawing.Size(142, 75);
            this.HOST_Auto_Connect.TabIndex = 4;
            this.HOST_Auto_Connect.Text = "自动连线";
            this.HOST_Auto_Connect.UseVisualStyleBackColor = true;
            this.HOST_Auto_Connect.Click += new System.EventHandler(this.HOST_Auto_Connect_Click);
            // 
            // PLC_Auto_Connect
            // 
            this.PLC_Auto_Connect.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.PLC_Auto_Connect.Location = new System.Drawing.Point(584, 252);
            this.PLC_Auto_Connect.Name = "PLC_Auto_Connect";
            this.PLC_Auto_Connect.Size = new System.Drawing.Size(142, 75);
            this.PLC_Auto_Connect.TabIndex = 4;
            this.PLC_Auto_Connect.Text = "自动连线";
            this.PLC_Auto_Connect.UseVisualStyleBackColor = true;
            this.PLC_Auto_Connect.Click += new System.EventHandler(this.PLC_Auto_Connect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(154, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 28);
            this.label4.TabIndex = 5;
            this.label4.Text = "连线模式";
            // 
            // Connect_Combo
            // 
            this.Connect_Combo.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Connect_Combo.FormattingEnabled = true;
            this.Connect_Combo.Items.AddRange(new object[] {
            "1",
            "2"});
            this.Connect_Combo.Location = new System.Drawing.Point(282, 190);
            this.Connect_Combo.Name = "Connect_Combo";
            this.Connect_Combo.Size = new System.Drawing.Size(91, 32);
            this.Connect_Combo.TabIndex = 6;
            this.Connect_Combo.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // HOST_Disconnect_Button
            // 
            this.HOST_Disconnect_Button.BackColor = System.Drawing.Color.LightCoral;
            this.HOST_Disconnect_Button.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.HOST_Disconnect_Button.Location = new System.Drawing.Point(429, 81);
            this.HOST_Disconnect_Button.Name = "HOST_Disconnect_Button";
            this.HOST_Disconnect_Button.Size = new System.Drawing.Size(142, 75);
            this.HOST_Disconnect_Button.TabIndex = 4;
            this.HOST_Disconnect_Button.Text = "中断连线";
            this.HOST_Disconnect_Button.UseVisualStyleBackColor = false;
            this.HOST_Disconnect_Button.Click += new System.EventHandler(this.HOST_Disconnect_Button_Click);
            // 
            // PLC_Disconnect_Button
            // 
            this.PLC_Disconnect_Button.BackColor = System.Drawing.Color.LightCoral;
            this.PLC_Disconnect_Button.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.PLC_Disconnect_Button.Location = new System.Drawing.Point(429, 252);
            this.PLC_Disconnect_Button.Name = "PLC_Disconnect_Button";
            this.PLC_Disconnect_Button.Size = new System.Drawing.Size(142, 75);
            this.PLC_Disconnect_Button.TabIndex = 4;
            this.PLC_Disconnect_Button.Text = "中断连线";
            this.PLC_Disconnect_Button.UseVisualStyleBackColor = false;
            this.PLC_Disconnect_Button.Click += new System.EventHandler(this.PLC_Disconnect_Button_Click);
            // 
            // PLC_Connect
            // 
            this.PLC_Connect.Enabled = true;
            this.PLC_Connect.Tick += new System.EventHandler(this.PLC_Connect_Tick);
            // 
            // HOST_Connect
            // 
            this.HOST_Connect.Interval = 1000;
            this.HOST_Connect.Tick += new System.EventHandler(this.HOST_Connect_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 644);
            this.Controls.Add(this.Connect_Combo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PLC_Auto_Connect);
            this.Controls.Add(this.HOST_Auto_Connect);
            this.Controls.Add(this.PLC_Connect_Button);
            this.Controls.Add(this.PLC_Disconnect_Button);
            this.Controls.Add(this.HOST_Disconnect_Button);
            this.Controls.Add(this.HOST_Connect_Button);
            this.Controls.Add(this.PLC_Connnect_Label);
            this.Controls.Add(this.HOST_Connect_Label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.URL);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox URL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label HOST_Connect_Label;
        private System.Windows.Forms.Label PLC_Connnect_Label;
        private System.Windows.Forms.Button HOST_Connect_Button;
        private System.Windows.Forms.Button PLC_Connect_Button;
        private System.Windows.Forms.Button HOST_Auto_Connect;
        private System.Windows.Forms.Button PLC_Auto_Connect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Connect_Combo;
        private System.Windows.Forms.Button HOST_Disconnect_Button;
        private System.Windows.Forms.Button PLC_Disconnect_Button;
        private System.Windows.Forms.Timer PLC_Connect;
        private System.Windows.Forms.Timer HOST_Connect;
    }
}

