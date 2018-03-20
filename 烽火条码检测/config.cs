using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Security.Cryptography;

namespace 烽火条码检测
{
    public partial class config : DevExpress.XtraEditors.XtraForm
    {
        public config()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string pw = "";
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text.Trim() != "")
            {
                MD5 md = new MD5CryptoServiceProvider();
                byte[] result = md.ComputeHash(System.Text.Encoding.UTF8.GetBytes(textEdit1.Text.Trim()));
                string s = System.BitConverter.ToString(result).Replace("-", "");
                if (s == pw)
                {
                    Form1 xf = (Form1)this.Owner;
                    xf.qx = true;
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("密码错误", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}