using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DownStairs
{
    public class gro
    {
        int x;
        int y;
        protected int typ;	//0:普通地板 1:帶刺地板 2:彈跳地板 3:往左移動的地板 4:往右移動的地板
        int width;
        int height;
        public PictureBox picBox;
        public gro(int a, int b, int ty, Panel panel)   //修改為傳panel
        {

            x = a;
            y = b;

            width = 175;
            height = 25;
            /*  
            picBox =new PictureBox();
			picBox.Location=new Point(a,b);
			picBox.Size=new Size(width,height);
			picBox.BackgroundImage=Image.FromFile(@"C:\Users\sandyhu\Documents\Rectangle.png");
			picBox.BackColor=Color.Black;
			form.Controls.Add(picBox);
            */
            switch (ty)
            {
                case 0:
                    typ = 0;
                    break;
                case 1:
                    typ = 1;
                    break;
                case 2:
                    typ = 2;
                    break;
                case 3:
                    typ = 3;
                    break;
                case 4:
                    typ = 4;
                    break;
            }

        }
        public int get_type()
        {
            return this.typ;
        }//取得地板的類別
        public int get_X()
        {
            return this.x;
        }
        public int get_Y()
        {
            return this.y;
        }
        public void up()
        {
            y -= 1;
            picBox.Location = new Point(x, y);
        }	//地板往上移
        public void changeSiz(int a, int b)
        {   //a:長 b:寬
            width = a;
            height = b;
            picBox.Size = new Size(width, height);
        }

        ~gro()
        {

        }


        //以下為Map與player需求所新增
        public int get_right()
        {
            return x + width;
        }
        public int get_bottom()
        {
            return y + height;
        }
        public int get_width()
        {
            return width;
        }
        public int get_height()
        {
            return height;
        }

        public bool collision { get; set; }


    }
}
