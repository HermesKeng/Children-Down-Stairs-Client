using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DownStairs
{
    public class player
    {
        public PictureBox human;
        public ProgressBar HP;
        bool jump;
        int G = 17;
        int force = 1;

        public player(PictureBox new_player)    //***************************
        {
            human = new_player;
        }

        public void start(Panel mainpage)
        {
            /*
            human = new PictureBox();
            // human.BackColor = System.Drawing.Color.Black;
            human.BackColor = Color.Black;

            human.Location = new System.Drawing.Point(100, 300);
            human.Size = new Size(25, 25);
            human.Visible = true;
            mainpage.Controls.Add(human);
            */

        }
        public int Left_pos
        {
            get
            {
                return human.Left;
            }
            set
            {
                human.Left = value;
            }
        }
        public int Right_pos
        {
            get
            {
                return Left_pos + Width;
            }

        }
        public int Top_pos
        {
            get
            {
                return human.Top;
            }
            set
            {
                human.Top = value;
            }
        }//取得y座標
        public int Bottom_pos//取得y座標
        {
            get
            {
                return Top_pos + Height;
            }

        }
        public int Width
        {
            get
            {
                return human.Width;
            }
        }
        public int Height
        {
            get
            {
                return human.Height;
            }
        }
        public int Crush_Type
        {
            get;
            set;

        }
        public bool hp { get; set; }
        public bool Move_Jump
        {
            get
            {
                return jump;
            }
            set
            {
                jump = value;
                force = G;
            }
        }
        public bool Collision { get; set; }
        public bool Dropping { get; set; }
        public bool Move_Right { set; get; }
        public bool Move_Left { set; get; }
        public void Move(Panel mainpage)
        {

            if (Move_Left && Move_Right)
            {
                return;
            }

            if (Move_Right)
            {
                human.Left += 5;


            }

            if (Move_Left)
            {
                human.Left -= 5;

            }


        }//判斷移動
        public void Drop(Panel mainpage)
        {



            if (human.Bottom > mainpage.Bottom)
            {
                human.Top = mainpage.Bottom - human.Width;
                Crush_Type = 0;
                Dropping = false;
            }

            if (human.Bottom < mainpage.Bottom)
            {
                human.Top += force;

                if (force < 20)
                {
                    force++;
                    Move_Jump = false;
                    Dropping = true;
                }

            }

        }
        public void Jump(Panel mainpage)
        {
            if (Move_Jump)
            {

                Dropping = false;
                
                if (human.Bottom <= mainpage.Bottom)
                {
                    human.Top -= force;
                    force--;
                }
                /*
                else
                {
                    human.Top = mainpage.Height - human.Height;
                    Move_Jump = false;
                }
                */
            }
            else
            {
                G = 1;
                human.Top += G;
            }


        }
        public void Rebound(Panel mainpage)
        {
            int a = 3;
            human.Top += a;
        }
        public void Stop(gro ground)
        {
            Top_pos = ground.get_Y() - Height;


            force = 0;      //新增     讓人離開版子掉落初速為零
            Dropping = false;


        }

        public void up()        //新增    配合地圖往上的函式
        {
            Size s = new Size();
            s.Width = human.Location.X;
            s.Height = human.Location.Y;

            human.Location = Point.Add(new Point(0, -1), s);

        }

    }
}
