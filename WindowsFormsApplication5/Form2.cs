using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Threading;


namespace WindowsFormsApplication5
{
    public partial class Form2 : Form
    {
        private frame fram = new frame();
       
        public Form2()
        {
            InitializeComponent();
            foreach (Control c in this.Controls)
                if (c is Button) c.Click += button_Click;
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {

            fram.op.Enable_passed_levels(this);
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button _clicked = sender as Button;
            string index = "";
            for (int i = 0; i < _clicked.Name.Length; i++)
            {
                if (char.IsDigit(_clicked.Name[i]))
                {
                    index += _clicked.Name[i]; 
                }
            }
            if (int.Parse(index) == 1) { fram.op.switch_form2_to_form3(this); }// if button pressed is 1 that indicates return to main menu
            else
            {
                fram.op.set_index_file(int.Parse(index) - 4); // -4 because we start at picturebox 4 if 4 will send level 0 if 5 send level 1 and so on
                fram.op.switch_form2_to_form1(this);
            }
        }

    }
}
