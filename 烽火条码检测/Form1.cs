using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors;

namespace 烽火条码检测
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        //读取的打印设置
        static string PrintChange = "";
        static string PrintText = "";

        int checknum = 0;
        int sum = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            barHeaderItem1.Caption = "时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            barHeaderItem2.Caption = "校验数量：" + sum.ToString();
        }

        public bool qx = false;
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (userpassword != "")
                {
                    config xf = new config();
                    xf.Owner = this;
                    xf.pw = userpassword;
                    this.Hide();
                    xf.ShowDialog();
                    xf.Dispose();
                    this.Show();
                    if (qx)
                    {
                        checkEdit1.Enabled = true;
                        checkEdit2.Enabled = true;
                        checkEdit3.Enabled = true;
                        checkEdit4.Enabled = true;
                        checkEdit5.Enabled = true;
                        checkEdit6.Enabled = true;
                        checkEdit7.Enabled = true;
                        checkEdit8.Enabled = true;
                        checkEdit9.Enabled = true;
                    }
                }
                else
                {
                    XtraMessageBox.Show("请先设置密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show("发生错误:\n\r" + ex.ToString(), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        string userpassword = "";
        int SheZhi = 11111111;
        private void Form1_Load(object sender, EventArgs e)
        {
            //FastReport环境变量设置（打印时不提示 "正在准备../正在打印..",一个程序只需设定一次，故一般写在程序入口）
            (new FastReport.EnvironmentSettings()).ReportSettings.ShowProgress = false;

            try
            {
                FileStream fs = new FileStream("load.ini", FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                userpassword = br.ReadString();
                SheZhi = br.ReadInt32();
                br.Close();
                fs.Close();
                br.Dispose();
                fs.Dispose();
                loadcheck();
            }
            catch
            {
            }

            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Config.ini"))
            {
                MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory + "Config.ini" + "文件不存在");
                return;
            }

            //读取配置文件，选择打印方式
            string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Config.ini", System.Text.Encoding.GetEncoding("GB2312"));

            foreach (string line in lines)
            {
                if (line.Contains("PrintChange"))
                {
                    PrintChange = line.Substring(line.IndexOf("=") + 1).Trim();
                }
            }

            simpleButton2.Visible = false;
            simpleButton3.Visible = false;
        }

        //初始化复选框
        private void loadcheck()
        {
            try
            {
                if (SheZhi != 11111111)
                {
                    int b = SheZhi % 10;
                    int a = SheZhi / 10;
                    if (b == 0)
                    {
                        textEdit2.Enabled = false;
                        checkEdit1.Checked = false;
                    }
                    b = a % 10;
                    a = a / 10;
                    if (b == 0)
                    {
                        textEdit3.Enabled = false;
                        checkEdit2.Checked = false;
                    }
                    b = a % 10;
                    a = a / 10;
                    if (b == 0)
                    {
                        textEdit4.Enabled = false;
                        checkEdit3.Checked = false;
                    }
                    b = a % 10;
                    a = a / 10;
                    if (b == 0)
                    {
                        textEdit5.Enabled = false;
                        checkEdit4.Checked = false;
                    }
                    b = a % 10;
                    a = a / 10;
                    if (b == 0)
                    {
                        textEdit6.Enabled = false;
                        checkEdit5.Checked = false;
                    }
                    b = a % 10;
                    a = a / 10;
                    if (b == 0)
                    {
                        textEdit7.Enabled = false;
                        checkEdit6.Checked = false;
                    }
                    b = a % 10;
                    a = a / 10;
                    if (b == 0)
                    {
                        textEdit8.Enabled = false;
                        checkEdit7.Checked = false;
                    }
                    b = a % 10;
                    a = a / 10;
                    if (b == 0)
                    {
                        textEdit9.Enabled = false;
                        checkEdit8.Checked = false;
                    }
                    b = a % 10;
                    a = a / 10;
                    if (b == 0)
                    {
                        textEdit10.Enabled = false;
                        checkEdit9.Checked = false;
                    }
                }
            }
            catch
            {
            }
        }

        public string setPassword="";        
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                setPassword = "";
                password xf = new password();
                xf.userpassword = userpassword;
                xf.Owner = this;
                this.Hide();
                xf.ShowDialog();
                xf.Dispose();
                this.Show();
                if (setPassword != "")
                {
                    userpassword = setPassword;                   
                    FileStream fs = new FileStream("load.ini", FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(fs);                    
                    bw.Write(setPassword);
                    bw.Write(SheZhi);
                    bw.Close();
                    fs.Close();
                    bw.Dispose();
                    fs.Dispose();
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show("发生错误:\n\r" + ex.ToString(), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            checkEdit1.Enabled = false;
            checkEdit2.Enabled = false;
            checkEdit3.Enabled = false;
            checkEdit4.Enabled = false;
            checkEdit5.Enabled = false;
            checkEdit6.Enabled = false;
            checkEdit7.Enabled = false;
            checkEdit8.Enabled = false;
            checkEdit9.Enabled = false;
            if (qx)
            {
                int i=0;
                if (checkEdit1.Checked) i = 1;
                if (checkEdit2.Checked) i = i+10;
                if (checkEdit3.Checked) i = i+100;
                if (checkEdit4.Checked) i = i+1000;
                if (checkEdit5.Checked) i = i+10000;
                if (checkEdit6.Checked) i = i+100000;
                if (checkEdit7.Checked) i = i+1000000;
                if (checkEdit8.Checked) i = i+10000000;
                if (checkEdit9.Checked) i = i+100000000;
                SheZhi = i;
                qx = false;               
            }
            if (userpassword != "")
            {
                FileStream fs = new FileStream("load.ini", FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(userpassword);
                bw.Write(SheZhi);
                bw.Close();
                fs.Close();
                bw.Dispose();
                fs.Dispose();
            }
        }
        #region 复选框事件
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                textEdit2.Enabled = true;
            }
            else
            {
                textEdit2.Enabled = false;
            }
        }

        
    

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked)
            {
                textEdit3.Enabled = true;
            }
            else
            {
                textEdit3.Enabled = false;
            }
        }

     

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit3.Checked)
            {
                textEdit4.Enabled = true;
            }
            else
            {
                textEdit4.Enabled = false;
            }
        }

        private void checkEdit4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit4.Checked)
            {
                textEdit5.Enabled = true;
            }
            else
            {
                textEdit5.Enabled = false;
            }
        }

        private void checkEdit6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit6.Checked)
            {
                textEdit7.Enabled = true;
            }
            else
            {
                textEdit7.Enabled = false;
            }
        }

        private void checkEdit5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit5.Checked)
            {
                textEdit6.Enabled = true;
            }
            else
            {
                textEdit6.Enabled = false;
            }
        }

        private void checkEdit7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit7.Checked)
            {
                textEdit8.Enabled = true;
            }
            else
            {
                textEdit8.Enabled = false;
            }
        }

        private void checkEdit8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit8.Checked)
            {
                textEdit9.Enabled = true;
            }
            else
            {
                textEdit9.Enabled = false;
            }
        }

        private void checkEdit9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit9.Checked)
            {
                textEdit10.Enabled = true;
            }
            else
            {
                textEdit10.Enabled = false;
            }
        }

        #endregion


        //查找启用项，给文本框焦点
        private void textcheck(int i)
        {
            pictureEdit1.Visible = true;
            pictureEdit2.Visible = false;
            switch(i)
            {
                case 1:
                    if (textEdit2.Enabled != false)
                    {
                        textEdit2.Text = "";
                        textEdit2.Focus();
                    }
                    else
                    {
                        textcheck(2);
                    }
                    break;
                case 2:
                    if (textEdit3.Enabled != false)
                    {
                        textEdit3.Text = "";
                        textEdit3.Focus();
                    }
                    else
                    {
                        textcheck(3);
                    }
                    break;
                case 3:
                    if (textEdit4.Enabled != false)
                    {
                        textEdit4.Text = "";
                        textEdit4.Focus();
                    }
                    else
                    {
                        textcheck(4);
                    }
                    break;
                case 4:
                    if (textEdit5.Enabled != false)
                    {
                        textEdit5.Text = "";
                        textEdit5.Focus();
                    }
                    else
                    {
                        textcheck(5);
                    }
                    break;
                case 5:
                    if (textEdit6.Enabled != false)
                    {
                        textEdit6.Text = "";
                        textEdit6.Focus();
                    }
                    else
                    {
                        textcheck(6);
                    }
                    break;
                case 6:
                    if (textEdit7.Enabled != false)
                    {
                        textEdit7.Text = "";
                        textEdit7.Focus();
                    }
                    else
                    {
                        textcheck(7);
                    }
                    break;
                case 7:
                    if (textEdit8.Enabled != false)
                    {
                        textEdit8.Text = "";
                        textEdit8.Focus();
                    }
                    else
                    {
                        textcheck(8);  
                    }
                    break;
                case 8:
                    if (textEdit9.Enabled != false)
                    {
                        textEdit9.Text = "";
                        textEdit9.Focus();
                    }
                    else
                    {
                        textcheck(9);
                    }
                    break;
                case 9:
                    if (textEdit10.Enabled != false)
                    {
                        textEdit10.Text = "";
                        textEdit10.Focus();
                    }
                    else
                    {
                        textcheck(10);
                    }
                    break;
                default:
                        sum++;
                        check();
                        clear();
                    break;
            }           
        }

        private void clear()
        {
            pictureEdit1.Visible = false;
            pictureEdit2.Visible = false;
            simpleButton2.Visible = false;
            simpleButton3.Visible = false;
            textEdit1.Enabled = true;
            textEdit1.Text = "";
            textEdit2.Text = "";
            textEdit3.Text = "";
            textEdit4.Text = "";
            textEdit5.Text = "";
            textEdit6.Text = "";
            textEdit7.Text = "";
            textEdit8.Text = "";
            textEdit9.Text = "";
            textEdit10.Text = "";
            checknum = 0;
            textEdit1.Focus();
        }

        private void check()
        {
            int x = 0;
            if (textEdit1.Enabled == true) x=x+1;
            if (textEdit2.Enabled == true) x=x+10;
            if (textEdit3.Enabled == true) x = x + 100;
            if (textEdit4.Enabled == true) x = x + 1000;
            if (textEdit5.Enabled == true) x = x + 10000;
            if (textEdit6.Enabled == true) x = x + 100000;
            if (textEdit7.Enabled == true) x = x + 1000000;
            if (textEdit8.Enabled == true) x = x + 10000000;
            if (textEdit9.Enabled == true) x = x + 100000000;
            if (textEdit10.Enabled == true) x = x + 1000000000;
            if (checknum % 1000000000 >= x % 1000000000 && checknum % 100000000 >= x % 100000000 && checknum % 10000000 >= x % 10000000 && checknum % 1000000 >= x % 1000000 
                && checknum % 100000 >= x % 100000 && checknum % 10000 >= x % 10000
            && checknum % 1000 >= x % 1000 && checknum % 100 >= x % 100 && checknum % 10 >= x % 10 && checknum >= x)
            {
                labelControl9.Text = "通过";
                labelControl9.ForeColor = Color.Green;
                string sql = @"insert  into [oracle].[dbo].[ODC_CHECK_Barcode](hostlable,maclable,barcode,PASS,PASSDATE) 
                                         values('" + ds.Tables[0].Rows[0][0].ToString().Trim() + @"','" + ds.Tables[0].Rows[0][1].ToString().Trim() +
                                           "','" + ds.Tables[0].Rows[0][2].ToString().Trim() + "','Y','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                int y = sqlserver.ExecCommand(sql);
                if (y < 1)
                {                   
                    MessageBox.Show("插入数据库失败！\n\r1.请检查网络、数据库是否正常；", "记录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                labelControl9.Text = "失败";
                labelControl9.ForeColor = Color.Red;
            }
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            labelControl9.Text = "";
            clear();
        }

        #region 文本框事件
        DataSet ds =null;
        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            //C40938-F4200C40938A29635
            if (e.KeyCode == Keys.Enter && textEdit1.Text.Trim().Length > 10)
            {
                labelControl9.Text = "";
                ds = null;
                try
                {
                    //string test = "";
                    //for (int i = 0; i < textEdit1.Text.Trim().Length; i = i + 4)
                    //{
                    //    if ((i + 3) < textEdit1.Text.Trim().Length)
                    //    {
                    //        test = test + " " + textEdit1.Text.Trim().Substring(i, 4);
                    //    }
                    //    else
                    //    {
                    //        test = test + " " + textEdit1.Text.Trim().Substring(i, textEdit1.Text.Trim().Length-i);
                    //    }
                    //}
                    //string test1 = test.Substring(1,test.Length-1);
                    string sql = @"SELECT a.hostlable,a.maclable,b.barcode1,b.sn,a.DYSTLABLE,a.PRODUCTLABLE,b.PASSWORD,b.wlanpas
  FROM [oracle].[dbo].[ODC_ALLLABLE] a,[oracle].[dbo].[ODC_MACINFO] b where a.MACLABLE=replace(b.MAC,'-','') and (b.barcode1='" + textEdit1.Text.Trim() + "' )";
                    ds = sqlserver.GetDataSet(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("请检查网络、数据库是否正常；\n\r" + ex, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    checknum += 1;
                    PrintText = ds.Tables[0].Rows[0][0].ToString().Trim();
                    textcheck(1);
                }
                else
                {
                    pictureEdit1.Visible = false;
                    pictureEdit2.Visible = true;
                    textEdit1.Text = "";
                    textEdit1.Focus();
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                textEdit1.Text = "";
                textEdit1.Focus();
            }
           
        }

        private void textEdit2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textEdit2.Text.Trim().Length > 0)
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && textEdit2.Text.Trim()==ds.Tables[0].Rows[0][3].ToString().Trim())
                {
                    checknum += 10;
                    textcheck(2);
                }
                else
                {
                    pictureEdit1.Visible = false;
                    pictureEdit2.Visible = true;
                    textEdit2.Text = "";
                    textEdit2.Focus();
                }
            }
        }

        private void textEdit3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textEdit3.Text.Trim().Length > 0)
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && textEdit3.Text.Trim() == ds.Tables[0].Rows[0][0].ToString().Trim())
                {
                    checknum += 100;
                    textcheck(3);
                }
                else
                {
                    pictureEdit1.Visible = false;
                    pictureEdit2.Visible = true;
                    textEdit3.Text = "";
                    textEdit3.Focus();
                }
            }
        }

        private void textEdit4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textEdit4.Text.Trim().Length > 0)
            {
                //string test = "";
                //for (int i = 0; i < textEdit4.Text.Trim().Length; i = i + 4)
                //{
                //    test = test + " " + textEdit4.Text.Trim().Substring(i, 4);
                //}
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && (textEdit4.Text.Trim() == ds.Tables[0].Rows[0][2].ToString().Trim()))
                {
                    checknum += 1000;
                    textcheck(4);
                }
                else
                {
                    pictureEdit1.Visible = false;
                    pictureEdit2.Visible = true;
                    textEdit4.Text = "";
                    textEdit4.Focus();
                }
            }
        }

        private void textEdit5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textEdit5.Text.Trim().Length > 0)
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && textEdit5.Text.Trim().Contains(ds.Tables[0].Rows[0][7].ToString().Trim()))
                {
                    checknum += 10000;
                    textcheck(5);
                }
                else
                {
                    pictureEdit1.Visible = false;
                    pictureEdit2.Visible = true;
                    textEdit5.Text = "";
                    textEdit5.Focus();
                }
            }
        }

        private void textEdit6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textEdit6.Text.Trim().Length > 0)
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && textEdit6.Text.Trim() == ds.Tables[0].Rows[0][3].ToString().Trim())
                {
                    checknum += 100000;
                    textcheck(6);
                }
                else
                {
                    pictureEdit1.Visible = false;
                    pictureEdit2.Visible = true;
                    textEdit6.Text = "";
                    textEdit6.Focus();
                }
            }
        }

        private void textEdit7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textEdit7.Text.Trim().Length > 0)
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && (textEdit7.Text.Trim() == ds.Tables[0].Rows[0][1].ToString().Trim() || textEdit7.Text.Trim().Replace("-","") == ds.Tables[0].Rows[0][1].ToString().Trim()))
                {
                    checknum += 1000000;

                    if (PrintChange == "1")
                    {
                        GetParaDataPrint_YX02(1);
                    }

                    textcheck(7);
                }
                else
                {
                    pictureEdit1.Visible = false;
                    pictureEdit2.Visible = true;
                    textEdit7.Text = "";
                    textEdit7.Focus();
                }
            }
        }

        private void textEdit8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textEdit8.Text.Trim().Length > 0)
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && textEdit8.Text.Trim() == ds.Tables[0].Rows[0][0].ToString().Trim())
                {
                    checknum += 10000000;
                    textcheck(8);
                }
                else
                {
                    pictureEdit1.Visible = false;
                    pictureEdit2.Visible = true;
                    textEdit8.Text = "";
                    textEdit8.Focus();
                }
            }
        }

        private void textEdit9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textEdit9.Text.Trim().Length > 0)
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && textEdit9.Text.Trim() == ds.Tables[0].Rows[0][4].ToString().Trim())
                {
                    checknum += 100000000;
                    textcheck(9);
                }
                else
                {
                    pictureEdit1.Visible = false;
                    pictureEdit2.Visible = true;
                    textEdit9.Text = "";
                    textEdit9.Focus();
                }
            }
        }

        private void textEdit10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textEdit10.Text.Trim().Length > 0)
            {
                string p1 = ds.Tables[0].Rows[0][2].ToString().Trim();
                string p2 = ds.Tables[0].Rows[0][6].ToString().Trim();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && (((textEdit10.Text.Trim().Contains(p1)) && (textEdit10.Text.Trim().Contains(p2))) || textEdit10.Text.Trim() == ds.Tables[0].Rows[0][5].ToString().Trim()))
                {
                    checknum += 1000000000;
                    textcheck(10);
                }
                else
                {
                    pictureEdit1.Visible = false;
                    pictureEdit2.Visible = true;
                    textEdit10.Text = "";
                    textEdit10.Focus();
                }
            }
        }

        #endregion


        //----以下是YX01数据采集----
        private void GetParaDataPrint_YX02(int tt_itemtype)
        {
            //单板打印
            if (PrintText != "")
            {
                FastReport.Report report = new FastReport.Report();

                report.Prepare();
                report.Load(Application.StartupPath + "\\LABLE\\YX_2.frx");
                report.SetParameterValue("S01", PrintText);

                report.PrintSettings.ShowDialog = false;

                //--打印
                if (tt_itemtype == 1)
                {
                    report.Print();
                }

                //--预览
                if (tt_itemtype == 2)
                {
                    report.Design();
                }
            }

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Form2 frmlogin = new Form2();
            frmlogin.StartPosition = FormStartPosition.CenterParent;
            if (frmlogin.ShowDialog() == DialogResult.OK)
            {
                simpleButton2.Visible = true;
                simpleButton3.Visible = true;
                textEdit1.Enabled = false;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            GetParaDataPrint_YX02(2);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            GetParaDataPrint_YX02(1);
        }
    }
}
