using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static RichTextBox rt;
        string fname;
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Modified)// This block checks that either you make changes or not , if yes and you don’t save the chnges than this code block executes . 
            {
                DialogResult cl = MessageBox.Show("Do you want to save changes in the current file", "Notepad", MessageBoxButtons.YesNoCancel);
                if (cl == DialogResult.Yes)// This block runs when you click on the yes option on message box. 
                {

                    save.PerformClick();
                    richTextBox1.Clear();
                    fname = string.Empty;
                    this.Text = "Untitled";

                }
                else if (cl == DialogResult.No)// This block runs when you click on the no option on message box. 

                {
                    richTextBox1.Clear();
                    fname = string.Empty;
                    this.Text = "Untitled";
                }



            }

            else  // this block executes when no modification is done in the file. 
            {
                richTextBox1.Clear();
                fname = string.Empty;
                this.Text = "Untitled";
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem.PerformClick();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Modified)
            {
                if (richTextBox1.Text.Length > 0)
                {
                    DialogResult cl = MessageBox.Show("Do you want to save changes in the current file", "My Notepad", MessageBoxButtons.YesNoCancel);

                    if (cl == DialogResult.Yes)
                    {


                        save.PerformClick();// perform the functioning  of save button

                    }
                    else if (cl == DialogResult.No)
                    {
                        DialogResult res;
                        openFileDialog1.Filter = "Text File|*.txt|Html file|*.htm|All Files|*."; // set filter for  the file dialod box 
                        openFileDialog1.FileName = string.Empty;
                        res = openFileDialog1.ShowDialog();//open file dialog box 
                        if (res == DialogResult.OK)
                        {
                            string s = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                            richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                            this.Text = s + "-Notepad";
                            richTextBox1.Modified = false;
                        }
                        fname = saveFileDialog1.FileName;
                    }
                }
                else
                {
                    DialogResult res;
                    openFileDialog1.Filter = "Text File|*.txt|Html file|*.htm|All Files|*.";
                    openFileDialog1.FileName = string.Empty;
                    res = openFileDialog1.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        string s = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                        richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        this.Text = s + "-Notepad";
                        richTextBox1.Modified = false;
                    }
                    fname = saveFileDialog1.FileName;
                }

            }


            else
            {
                DialogResult res;
                openFileDialog1.Filter = "Text File|*.txt|Html file|*.htm|All Files|*.";
                openFileDialog1.FileName = string.Empty;
                res = openFileDialog1.ShowDialog();
                if (res == DialogResult.OK)
                {
                    string s = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                    this.Text = s + "-Notepad";
                    richTextBox1.Modified = false;
                }
                fname = saveFileDialog1.FileName;
            }
        }

        private void saveAs_Click(object sender, EventArgs e)
        {
            DialogResult res;
            saveFileDialog1.Filter = "Text File|*.txt|Html file|*.htm|All Files|*.";
            res = saveFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                richTextBox1.Modified = false;
            }
            fname = saveFileDialog1.FileName;
            string name = Path.GetFileNameWithoutExtension(fname);

            this.Text = name + "- Notepad";
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            save.PerformClick();
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                if (string.IsNullOrEmpty(fname))
                {
                    saveAs.PerformClick();// perform click function executes the code of the save as button.
                    richTextBox1.Modified = false;
                }
                else
                {
                    richTextBox1.SaveFile(fname, RichTextBoxStreamType.PlainText);
                }
            }
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PageSetupDialog ps;
                printDocument1.DocumentName = "print document";
                ps = pageSetupDialog1;
                ps.AllowMargins = true;
                ps.AllowOrientation = true;
                ps.AllowPaper = true;
                ps.AllowPrinter = true;
                ps.Document = printDocument1;
                ps.ShowDialog();
                ps.Reset();
                if (ps.ShowDialog() == DialogResult.OK)
                {
                    //printDocument1.DefaultPageSettings;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult pr;
            pr = printDialog1.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            line = 1 + richTextBox1.GetLineFromCharIndex(richTextBox1.GetFirstCharIndexOfCurrentLine());
            column = 1 + richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine();
            richTextBox1.ForeColor = Color.Black;

            toolStripStatusLabel1.Text = string.Format("ln {0} cl {1}", line, column);


            if (richTextBox1.Text.Length > 0)
            {
                toolStripButton5.Enabled = true;

            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        bool isundo = false;
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isundo = !isundo;
            if (isundo)
            {
                richTextBox1.Undo();
                redoToolStripMenuItem.Enabled = true;

            }
            else
            {
                richTextBox1.Redo();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataFormats.Format myformat = DataFormats.GetFormat(DataFormats.Text);
            if (richTextBox1.CanPaste(myformat))
            {
                richTextBox1.Paste(myformat);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.SelectionStart, richTextBox1.SelectionLength);
        }

        private void findToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process proc = new Process();
            if (richTextBox1.SelectedText.Length > 0)
            {
                proc.StartInfo.FileName = "http://www.google.com/search?q=" + richTextBox1.SelectedText;
                proc.Start();
            }
            else
            {
                MessageBox.Show("Please select text for search on Google");
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += DateTime.Today.ToShortDateString();
        }

        private void convertCaseToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = richTextBox1.SelectedText.ToUpper();
        }

        private void lowerCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = richTextBox1.SelectedText.ToLower();
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (richTextBox1.Modified)
            {
                undoToolStripMenuItem.Enabled = true;
            }

            deleteToolStripMenuItem.Enabled = cutToolStripMenuItem.Enabled = copyToolStripMenuItem.Enabled = richTextBox1.SelectedText.Length > 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.WordWrap = !richTextBox1.WordWrap;// Word wrap is a property of rich textbox which return true and false. 
            statusBarToolStripMenuItem.Enabled = !richTextBox1.WordWrap;
            if (richTextBox1.WordWrap == true)
            {
                wordWrapToolStripMenuItem.Checked = true;
                statusBarToolStripMenuItem.Checked = false;
                statusStrip1.Visible = false;
            }
            else
            {
                statusBarToolStripMenuItem.Checked = true;
                statusStrip1.Visible = true;
                wordWrapToolStripMenuItem.Checked = false;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult ft;
            ft = fontDialog1.ShowDialog();
            if (ft == DialogResult.OK)// If we click on the ok button oh font dialog 
            {
                richTextBox1.SelectionFont = fontDialog1.Font;// set the font of rich textbox text same as selected in the Fnt dialog. 
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult cl;

            cl = colorDialog1.ShowDialog();
            if (cl == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
            }
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!wordWrapToolStripMenuItem.Checked)
            {
                statusBarToolStripMenuItem.Checked = !statusBarToolStripMenuItem.Checked;
                if (statusBarToolStripMenuItem.Checked)
                {

                    statusStrip1.Visible = true;

                }
                else
                {

                    statusStrip1.Visible = false;
                }
            }
        }

        int line;
        int column;
        private void richTextBox1_CursorChanged(object sender, EventArgs e)
        {
            line = 1 + richTextBox1.GetLineFromCharIndex(richTextBox1.GetFirstCharIndexOfCurrentLine());
            column = 1 + richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine();
            richTextBox1.ForeColor = Color.Black;
            MessageBox.Show("the cussor changed ");
            toolStripStatusLabel1.Text = string.Format("ln {0} cl {1}", line, column);
        }

        float m = 12;
        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(m < 73)
            {
                m = richTextBox1.Font.Size;
                richTextBox1.Font = new Font(richTextBox1.Font.Name, m + 2);// add 3 on every click until the font size is less than 73 (you can use any value for increment and limit ) 
            }
            else if (m <= 70)
            {
                zoomInToolStripMenuItem.Enabled = false;
            }
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m > 7)
            {
                m = richTextBox1.Font.Size;
                richTextBox1.Font = new Font(richTextBox1.Font.Name, m - 2);
            }
            else if (m >= 8)
            {
                zoomOutToolStripMenuItem.Enabled = false;
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Font = new Font(richTextBox1.Font.Name, 12);
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 obj = new AboutBox1();
            obj.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem.PerformClick();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            isundo = !isundo;
            if (isundo)
            {
                richTextBox1.Undo();
                redoToolStripMenuItem.Enabled = true;

            }
            else
            {
                richTextBox1.Redo();
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.Font.Name, richTextBox1.Font.Size, FontStyle.Bold);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.Font.Name, richTextBox1.Font.Size, FontStyle.Italic);
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.Font.Name, richTextBox1.Font.Size, FontStyle.Underline);
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            zoomInToolStripMenuItem.PerformClick();
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            zoomOutToolStripMenuItem.PerformClick();
        }
    }
}
