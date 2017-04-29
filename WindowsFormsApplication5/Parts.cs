using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;




namespace WindowsFormsApplication5
{
   

    public class frame :Control 

    {
        public Parts[,] part ;
        public int row, col;
        public operations op;
        public int sx , sy, ex, ey;
        public int Count_num ;
        public Int16 timer_count ;
       // private int sbx, sby, ebx, eby; // sbx : start ball x-axis .......
       
        public frame ()
        {
            row = col = 0;
            op = new operations();
            timer_count = 60;
        }

        public void get_dimension_of_lvl ()
        {

            FileStream fs = new FileStream("Levels pictures.txt", FileMode.Open);
            StreamReader r = new StreamReader(fs);
            FileStream k = new FileStream("Index.txt", FileMode.Open);
            StreamReader l = new StreamReader(k);
            string s = "";
            while (l.Peek() != -1) // to read last line in the index file bec. if the index file have previous value i need to take last press i have did
            { s = l.ReadLine(); }

            int level = int.Parse(s);
            int o = 0;
            while (true)
            {

                if (o == level)
                {
                    s = r.ReadLine();
                   
                    break;
                }
                r.ReadLine();
                o++;
            }
           row = int.Parse(s[0].ToString());
           col = int.Parse(s[1].ToString());
       
           l.Close(); k.Close();
           r.Close();
           fs.Close();

        }
        
        public frame(frame f)
        {
            
                get_dimension_of_lvl();
              
            this.part = new Parts[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    this.part[i, j] = new Parts(f.part[i,j]);
                }
            }
            this.op = new operations(); this.op = f.op;
            this.sx = f.sx; this.sy = f.sy; this.ex = f.ex; this.ey = f.ey;
        }

