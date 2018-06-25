using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 烽火条码检测
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            bool bRet = false;
            if (e.KeyCode == Keys.Enter)
            {
                if (this.textBox1.Text == "hushuangnizhegedakeng")
                {
                    bRet = true;
                }

                if (bRet)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
