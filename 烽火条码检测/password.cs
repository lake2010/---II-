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
    public partial class password : DevExpress.XtraEditors.XtraForm
    {
        public password()
        {
            InitializeComponent();
        }

        public string userpassword = "";
        private void simpleButton2_Click(object sender, EventArgs e)
        {            
            this.Close();            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textEdit1.Text != null && textEdit1.Text.Trim() != "" && textEdit2.Text != null && textEdit2.Text.Trim() != "" && textEdit3.Text != null && textEdit3.Text.Trim() != "")
                {
                    if (textEdit2.Text.Trim() == textEdit3.Text.Trim())
                    {
                        string ss = "";
                        if (userpassword == "")
                        {
                            userpassword = "1234567890";
                            MD5 md1 = new MD5CryptoServiceProvider();
                            byte[] result2 = md1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userpassword));
                            ss = System.BitConverter.ToString(result2).Replace("-", "");
                        }
                        else
                        {
                            ss = userpassword;
                        }
                        MD5 md = new MD5CryptoServiceProvider();
                        byte[] result = md.ComputeHash(System.Text.Encoding.UTF8.GetBytes(textEdit1.Text.Trim()));
                        string s = System.BitConverter.ToString(result).Replace("-", "");                        
                        if (ss == s)
                        {
                            Form1 xf = (Form1)this.Owner;                            
                            byte[] result1 = md.ComputeHash(System.Text.Encoding.UTF8.GetBytes(textEdit2.Text.Trim()));
                            string sss = System.BitConverter.ToString(result1).Replace("-", "");
                            xf.setPassword = sss;
                            this.Close();
                        }
                        else
                        {
                            XtraMessageBox.Show("原密码不正确！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("两次输入新密码不同！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("请输入原密码及新密码！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show("发生错误:\n\r" + ex.ToString(), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}