        public void load_images(Form1 form1)
        {
            try
            {

                FileStream fs = new FileStream("Levels pictures.txt", FileMode.Open);
                StreamReader r = new StreamReader(fs);
                FileStream k = new FileStream("Index.txt", FileMode.Open);
                StreamReader l = new StreamReader(k);
                string s = "";
                int tr = 4, tc = 2;              // we would place the picturebox start at (4,2) at the form 
                while (l.Peek() != -1) // to read last line in the index file bec. if the index file have previous value i need to take last press i have did
                {
                    s = l.ReadLine();
                }
                int level = int.Parse(s);
                int o = 0;
                while (true)
                {

                    if (o == level)
                    {
                        s = r.ReadLine();
                        if (o == 2)
                        { form1.timer1.Enabled = true; form1.label1.Visible = true; form1.label4.Visible = true; }// Timer_label.Visible = true; 
                        break;
                    }
                    r.ReadLine();
                    o++;
                }
                l.Close();
                k.Close();
                r.Close(); fs.Close();
                string[] z = s.Split('*');
                part = null;
                get_dimension_of_lvl();
                part = new Parts[row, col];
                set_frame();
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {

                        PictureBox bm = new PictureBox();
                        bm.Location = new Point(tr, tc);
                        bm.Size = new System.Drawing.Size(100, 100);
                        bm.Visible = true;
                        bm.Enabled = true;
                        bm.Name = "PictureBox" + (row * i + j + 1).ToString();
                        bm.MouseDown += new MouseEventHandler(form1.pictureBox_Click);
                        bm.SizeMode = PictureBoxSizeMode.StretchImage;
                        this.Controls.Add(bm);
                        s = string.Join(string.Empty, z[0].Skip(2)); z[0] = s; // remove first two characters in each row that indicates the row and the column

                        if (z[(row * i + j)][0] == 'e')                               //it's surely movable&it's empty
                        {
                            bm.Image = null;
                            part[i, j] = new Movable();
                            part[i, j].box = bm as PictureBox;
                            part[i, j].direct.set_directions(z[(row * i + j)]);

                        }
                        else
                        {
                            string q = z[row * i + j];
                            if (q[q.Length - 1] == 'A')
                            {
                                q = q.Remove(q.Length - 1); // if level was passed befor you would not read 'A' that indicate that this level is passed in the name of picture
                                z[row * i + j] = q;
                            }

                            bm.Image = Image.FromFile("Project pictures//" + z[(row * i + j)] + ".jpg");


                            if (z[(row * i + j)][0] == 'F')   // if image is fixed 
                            {

                                part[i, j] = new Fixed();
                                part[i, j].box = bm as PictureBox;
                                part[i, j].direct.set_directions(z[(row * i + j)]);
                            }
                            else       // if it's not fixed(movable)
                            {
                                part[i, j] = new Movable();
                                part[i, j].box = bm as PictureBox;
                                part[i, j].direct.set_directions(z[(row * i + j)]);
                            }

                        }

                        if (z[row * i + j][5] == 's') //to get the start point
                        {
                            part[i, j].is_start = true; sx = i; sy = j;
                        }
                        if (z[row * i + j][5] == 'e') //to get the end point
                        {
                            part[i, j].is_end = true; ex = i; ey = j;
                        }
                        int x = bm.Location.X;
                        int y = bm.Location.Y;
                        form1.Controls.Add(bm);
                        tr += 100;// every time we would increment temp row by 100 width of box

                    }//for
                    tr = 4;
                    tc += 100;// every time we would increment temp column by 100 hight of box 
                } // for

                // this picture box make frame that woudn't appear unless its parts that get out area of our pictureboxes
                PictureBox b = new PictureBox();
                b.Location = new Point(-1, 0);
                b.Size = new System.Drawing.Size(9 + row * 100, 5 + 2 + col * 100);
                b.Visible = true;
                b.Enabled = false;
                b.Name = "PictureBox";
                b.BackColor = Color.Red;
                b.SizeMode = PictureBoxSizeMode.StretchImage;
                form1.Controls.Add(b);
            }//try
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool  solve()       //to insure the path is solved
        {
            try
            {
                int x, y;
                x = sx;
                y = sy;

                frame f = new frame(this);

                while (true)
                {
                    if (f.part[x, y].direct.dawn == true)
                    {
                        if (x + 1 < row)
                        {
                            if (f.part[x + 1, y].direct.up == true)
                            {
                                x++;
                                f.part[x, y].direct.up = false;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else if (f.part[x, y].direct.up == true)
                    {
                        if (x - 1 >= 0)
                        {
                            if (f.part[x - 1, y].direct.dawn == true)
                            {
                                x--;
                                f.part[x, y].direct.dawn = false;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else if (f.part[x, y].direct.right == true)
                    {
                        if (y + 1 < col)
                        {
                            if (f.part[x, y + 1].direct.left == true)
                            {
                                y++;
                                f.part[x, y].direct.left = false;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else if (f.part[x, y].direct.left == true)
                    {
                        if (y - 1 >= 0)
                        {
                            if (f.part[x, y - 1].direct.right == true)
                            {
                                y--;
                                f.part[x, y].direct.right = false;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }

                    if (x == ex && y == ey)
                    {

                        return true;
                    }

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

               
        }
    
        public void set_frame ()
        {
            try
            {
                part = new Parts[row, col];
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {

                        part[i, j] = new Parts();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       public void Counter(ref Label l)
        {
            
            l.Text = Count_num.ToString();
        }

        public bool click(int a,int b)
        {
        
            
            part[a, b].check = true;        
            if (can_you_solve(a, b))
            {              
                return true;
            }
        
            return false;
        }

        public void checkbothclicked(int a, int b,ref int c,ref int d )
        {
            try
            {

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {

                        if (part[i, j].check && part[i, j].box.Name != part[a, b].box.Name)// to get the other clicked button and it is not a&b
                        {
                            c = i;
                            d = j;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }
        
        public void swap(ref Parts first,ref Parts second )
        {
            Image temp = first.box.Image;
            first.box.Image = second.box.Image;
            second.box.Image = temp;

            directions d = first.direct;
            first.direct = second.direct;
            second.direct = d;

        }

        public bool is_arround(int a, int b, int c, int d)
        {
            if ((a == c && b == d + 1) || (a == c && b == d - 1) || (a == c - 1 && b == d) || (a == c + 1 && b == d))
                return true;
            return false;
        }
     
        public bool can_you_solve(int a, int b)
        {
           try{
                int c = -1, d = -1;
                checkbothclicked(a, b, ref c, ref d);
              
                if (c != -1 && d != -1) // if two buttons are clicked 
                {
                    if (is_arround(a, b, c, d) && part[a, b].moving_option() && part[c, d].moving_option() && (part[a, b].box.Image == null || part[c, d].box.Image == null)) // they are around each other and both can move and one of two images has no picture
                    {
                        
                        part[a, b].check = false; part[c, d].check = false; // if he go true in solve it will never false pressed parts
                        swap(ref part[a, b], ref part[c, d]);
                        Count_num++;// it of course will make swap to reach here equivilant this line to put in swap function
                        
                        if (solve()) // check if it solved in every swap
                        {
                            Count_num++;
                            if (Count_num >= 12)
                            { MessageBox.Show("Badly Solved"); }
                            else
                            { MessageBox.Show("Well Solved"); }

                            return true;
                        }
                    }
                    part[a, b].check = false; part[c, d].check = false;
                }

                return false;
        }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               return false;
           }
        
        }
        
    }
      
    public class operations : Control
    {
        Thread th;
        public void opennewform2(object obj)
        {
            try
            {
                Application.Run(new Form2());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void opennewform1(object obj)
        {
            try
            {
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void opennewform3(object obj)
        {
            try
            {
                Application.Run(new Form3());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void switch_form2_to_form1(Form2 f)
        {
            try
            {
                f.Close();
                th = new Thread(opennewform1);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void switch_form2_to_form3(Form2 f)
        {
            f.Close();
            th = new Thread(opennewform3);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

       public void switch_form3_to_form2(Form3 f)
        {
            try
            {
                f.Close();
                th = new Thread(opennewform2);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       public void set_index_file(int level)
       {
           FileStream fs = new FileStream("Index.txt", FileMode.Append);
           StreamWriter r = new StreamWriter(fs);
           r.WriteLine(level.ToString());
           r.Close();
           fs.Close();
       }

       private bool the_past_level_passed(int level)
       {

           FileStream fs = new FileStream("Levels pictures.txt", FileMode.Open);
           StreamReader r = new StreamReader(fs);

           if (level == 0)
           {
               r.Close();
               fs.Close();
               return true;
           }
           else
           {
               int o = 0;
               string s;
               level--;

               while (true)
               {

                   if (o == level)
                   {
                       s = r.ReadLine();
                       break;
                   }
                   r.ReadLine();
                   o++;
               }
               if (s[s.Length - 1] == 'A')
               {
                   r.Close();
                   fs.Close();
                   return true;
               }
           }

           r.Close();
           fs.Close();
           return false;
       }

       public void Enable_passed_levels(Form2 f)
       {
           for (int i = 1; i <= 2; i++) // because we make 3 levels only starting from 0 you can make it for 100 level
           {
               //   MessageBox.Show("check if past  level is past " + (i-1));
               if (the_past_level_passed(i))
               {
                   //  MessageBox.Show("levels" + i);
                   f.Controls["button" + (4 + i)].Enabled = true; // 4 bec. we started from picture box 4
               }
           }
       }

       public void switch_form1_to_form2(Form1 f)
       {
           try
           {
               f.Close();
               th = new Thread(opennewform2);
               th.SetApartmentState(ApartmentState.STA);
               th.Start();
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
       }

       public void update_index_file()// if level is solved put A = accepted in file 
       {
           try
           {
               List<string> list = File.ReadAllLines("Levels pictures.txt").ToList();
               FileStream k = new FileStream("Index.txt", FileMode.Open);
               StreamReader l = new StreamReader(k);

               string r = "";
               while (l.Peek() != -1)// if file is not empty read the last line that indicate what level is pressed
               { 
                   r = l.ReadLine(); 
               }
               int a = int.Parse(r);
               string s = list[a];
               if (s[s.Length - 1] != 'A')
               {
                   s += 'A';
                   list[a] = s;
               }

               k.SetLength(0);
               File.WriteAllLines("Levels pictures.txt", list.ToArray()); // update for file by adding accepted'A' to solved level
              
               l.Close();
               k.Close();
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }

       }

    }


    public class directions
    {


        public bool up { set; get; }
        public bool dawn { set; get; }
        public bool right { set; get; }
        public bool left { set; get; }

        public void set_directions(string name)
        {
            if (name[1] == 't') {up = true;}
            if (name[2] == 't') {dawn = true;}
            if (name[3] == 't') {right = true;}
            if (name[4] == 't') {left = true;}   
        }

        public directions()
        { up = dawn = right = left = false; }

       public  directions(directions di)
        {
                    
            this.up = di.up;
            this.dawn = di.dawn;
            this.left = di.left;
            this.right = di.right;
        }

       public directions(bool up, bool dawn, bool right, bool left)
       {
           this.up = up;
           this.dawn = dawn;
           this.left = left;
           this.right = right;
       }
    }

    public class Parts 
    {
      public directions direct;
     
      public PictureBox box{ set;get;}   
      public bool check { set; get; }
      public bool is_start { set; get; }
      public bool is_end { set; get; }
      public Parts() { check = false; direct = new directions();  }
      public Parts(PictureBox b) { box = b; check =is_start = is_end = false; direct = new directions(); }

      public Parts(PictureBox b, bool is_s, bool is_e, bool ch, bool u, bool d, bool r, bool l)
      {
          this.box = b;
          this.is_start = is_s;
          this.is_end = is_e;
          this.check = ch;
         this.direct = new directions(u, d, r, l);
      }

      public Parts(Parts d)
      {
          this.box = d.box;
          this.check = d.check;
          this.direct = new directions(d.direct.up,d.direct.dawn,d.direct.right,d.direct.left);
          this.is_start = d.is_start;
          this.is_end = d.is_end;
      }
       
      public Parts copy_constructor()
      {
          Parts p = new Parts(this.box, this.is_start, this.is_end, this.check, direct.up, direct.dawn, direct.right, direct.left);
          return p;
      }
         
      public virtual bool moving_option() { return false; } 
    }
    
   public  class  Movable : Parts
   {
       public override bool moving_option()
       { return true; }

   }

   public class Fixed : Parts
   {
       public override bool moving_option()
       { return false; }
   }
    
}
