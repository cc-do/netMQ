using myNetMq;
using System.Windows.Forms;
using System.Configuration;
using System;
using System.Threading;

namespace sendApp
{
    public partial class Form1 : Form
    {
        private MyPublisher _myPublisher = null;
        private System.Timers.Timer timer;
        private string sendAddr;
        private string sendHost;
        private int timeSpan;
        /// <summary>
        /// 初始化
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            sendAddr = ConfigurationManager.AppSettings["sendAddr"];
            sendHost = ConfigurationManager.AppSettings["sendHost"];
            timeSpan = Int32.Parse(ConfigurationManager.AppSettings["timeSpan"]);
            _myPublisher = new MyPublisher(sendAddr);
        }
        /// <summary>
        /// 发送输入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_send_Click(object sender, System.EventArgs e)
        {
            string record = String.Format("{0}\t{1}", System.DateTime.Now, this.txt_content.Text.Trim());
            _myPublisher.Publish<string>(sendHost, record);
            this.lbx_sendList.Items.Add(record);
        }
        /// <summary>
        /// 定时执行的函数，可间隔一段时间执行，也可在指定的时间执行
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void timeOutToDo(object source, System.Timers.ElapsedEventArgs e)
        {
            Random random = new Random();
            string record = String.Format("{0}\t{1}", System.DateTime.Now, random.Next());
            _myPublisher.Publish<string>(sendHost, record);
            this.lbx_sendList.Items.Add(record);

            //如果当前时间是10点30分，此时时间间隔要设置为1分钟
            //timer.Interval = 60000;  //执行间隔时间,单位为毫秒;此时时间间隔为1分钟  
            //if (DateTime.Now.Hour == 10 && DateTime.Now.Minute == 30)
            //    Console.WriteLine("OK, event fired at: " + DateTime.Now.ToString());
        }
        /// <summary>
        /// 开启定时运行的线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_time_Click(object sender, EventArgs e)
        {
            timer = new System.Timers.Timer(timeSpan);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timeOutToDo); //到达时间的时候执行事件；   
            timer.AutoReset = true;   //设置是执行一次（false）还是一直执行(true)；   
            timer.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件； 
        }
        /// <summary>
        /// 关闭定时执行的线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_stopSend_Click(object sender, EventArgs e)
        {
            timer.Stop();
            this.lbx_sendList.Items.Add("已关闭定时发送！！");
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            this.lbx_sendList.Items.Clear();
        }
        /// <summary>
        /// 最小化隐藏窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                HideMainForm();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            HideMainForm();
        }
        /// <summary>
        /// 双击显示窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ShowMainForm();
        }
        /// <summary>
        /// 退出程序确认
        /// </summary>
        private void ExitMainForm()
        {
            if (MessageBox.Show("您确定要退出数据发送端吗？", "确认退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                this.notifyIcon1.Visible = false;
                this.Close();
                this.Dispose();
                Application.Exit();
            }
        }
        /// <summary>
        /// 窗口显示函数
        /// </summary>
        private void ShowMainForm()
        {
            this.Visible = true;

            this.WindowState = FormWindowState.Normal;

            this.notifyIcon1.Visible = false;
        }
        /// <summary>
        /// 隐藏窗口函数
        /// </summary>
        private void HideMainForm()
        {
            this.Hide();
            this.notifyIcon1.Visible = true;
        }

        private void menuItem_Show_Click(object sender, EventArgs e)
        {
            ShowMainForm();
        }

        private void menuItem_Exit_Click(object sender, EventArgs e)
        {
            ExitMainForm();
        }
    }
}
