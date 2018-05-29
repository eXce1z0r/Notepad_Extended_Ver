using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace lab3_super_notepad
{
    public partial class file_form : Form
    {
        private String form_name = "";
        private String BufferText = "";
        public file_form()
        {
            InitializeComponent();
            toolStripStatusLabel2.Text = System.DateTime.Now.ToLongTimeString();
            toolStripStatusLabel2.AutoToolTip = true;
            toolStripStatusLabel2.ToolTipText = System.DateTime.Now.ToLongDateString();
            //ContextMenu a;
        }

        public void setRichTextBox(RichTextBox RTB)
        {
            richTextBox1 = RTB;
        }

        public RichTextBox getRichTextBox()
        {
            return richTextBox1;
        }

        public void setFormName(String name)
        {
            form_name = name;
            this.Text = form_name;
        }

        public String getFormName()
        {
            return form_name;
        }

        public void Cut()
        {
            if (richTextBox1.SelectedText.Length == 0)
            {
                return;
            }
            else
            {
                BufferText = richTextBox1.SelectedText;
                richTextBox1.SelectedText = "";
            }
        }

        public void Copy()
        {
            if (richTextBox1.SelectedText.Length == 0)
            {
                return;
            }
            else
            {
                BufferText = richTextBox1.SelectedText;
            }
        }

        public void Paste()
        {
            richTextBox1.SelectedText = "";
            int cursor_pos = richTextBox1.SelectionStart;
            String textBeforeCursor = richTextBox1.Text.Substring(0, cursor_pos);
            String textAfterCursor = richTextBox1.Text.Substring(cursor_pos, (richTextBox1.Text.Length - cursor_pos));
            int cursor_pos_after_all = textBeforeCursor.Length + BufferText.Length;

            richTextBox1.Text = textBeforeCursor + BufferText + textAfterCursor;
            richTextBox1.SelectionStart = cursor_pos_after_all;
            //richTextBox1.
        }

        public void ClearAll()
        {
            BufferText = "";
            richTextBox1.Text = "";
        }

        public void SelectAll()
        {
            richTextBox1.SelectAll();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            /*int symbols_amount = 0;
            if (richTextBox1.TextLength != null)
            {
                symbols_amount = richTextBox1.TextLength;
            }*/
            toolStripStatusLabel1.Text = "Amount of symbols: " + richTextBox1.TextLength.ToString();
        }

        private void file_form_Load(object sender, EventArgs e)
        {

        }

        private void file_form_Resize(object sender, EventArgs e)
        {
            richTextBox1.Width = this.Width - 15;
            richTextBox1.Height = this.Height - 60;
        }

        private void toolStripStatusLabel2_MouseEnter(object sender, EventArgs e)
        {
            //toolStripStatusLabel2.Selected = true;
        }

        private void contextMenuStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            /*if (e.Button == MouseButtons.Right)
            {
                int index = contextMenuStrip1.
                //contextMenuStrip1.Show(this.);
            }*/
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        public void Open(string OpenFileName)
        {
            if (OpenFileName == "")
            {
                return;
            }
            else
            {
                StreamReader sr = new StreamReader(OpenFileName);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();
                form_name = OpenFileName;
            }
        }

        public void Save(string SaveFileName)
        {
            if (SaveFileName == "")
            {
                return;
            }
            else
            {
                StreamWriter sw = new StreamWriter(SaveFileName);
                sw.WriteLine(richTextBox1.Text);
                sw.Close();
                form_name = SaveFileName;
            }
        }

        private void file_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.ParentForm is Form1)
            {
                Form1 tmp = this.ParentForm as Form1;
                tmp.menuItemClose(this.getFormName());
            }
        }
    }
}
