using System;
using System.Diagnostics;
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
                    this.Text = "Untitled - Notepad";

                }
                else if (cl == DialogResult.No)// This block runs when you click on the no option on message box. 

                {
                    richTextBox1.Clear();
                    fname = string.Empty;
                    this.Text = "Untitled - Notepad";
                }



            }

            else  // this block executes when no modification is done in the file. 
            {
                richTextBox1.Clear();
                fname = string.Empty;
                this.Text = "Untitled - Notepad";
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

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

                string s;
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

        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 obj = new Form2();
            obj.Show();
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
                replaceToolStripMenuItem.Enabled = findToolStripMenuItem.Enabled = undoToolStripMenuItem.Enabled = true;
                //       isundo = true;
            }

            findOnWebToolStripMenuItem.Enabled = convertCaseToToolStripMenuItem.Enabled = deleteToolStripMenuItem.Enabled = cutToolStripMenuItem.Enabled = copyToolStripMenuItem.Enabled = richTextBox1.SelectedText.Length > 0;
        }
    }
}
