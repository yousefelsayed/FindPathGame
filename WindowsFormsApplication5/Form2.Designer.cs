namespace WindowsFormsApplication5
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(12, 168);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(120, 100);
            this.button4.TabIndex = 3;
            this.button4.Text = "Level 1";
            this.button4.UseVisualStyleBackColor = false;
          
            
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(346, 312);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(120, 100);
            this.button5.TabIndex = 4;
            this.button5.Text = "Level 2";
            this.button5.UseVisualStyleBackColor = false;

            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(655, 159);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(120, 100);
            this.button6.TabIndex = 5;
            this.button6.Text = "Level 3";
            this.button6.UseVisualStyleBackColor = false;
   
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button1.Location = new System.Drawing.Point(346, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 50);
            this.button1.TabIndex = 6;
            this.button1.Text = "Return To Main Menu";
            this.button1.UseVisualStyleBackColor = false;

            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApplication5.Properties.Resources.تنزيل1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(812, 424);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button button4;
        public System.Windows.Forms.Button button5;
        public System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button1;
    }
}