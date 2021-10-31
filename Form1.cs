using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WxMsgHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            WXMsg wx = new WXMsg();
            wx.Init();
            List<WxMsgInfo> info = wx.GetWxMsg();

            textBox1.Text = "";

            foreach(var tinfo in info) {
                string str = $"\r\n ============ \r\n User:{tinfo.name} \r\n Msg:{tinfo.value} \r\n ============ \r\n";
                textBox1.Text += str;


            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
