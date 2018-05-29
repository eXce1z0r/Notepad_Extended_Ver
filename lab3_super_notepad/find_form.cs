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
    public partial class find_form : Form
    {
        public find_form()
        {
            InitializeComponent();
        }

        private void find_form_Load(object sender, EventArgs e)
        {

        }

        public RichTextBoxFinds Condition
        {
            get
            {
                if (MC.Checked && MWW.Checked)
                {
                    return RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord;
                }
                else if (MC.Checked)
                {
                    return RichTextBoxFinds.MatchCase;
                }
                else if (MWW.Checked)
                {
                    return RichTextBoxFinds.WholeWord;
                }
                else
                {
                    return RichTextBoxFinds.None;
                }
            }
        }

        public string FindText
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        private void fFind_Load(object sender, EventArgs e)
        {

        }

        private void MWW_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MC_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
