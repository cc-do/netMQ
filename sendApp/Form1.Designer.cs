namespace sendApp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txt_content = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.lbx_sendList = new System.Windows.Forms.ListBox();
            this.btn_time = new System.Windows.Forms.Button();
            this.btn_stopSend = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menu_Notify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem_Show = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Notify.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_content
            // 
            this.txt_content.Location = new System.Drawing.Point(33, 24);
            this.txt_content.Name = "txt_content";
            this.txt_content.Size = new System.Drawing.Size(169, 21);
            this.txt_content.TabIndex = 0;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(220, 22);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 1;
            this.btn_send.Text = "发送数据";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // lbx_sendList
            // 
            this.lbx_sendList.FormattingEnabled = true;
            this.lbx_sendList.ItemHeight = 12;
            this.lbx_sendList.Location = new System.Drawing.Point(12, 69);
            this.lbx_sendList.Name = "lbx_sendList";
            this.lbx_sendList.Size = new System.Drawing.Size(585, 148);
            this.lbx_sendList.TabIndex = 2;
            // 
            // btn_time
            // 
            this.btn_time.Location = new System.Drawing.Point(301, 22);
            this.btn_time.Name = "btn_time";
            this.btn_time.Size = new System.Drawing.Size(94, 23);
            this.btn_time.TabIndex = 3;
            this.btn_time.Text = "定时发送数据";
            this.btn_time.UseVisualStyleBackColor = true;
            this.btn_time.Click += new System.EventHandler(this.btn_time_Click);
            // 
            // btn_stopSend
            // 
            this.btn_stopSend.Location = new System.Drawing.Point(401, 22);
            this.btn_stopSend.Name = "btn_stopSend";
            this.btn_stopSend.Size = new System.Drawing.Size(75, 23);
            this.btn_stopSend.TabIndex = 4;
            this.btn_stopSend.Text = "停止发送";
            this.btn_stopSend.UseVisualStyleBackColor = true;
            this.btn_stopSend.Click += new System.EventHandler(this.btn_stopSend_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(482, 22);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 5;
            this.btn_clear.Text = "清空窗口";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.menu_Notify;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "数据发送端";
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // menu_Notify
            // 
            this.menu_Notify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Show,
            this.menuItem_Exit});
            this.menu_Notify.Name = "menu_Notify";
            this.menu_Notify.Size = new System.Drawing.Size(125, 48);
            // 
            // menuItem_Show
            // 
            this.menuItem_Show.Name = "menuItem_Show";
            this.menuItem_Show.Size = new System.Drawing.Size(180, 22);
            this.menuItem_Show.Text = "显示窗口";
            this.menuItem_Show.Click += new System.EventHandler(this.menuItem_Show_Click);
            // 
            // menuItem_Exit
            // 
            this.menuItem_Exit.Name = "menuItem_Exit";
            this.menuItem_Exit.Size = new System.Drawing.Size(180, 22);
            this.menuItem_Exit.Text = "退出程序";
            this.menuItem_Exit.Click += new System.EventHandler(this.menuItem_Exit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 233);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_stopSend);
            this.Controls.Add(this.btn_time);
            this.Controls.Add(this.lbx_sendList);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.txt_content);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "数据发送端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.menu_Notify.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_content;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.ListBox lbx_sendList;
        private System.Windows.Forms.Button btn_time;
        private System.Windows.Forms.Button btn_stopSend;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip menu_Notify;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Show;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Exit;
    }
}

