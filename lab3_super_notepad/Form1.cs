using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lab3_super_notepad
{
    public partial class Form1 : Form
    {
        private int window_id = 1;
        public Form1()
        {
            InitializeComponent();
            saveAsToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file_form new_ff = new file_form();
            new_ff.MdiParent = this;
            new_ff.setFormName("Untitled " + window_id);
            new_ff.Show();
            window_id++;

            ToolStripMenuItem tsmi = new ToolStripMenuItem(new_ff.getFormName(), null, new EventHandler(menuClick));
            arrangeIconsToolStripMenuItem.DropDownItems.Add(tsmi);
        }

        private void menuClick(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem tmp = sender as ToolStripMenuItem;
                foreach (ToolStripMenuItem val in arrangeIconsToolStripMenuItem.DropDownItems)
                {
                    val.Checked = false;
                }
                tmp.Checked = true;
                foreach (file_form val in this.MdiChildren)
                {
                    if (tmp.Text.CompareTo(val.getFormName()) == 0)
                    {
                        this.ActivateMdiChild(val);
                        val.BringToFront();
                        break;
                    }
                }
            }
        }

        private void arrangeIconsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem val in arrangeIconsToolStripMenuItem.DropDownItems)
            {
                val.Checked = false;
            }
            if (this.ActiveMdiChild == null)
                return;

            file_form tmp = this.ActiveMdiChild as file_form;
            foreach (ToolStripMenuItem val in arrangeIconsToolStripMenuItem.DropDownItems)
            {
                if (tmp.getFormName().CompareTo(val.Text) == 0)
                {
                    val.Checked = true;
                    break;
                }
            }
        }  

        private void arrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file_form ff = (file_form)this.ActiveMdiChild;
            ff.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file_form ff = (file_form)this.ActiveMdiChild;
            ff.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file_form ff = (file_form)this.ActiveMdiChild;
            ff.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file_form ff = (file_form)this.ActiveMdiChild;
            ff.SelectAll();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file_form ff = (file_form)this.ActiveMdiChild;
            ff.ClearAll();
        }

       private void openToolStripMenuItem_Click(object sender, EventArgs e)
       {
           openFileDialog1.FileName = "Текстовые файлы";
           openFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files(*.*)|*.*";

           if (openFileDialog1.ShowDialog() == DialogResult.OK)
           {
               file_form ff = new file_form();
               ff.Open(openFileDialog1.FileName);
               ff.MdiParent = this;
               ff.setFormName(openFileDialog1.FileName);
               ff.Text = ff.getFormName();
               ff.Show();
               saveAsToolStripMenuItem.Enabled = true;
               saveToolStripMenuItem.Enabled = true;
               ToolStripMenuItem tsmi = new ToolStripMenuItem(ff.getFormName(), null, new EventHandler(menuClick));
               arrangeIconsToolStripMenuItem.DropDownItems.Add(tsmi);
           }
       }

       private void saveToolStripMenuItem_Click(object sender, EventArgs e)
       {
           saveFileDialog1.FileName = "Текстовые файлы";
           saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files(*.*)|*.*";

           file_form ff = (file_form)this.ActiveMdiChild;
           if (ff.Text == ("Untitled " + window_id))
           {
               if (saveFileDialog1.ShowDialog() == DialogResult.OK)
               {
                   ff.Save(saveFileDialog1.FileName);
                   ff.MdiParent = this;
                   ff.setFormName(saveFileDialog1.FileName);
                   ff.Text = ff.getFormName();
               }
           }
           else
           {
               ff.Save(ff.getFormName());
           }
       }

       private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
       {
           saveFileDialog1.FileName = "Текстовые файлы";
           saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files(*.*)|*.*";

           if (saveFileDialog1.ShowDialog() == DialogResult.OK)
           {
               file_form ff = (file_form)this.ActiveMdiChild;
               ff.Save(saveFileDialog1.FileName);
               ff.MdiParent = this;
               ff.setFormName(saveFileDialog1.FileName);
               ff.Text = ff.getFormName();
           }
       }

       private void fontToolStripMenuItem_Click(object sender, EventArgs e)
       {
           file_form ff = (file_form)this.ActiveMdiChild;
           ff.MdiParent = this;
           fontDialog1.ShowColor = true;
           fontDialog1.Font = ff.getRichTextBox().SelectionFont;
           fontDialog1.Color = ff.getRichTextBox().SelectionColor;
           if (fontDialog1.ShowDialog() == DialogResult.OK)
           {
               ff.getRichTextBox().SelectionFont = fontDialog1.Font;
               ff.getRichTextBox().SelectionColor = fontDialog1.Color;
           }
           /*fontDialog1.Font = ff.richTextBox1.SelectionFont;
           fontDialog1.Color = ff.richTextBox1.SelectionColor;
           if (fontDialog1.ShowDialog() == DialogResult.OK)
           {
               ff.richTextBox1.SelectionFont = fontDialog1.Font;
               ff.richTextBox1.SelectionColor = fontDialog1.Color;
           }*/
           ff.Show();
       }

       private void colorToolStripMenuItem_Click(object sender, EventArgs e)
       {
           file_form ff = (file_form)this.ActiveMdiChild;
           ff.MdiParent = this;
           colorDialog1.Color = ff.getRichTextBox().SelectionColor;
           if (colorDialog1.ShowDialog() == DialogResult.OK)
           {
               ff.getRichTextBox().SelectionColor = colorDialog1.Color;
           }
           ff.Show();
       }

       private void exitToolStripMenuItem_Click(object sender, EventArgs e)
       {
           this.Close();
       }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(this, new EventArgs());
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(this, new EventArgs());
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                return;
            }
            else
            {
                saveToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                return;
            }
            else
            {
                cutToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                return;
            }
            else
            {
                copyToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                return;
            }
            else
            {
                pasteToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        public void menuItemClose(string val)
        {
            foreach (ToolStripMenuItem item in arrangeIconsToolStripMenuItem.DropDownItems)
            {
                if (val.CompareTo(item.Text) == 0)
                {
                    arrangeIconsToolStripMenuItem.DropDownItems.Remove(item);
                    break;
                }
            }
        }

        private void formatToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                fontToolStripMenuItem.Enabled = false;
                colorToolStripMenuItem.Enabled = false;
            }
            else
            {
                fontToolStripMenuItem.Enabled = true;
                colorToolStripMenuItem.Enabled = true;
            }
        }

        private void editToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                pasteToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                selectAllToolStripMenuItem.Enabled = false;
                findToolStripMenuItem.Enabled = false;
            }
            else
            {
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                pasteToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
                selectAllToolStripMenuItem.Enabled = true;
                findToolStripMenuItem.Enabled = true;
            }
        }

        private void fileToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                saveToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
            }
            else
            {
                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
            }
        }

        private void windowToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                arrangeIconsToolStripMenuItem.Enabled = false;
                cascadeToolStripMenuItem.Enabled = false;
                tileHorizontalToolStripMenuItem.Enabled = false;
                tileVerticalToolStripMenuItem.Enabled = false;
            }
            else
            {
                arrangeIconsToolStripMenuItem.Enabled = true;
                cascadeToolStripMenuItem.Enabled = true;
                tileHorizontalToolStripMenuItem.Enabled = true;
                tileVerticalToolStripMenuItem.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            info_form i_f = new info_form();
            i_f.Show();
            /*i_f.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            i_f.TopLevel = true;*/
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            find_form frm = new find_form();
            if (frm.ShowDialog(this) == DialogResult.Cancel)
                return;
            file_form form = (file_form)this.ActiveMdiChild;
            form.MdiParent = this;
            int pos = form.getRichTextBox().SelectionStart;
            form.getRichTextBox().Find(frm.FindText, pos, frm.Condition);
        }
    }
}
