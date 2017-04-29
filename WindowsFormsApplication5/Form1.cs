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
using System.Threading;
using System.Globalization;

namespace WindowsFormsApplication5
{
    
    public partial class Form1 : Form
    {

       private  frame fram = new frame();
       
        public Form1()
        {
           InitializeComponent();
          
           foreach (Control c in this.Controls)
               if (c is PictureBox) c.Click += pictureBox_Click;

        }

       public void pictureBox_Click(object sender, EventArgs e)
        {
            int r=-1,c=-1;
            string index = "";
            PictureBox _clicked = sender as PictureBox;
            for (int i = 0; i < _clicked.Name.Length; i++)
            {
                if (char.IsDigit(_clicked.Name[i])) { index += _clicked.Name[i]; }
            }
            
            for (int i = 0; i < fram.row; i++)
            {
                for (int j = 0; j < fram.col; j++)
                {
                    if (fram.row * i + j + 1 == int.Parse(index))
                    { r = i; c = j; break; }
                }
                if (r != -1) { break; }
            }
       
            if (fram.click( r, c))
            {

                fram.op.update_index_file();
                fram.op.switch_form1_to_form2(this);
            }
            fram.Counter(ref label2);
        }
             
        private void Form1_Load(object sender, EventArgs e)
        {
            
            //make_boxes(fram.row, fram.col);
            
             fram.load_images(this);

        }
       
        private void timer1_Tick(object sender, EventArgs e)
        {
             fram.timer_count--;
            int a =  fram.timer_count;
           label1.Text = a.ToString();
           if ( fram.timer_count == 0)
            {
                label4.Visible = false;
                timer1.Enabled = false; 
                MessageBox.Show("Game Over");
               // Timer_label.Visible = false;
                 fram.op.switch_form1_to_form2(this);
            }
           if ( fram.solve())
            {
                label4.Visible = false;
                timer1.Enabled = false;
             //   Timer_label.Visible = false;
            }
        }   
    }
}
