using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace WindowsFormsApplication5
{
    public partial class Form3 : Form
    {
       private frame fram = new frame();  

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           // System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\yousef\Downloads\Music\beep-01a.wav");
           // player.Play();
            fram.op.switch_form3_to_form2(this);
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Instructions
            string readText = File.ReadAllText("Instructions.txt");
            MessageBox.Show(readText);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // exit button 
            System.Windows.Forms.Application.Exit();
        }
    }
}
