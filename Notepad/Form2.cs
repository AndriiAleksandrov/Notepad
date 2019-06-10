using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                button1.Enabled = true;
            }
        }

        RichTextBox rt;
        public static Button bt;
        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            rt = Form1.rt;
        }

        int m;
        int end;
        private void button1_Click(object sender, EventArgs e)
        {
            if (rt.SelectionStart == rt.Text.Length)
            {
                m = rt.SelectionStart;
            }



            bt = button1;
            if (radioButton2.Checked)
            {
                rt.SelectionStart = m;
                m = rt.SelectionStart;
                end = rt.Text.Length + 1;
            }
            else
            {
                rt.SelectionStart = m;
                m = rt.SelectionStart;
                end = -1;
            }

            //    m = rt.SelectionStart;
            if (checkBox1.Checked)
            {
                int j = rt.Find(textBox1.Text, m, end, RichTextBoxFinds.MatchCase);
                if (j >= 0)
                {
                    m = j + textBox1.Text.Length;


                    rt.Focus();
                }
                else
                {

                    MessageBox.Show(string.Format("can't find \"{0}\"", textBox1.Text));
                    m = 0;
                }

            }
            else
            {


                int i = rt.Find(textBox1.Text, m, end, RichTextBoxFinds.None);
                if (i >= 0)
                {

                    m = i + textBox1.Text.Length;


                    rt.Focus();
                }
                else
                {
                    MessageBox.Show(string.Format("can't find \"{0}\"", textBox1.Text));
                    m = 0;
                }
            }

        }
    }
}
