using myNetMq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace receiveApp
{
    public partial class Form1 : Form
    {
        private MySubscriber _mySubscriber = null;
        private string receiveAddr;
        public Form1()
        {
            InitializeComponent();
            receiveAddr = ConfigurationManager.AppSettings["receiveAddr"];
            _mySubscriber = new MySubscriber(receiveAddr);
            _mySubscriber.Subscriber<string>();
            _mySubscriber.Notify += OnmySubscriber_Notify;

        }

        private void OnmySubscriber_Notify(string arg1, object arg2)
        {
            //this.lbx_receiveContent.Dispatcher.BeginInvoke((Action)(() => this.lbx_receiveContent.Items.Add(arg2)));
            this.lbx_receiveContent.Items.Add(arg1 + " 发布：" + arg2);
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            this.lbx_receiveContent.Items.Clear();
        }
    }
}
