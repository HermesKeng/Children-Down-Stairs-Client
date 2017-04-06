using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using global::Children_Down_Stairs_Client.Properties;
using sound;

namespace DownStairs
{
    public class Map
    {
        private music MapSound = new music();
        private bool Hurt = true;

        public bool single = false;
        public int singleLife = 3;
        private List<peopleFill> peopleBuffer;
        private bool[] moveBuffer;
        private List<player> peopleList;
        private Queue<gro> groundQueue;
        private int maxPlayer;
        private int currentPlayer;
        private int groundSeriesNum;
        private int stepHeight = 150;
        private int stepCounter = 0;
        public int ControlNum = 0;
        private static Bitmap[] groundBitmap = new Bitmap[5]
        {Resources.bar0,Resources.bar1,Resources.bar2,Resources.bar3,Resources.bar4};
        public static Bitmap[] playerBitmap = new Bitmap[6]
        {Resources.character1,Resources.character2,Resources.character3,Resources.character4,Resources.character5,Resources.character6};

        private PictureBox LeftWall;
        private PictureBox RightWall;
        private PictureBox roof;


        private bool FirstGround;
        private ulong Frame;

        int leftFrame = 15;
        int rightFrame = 15;

        private bool inTheGame;
        private int dieNum = 0;

        Panel MapPanel;
        Form MapForm;
        private int[,] MapPosition;
        private Point Map_Pos;              //地圖的左上角點
        private Size Map_Size;              //地圖的大小

        private static int people_width = 30;
        private static int people_height = 36;
        private static int ground_width = 175;
        private static int ground_height = 25;


        /*
        public Map()            //若不給則用預設
        {
            intialize();
            MapPosition = new int[800, 700];
            Map_Pos = new Point(12, 12);
            Map_Size = new Size(800, 700);
        }
        public Map(Point mapPos, Size mapSize)
        {
            intialize();
            MapPosition = new int[mapSize.Width, mapSize.Height];
            Map_Size = mapSize;
            Map_Pos = mapPos;
        }
        */
        public Map(Panel mapPanel,Form mapform)
        {
            intialize();
            MapPosition = new int[mapPanel.Size.Width, mapPanel.Size.Height];
            Map_Size = mapPanel.Size;
            Map_Pos = mapPanel.Location;
            MapPanel = mapPanel;
            MapForm = mapform;
        }

        ~Map()
        {
            peopleList = null;
            groundQueue = null;
            MapPosition = null;
        }


        //public method
        public void SetMaxPlayer(int max)
        {
            if (peopleList.Count() < max)
            {
                maxPlayer = max;
            }
            else
            {
                Console.WriteLine("Map.SetMaxPlayer error : 目前人數大於想更改成的最高人數");
                Console.WriteLine("                         目前人數:" + peopleList.Count());
                Console.WriteLine("                         想更改的最高人數:" + max);
            }
        }
        public int Fill_peopleList(Bitmap character, bool control)
        {
            if (character == null)
            {
                return 1;
            }

            peopleBuffer.Add(new peopleFill(character, control));

            return 0;
        }
        public int SetPeopleMove(int number, MoveInfo info)
        {
            peopleList[number].Move_Jump = info.jump;
            peopleList[number].Move_Left = info.left;
            peopleList[number].Move_Right = info.right;

            return 0;
        }
        public int SetPeopleMove(MoveInfo[] infoArr)
        {
            
            for(int i = 0; i < maxPlayer; i++)
            {   
                if (!peopleList[i].Move_Jump)
                {
                    peopleList[i].Move_Jump = infoArr[i].jump;
                }
                peopleList[i].Move_Left = infoArr[i].left;
                peopleList[i].Move_Right = infoArr[i].right;
            }
            
            /*
            for (int i = 0; i < maxPlayer; i++)
            {
                if (!peopleList[i].Move_Jump)
                {
                    if (infoArr[i].jump == true)
                    {
                        peopleList[i].Move_Jump = true;
                    }
                    
                }
                if(infoArr[i].left == true)
                {
                    peopleList[i].Move_Left = true;
                }
                if (infoArr[i].right == true)
                {
                    peopleList[i].Move_Right = true;
                }
                
            }
            */

            return 0;
        }
        public int SetGorund(GroundInfo new_gro)
        {
            AddGround(new_gro.x, 700, new_gro.type, groundBitmap[new_gro.type]);
            return 0;
        }

        public MoveInfo GetPeopleMove()
        {
            //return new MoveInfo(peopleList[ControlNum - 1].Move_Jump, peopleList[ControlNum - 1].Move_Left, peopleList[ControlNum - 1].Move_Right);
            /*
            if (peopleList[ControlNum - 1].Move_Jump == true)
            {
                peopleList[ControlNum - 1].Move_Jump = false;
            }
            */
            return new MoveInfo(moveBuffer[0],moveBuffer[1],moveBuffer[2]);
        }
        public int MapStart()
        {
            LeftWall = makeGroundControl(LeftWall, MapBackground.LEFT);
            RightWall = makeGroundControl(RightWall, MapBackground.RIGHT);
            roof = makeGroundControl(roof, MapBackground.ROOF);




            int peopleRange = MapPanel.Size.Width / (peopleBuffer.Count + 1);

            for (int i = 0; i < peopleBuffer.Count; i++)
            {
                player temp_player = AddPeople(peopleRange * (i + 1), 100, peopleBuffer[i].control, peopleBuffer[i].pic);
                //makeHPControl(peopleRange * (i + 1), 30, temp_player);
                makeHPControl(760, 400 + i * 50, temp_player);
            }

            AddGround(ground_width * 0, 500, 100, Resources.bar0);
            AddGround(ground_width * 1, 500, 100, Resources.bar0);
            AddGround(ground_width * 2, 500, 100, Resources.bar0);
            AddGround(ground_width * 3, 500, 100, Resources.bar0);

            return 0;
        }
        public ulong MapUpdate()
        {
            GroundUpdate();
            int rv = PeopleUpdate();
            if(rv == 1)
            {
                return ulong.MaxValue;
            }
            Frame++;
            return Frame;
        }
        public int MapEnd()
        {
            for(int i = maxPlayer - 1; i>= 0; i--)
            {
                RemovePeople(peopleList[i]);
            }
            while(groundQueue.Count > 0)
            {
                RemoveGround(groundQueue.First());
            }
            return 0;
        }
        public int KeyDown(KeyEventArgs e)      //參考楷甯
        {
            if(single == true)
            {
                try
                {
                    if (ControlNum == 0)
                    {
                        return 1;
                    }

                    if (e.KeyCode == Keys.D)
                    {
                        peopleList[ControlNum - 1].Move_Right = true;

                    }
                    if (e.KeyCode == Keys.A)
                    {
                        peopleList[ControlNum - 1].Move_Left = true;

                    }
                    /*
                    if (!peopleList[ControlNum - 1].Move_Jump)
                    {
                        if (e.KeyCode == Keys.W)
                        {
                            peopleList[ControlNum - 1].Move_Jump = true;
                        }
                    }
                     * */
                }
                catch
                {

                }

                return 0;
            }
            else
            {
                try
                {
                    if (ControlNum == 0)
                    {
                        return 1;
                    }

                    if (e.KeyCode == Keys.D)
                    {
                        moveBuffer[2] = true;

                    }
                    if (e.KeyCode == Keys.A)
                    {
                        moveBuffer[1] = true;

                    }
                    /*
                    if (!peopleList[ControlNum - 1].Move_Jump == true)
                    {

                        if (e.KeyCode == Keys.W)
                        {
                     * */
                            /*
                            if(moveBuffer[0] == true)
                            {
                                moveBuffer[0] = false;
                            }
                            else
                            {
                            */
                            //peopleList[ControlNum - 1].Move_Jump = true;
                            /*
                        }
                        */
                       // }
                        //if (peopleList[ControlNum - 1].Move_Jump == true)
                        //{
                        //    moveBuffer[0] = false;
                        // }

                   // }
                }
                catch
                {

                }

                return 0;
            }

            
        }
        public int KeyUp(KeyEventArgs e)      //參考楷甯
        {
            if (single)
            {
                try
                {
                    if (ControlNum == 0)
                    {
                        return 1;
                    }

                    if (e.KeyCode == Keys.D)
                    {
                        peopleList[ControlNum - 1].Move_Right = false;

                    }
                    if (e.KeyCode == Keys.A)
                    {
                        peopleList[ControlNum - 1].Move_Left = false;

                    }
                }
                catch
                {

                }

                return 0;
            }
            else
            {
                try
                {
                    if (ControlNum == 0)
                    {
                        return 1;
                    }

                    if (e.KeyCode == Keys.D)
                    {
                        moveBuffer[2] = false;

                    }
                    if (e.KeyCode == Keys.A)
                    {
                        moveBuffer[1] = false;

                    }
                }
                catch
                {

                }

                return 0;
            }
            
        }





        //private method
        private void intialize()
        {
            peopleBuffer = new List<peopleFill>();
            moveBuffer = new bool[3];
            peopleList = new List<player>();
            groundQueue = new Queue<gro>();
            maxPlayer = 5;
            currentPlayer = 0;
            groundSeriesNum = 0;
            stepHeight = 150;
            FirstGround = true;
            Frame = 0;
            inTheGame = false;
        }



        private int GroundUpdate()
        {

            Stack<gro> groTrashCan = new Stack<gro>();
            bool isPop = false;

            if (Frame == 400)
            {
                RemoveGround(groundQueue.First());
                RemoveGround(groundQueue.First());
                RemoveGround(groundQueue.First());
                RemoveGround(groundQueue.First());
            }

            if (single)
            {
                if (stepCounter == stepHeight)
                {
                    Random rnd = new Random(Guid.NewGuid().GetHashCode());

                    int rnd_x = rnd.Next() % 425;

                    int rnd_ground = rnd.Next() % 5;

                    stepCounter = 0;

                    AddGround(rnd_x, 700, rnd_ground, groundBitmap[rnd_ground]);
                }
                else
                {
                    stepCounter++;
                }
            }

            foreach (gro ground in groundQueue)
            {
                ground.up();

                if (ground.get_Y() == -ground_height)
                {
                    isPop = true;
                }
            }


            if (isPop == true)
            {
                gro tempGro = groundQueue.First();

                RemoveGround(tempGro);
            }


            return 0;
        }
        private int PeopleUpdate()
        {

           
            Stack<player> peoTrashCan = new Stack<player>();

            foreach (player peo in peopleList)
            {
                if (CheckPeopleDie(peo))
                {
                    if (single)
                    {
                        if (singleLife == 0)
                        {
                            continue;
                        }
                        peo.HP.Value = 100;
                        peo.Left_pos = groundQueue.First().get_X() + ground_width/2;
                        peo.Top_pos = groundQueue.First().get_Y() - 10;
                    }
                    else
                    {
                        continue;
                    }
                    
                }

                peo.up();
                bool drop = true;
                bool touchWall = false;
                if (peo.Move_Jump)  //
                {
                    peo.Jump(MapPanel);//跳一下

                }

                if (CheckWall(peo))
                {
                    touchWall = true;
                }

                foreach (gro ground in groundQueue)
                {
                    detector(peo, ground);   //參考楷甯

                    if (ground.collision && peo.Collision)  //參考楷甯
                    {
                        if (touchWall == false)
                        {
                            detector_x_dir(peo);
                        }

                        detector_y_dir(peo, ground);
                        detect_gro(peo, ground);
                        drop = false;
                        break;
                    }
                    /*
                    else if (peo.Collision)//================5/28更新==========================
                    {
                        detector_x_dir(peo);//================5/28更新==========================//
                        drop = true;
                        detector_y_dir(peo, ground);//================5/28更新==========================
                                                    // detect_gro(p1, g1[i]);================5/28更新==========================
                        break;//================5/28更新==========================
                    }//================5/28更新==========================
                    */


                    /*
                    detector_x_dir(peo);
                    detector_y_dir(peo, ground);
                    if(peo.Crush_Type != 0)
                    {
                        break;
                    }
                    */

                }


                if (drop == true)  //參考楷甯
                {
                    if (touchWall == false)
                    {
                        detector_x_dir(peo);
                    }
                    detector_y_dir(peo, groundQueue.Last());
                    peo.hp = true;
                }



                if (CheckRoof(peo))      //判斷是否碰到頂針
                {
                    MapSound.playHurt();
                    if (peo.HP.Value < 30)
                    {
                        peo.HP.Value = 0;
                    }
                    else {
                        peo.HP.Value -= 30;
                        peo.Top_pos += (ground_height + people_height);
                    }
                }

                if (CheckHell(peo))
                {
                    
                    peo.HP.Value = 0;
                }

                if (CheckPeopleDie(peo))
                {
                    peoTrashCan.Push(peo);
                }

                /*
                if (drop == true)
                {
                    peo.Drop(MapPanel);
                }
                */
            }

            if (peoTrashCan.Count > 0)
            {
                MapSound.playDeath();
                player temp;

                temp = peoTrashCan.Pop();

                int tempNum = Convert.ToInt32(temp.human.Name.ToString().Substring(7));

                /*if (tempNum == ControlNum)
                {
                    ControlNum = 0;
                }*/

                /*
                if (tempNum < ControlNum)
                {
                    ControlNum--;
                }
                */

                //if()

                if (single)
                {
                    
                    singleLife--;
                    if (singleLife == 0)
                    {
                        dieNum++;
                        temp.human.Hide();
                    }
                }
                else
                {
                    dieNum++;
                    temp.human.Hide();
                }
                

                //RemovePeople(temp);
            }
            moveBuffer[0] = peopleList[ControlNum - 1].Move_Jump;

            if(dieNum == maxPlayer)
            {
                return 1;
            }

            return 0;
        }

        private player AddPeople(int x, int y, bool Control, Bitmap character)
        {
            if (inTheGame)
            {
                Console.WriteLine("Map.AddPeople error : 遊戲開始  無法增加人數");
                return null;
            }

            if (peopleList.Count() >= maxPlayer)
            {
                Console.WriteLine("Map.AddPeople error : 已超過最大人數");
                return null;
            }




            currentPlayer++;
            PictureBox newPic = makePeopleControl(x, y, character);

            player new_people = new player(newPic);

            peopleList.Add(new_people);

            if (Control == true)
            {
                if (ControlNum == 0)
                {
                    ControlNum = currentPlayer;
                }
            }


            return new_people;
        }
        private int RemovePeople(player PRemoved)
        {
            bool rv;
            rv = peopleList.Remove(PRemoved);
            if (rv == false)
            {
                Console.WriteLine("Map.RemovePeople error : 找不到指定要刪除的人");

                return 1;
            }

            this.MapPanel.Controls.RemoveByKey(PRemoved.human.Name);
            PRemoved = null;

            return 0;
        }

        private int AddGround(int x, int y, int type, Bitmap g_bitmap)
        {
            groundSeriesNum++;

            gro new_gro = new gro(x, y, type, MapPanel);

            PictureBox newPic = makeGroundControl(new_gro, g_bitmap);


            new_gro.picBox = newPic;

            groundQueue.Enqueue(new_gro);
            return 0;
        }
        private int RemoveGround(gro GRemoved)
        {

            bool rv;
            rv = GRemoved.Equals(groundQueue.Dequeue());
            if (rv == false)
            {
                Console.WriteLine("Map.RemovePeople error : 找不到指定要刪除的地板");

                return 1;
            }

            this.MapPanel.Controls.RemoveByKey(GRemoved.picBox.Name);
            GRemoved = null;
            return 0;
        }

        private void detector_x_dir(player player)
        {
            /*
            if (player.Move_Right)//判斷是否向右
            {
            */
            player.Move(MapPanel);
            /*
            }
            if (player.Move_Left)//判斷是否向左
            {
                player.Move(MapPanel);
            }
            */
        }
        private void detector_y_dir(player player, gro ground)
        {

            if (!player.Move_Jump)
            {
                if (player.Crush_Type == 0 && !player.Collision && !ground.collision)
                {
                    player.Drop(MapPanel);

                }

                if (player.Crush_Type == 1 && player.Collision && ground.collision)
                {



                    player.Stop(ground);


                }
                if (player.Crush_Type == 2 && player.Collision && !ground.collision)
                {
                    player.Rebound(MapPanel);

                }
            }
        }
        private void detector(player player, gro ground)
        {
            player.Crush_Type = 0;
            player.Collision = false;
            ground.collision = false;



            if (player.Right_pos > ground.get_X() && player.Bottom_pos > ground.get_Y() && ground.get_X() + ground.get_width() > player.Right_pos && player.Bottom_pos < ground.get_Y() + ground.get_height())
            {
                player.Move_Right = false;
                // MessageBox.Show("" + player.Move_Right);

            }
            if (player.Left_pos < ground.get_X() + ground.get_width() && player.Bottom_pos > ground.get_Y() && player.Right_pos > ground.get_X() && player.Bottom_pos < ground.get_Y() + ground.get_height())
            {
                player.Move_Left = false;
            }
            if (player.Right_pos > ground.get_X() && player.Right_pos < ground.get_X() + ground.get_width() + player.Width && player.Bottom_pos >= ground.get_Y() && player.Top_pos < ground.get_Y())
            {
                player.Move_Jump = false;
                //moveBuffer[0] = false;
                //MessageBox.Show("" + player.Move_Jump);
                player.Crush_Type = 1;
                player.Collision = true;
                ground.collision = true;
                //掉落case 1
            }
            if (player.Right_pos > ground.get_X() && player.Left_pos < ground.get_X() + ground.get_width() && player.Top_pos < ground.get_Y() + ground.get_height() && player.Top_pos > ground.get_Y())
            {
                if (player.Top_pos < ground.get_Y() + ground.get_height())
                {
                    player.Crush_Type = 2;
                    player.Move_Jump = false;
                    player.Collision = true;
                    ground.collision = false;
                    //掉落case 2

                }

            }

        }

        private void detect_gro(player player, gro ground)
        {
            if (ground.get_type() == 0)
            {
                if (player.HP.Value < 100 && player.hp)
                {
                    player.HP.Value += 10;
                    player.hp = false;
                }
            }
            else if (ground.get_type() == 1)
            {   
                if (player.HP.Value > 0 && player.hp)
                {
                    MapSound.playHurt();
                    if (player.HP.Value < 30)
                    {
                        player.HP.Value = 0;
                    }
                    else
                    {
                        player.HP.Value -= 30;
                        player.hp = false;
                    }
                }
            }
            else if (ground.get_type() == 2)
            {
                MapSound.playSpring();
                player.Move_Jump = true;
                player.Jump(MapPanel);
            }
            else if (ground.get_type() == 3)
            {
                if (LeftWall.Right > player.Left_pos - 5)
                {
                    return;
                }
                if (player.Right_pos + 5 > RightWall.Left)
                {
                    return;
                }

                player.Left_pos += 3;

            }
            else if (ground.get_type() == 4)
            {
                if (LeftWall.Right > player.Left_pos - 5)
                {
                    return;
                }
                if (player.Right_pos + 5 > RightWall.Left)
                {
                    return;
                }
                player.Left_pos -= 3;

            }
        }

        private PictureBox makePeopleControl(int x, int y, Bitmap character)
        {
            PictureBox newPic = new PictureBox();
            newPic.Image = character;
            newPic.Name = "player_" + currentPlayer.ToString();//new_people.name;
            newPic.Location = new Point(x, y);//GetCenterPoint(new Point(0,0), Map_Size);
            newPic.Size = new Size(people_width, people_height);
            newPic.Text = "testPeo";
            newPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;


            this.MapPanel.Controls.Add(newPic);

            return newPic;
        }
        private PictureBox makeGroundControl(gro new_ground, Bitmap g_bitmap)
        {
            PictureBox newPic = new PictureBox();
            newPic.Image = g_bitmap;
            newPic.Name = "ground_" + groundSeriesNum.ToString();
            newPic.Location = new Point(new_ground.get_X(), new_ground.get_Y()); //new_ground.pos;//
            newPic.Size = new Size(ground_width, ground_height);
            newPic.Text = "testGro";
            newPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;


            this.MapPanel.Controls.Add(newPic);

            return newPic;
        }

        private PictureBox makeGroundControl(PictureBox MapBackground, MapBackground MapBG)
        {
            PictureBox newPic = new PictureBox();

            if (MapBG == DownStairs.MapBackground.LEFT)
            {
                newPic.Image = Resources.edge;
                newPic.Name = "LeftWall" + groundSeriesNum.ToString();
                newPic.Location = new Point(0, 0);
                newPic.Size = new Size(26, 700);
                newPic.Text = "LeftWall";
                newPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            }
            else if (MapBG == DownStairs.MapBackground.RIGHT)
            {
                newPic.Image = Resources.edge;
                newPic.Name = "RightWall" + groundSeriesNum.ToString();
                newPic.Location = new Point(574, 0);
                newPic.Size = new Size(26, 700);
                newPic.Text = "RightWall";
                newPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            }
            else
            {
                newPic.Image = Resources.roof;
                newPic.Name = "Roof" + groundSeriesNum.ToString();
                newPic.Location = new Point(35, 0);
                newPic.Size = new Size(535, 29);
                newPic.Text = "Roof";
                newPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            }



            this.MapPanel.Controls.Add(newPic);

            return newPic;
        }

        private ProgressBar makeHPControl(int x, int y, player HPer)
        {
            HPer.HP = new ProgressBar();
            HPer.HP.Name = "progressBar_new";
            HPer.HP.Location = new System.Drawing.Point(x, y);
            HPer.HP.Size = new Size(100, 25);
            HPer.HP.Value = 80;

            //this.MapPanel.Controls.Add(HPer.HP);
            this.MapForm.Controls.Add(HPer.HP);

            return HPer.HP;
        }

        private Point GetCenterPoint(Point StartPos, Size TotalSize)
        {
            Point center;

            int x = StartPos.X + TotalSize.Width / 2;
            int y = StartPos.Y + TotalSize.Height / 2;

            center = new Point(x, y);

            return center;
        }
        private Point GetStartPoint(Point center, Size TotalSize)
        {
            Point StartPos;

            int x = center.X - TotalSize.Width / 2;
            int y = center.Y - TotalSize.Height / 2;

            StartPos = new Point(x, y);

            return StartPos;
        }
        private bool CheckIntersect(Point UP_pos, Size UP_size, Point DOWN_pos, Size DOWN_size)
        {
            Size height = new Size(0, UP_size.Height);

            Point U_LDpoint = Point.Add(UP_pos, height);
            Point U_RDpoint = Point.Add(UP_pos, UP_size);
            int U_TOP = UP_pos.Y;


            Size width = new Size(DOWN_size.Width, 0);

            Point D_LUpoint = DOWN_pos;
            Point D_RUpoint = Point.Add(DOWN_pos, width);
            int D_BUTTOM = DOWN_pos.Y + DOWN_size.Height;


            //若UP的下排在DOWN的上排之上
            if (U_LDpoint.Y < D_LUpoint.Y)
            {

            }
            else
            {
                //若DOWN的上排在UP的下排之左
                if (D_RUpoint.X < U_LDpoint.X)
                {

                }
                else if (U_RDpoint.X < D_LUpoint.X)//若DOWN的上排在UP的下排之右
                {

                }
                else
                {
                    if (D_BUTTOM < U_TOP)//若DOWN的下排在UP的上排之上
                    {

                    }
                    else//重疊!!!!
                    {
                        return true;
                    }
                }
            }


            return false;
        }

        private bool CheckWall(player peo)
        {
            if (peo.Move_Left && peo.Move_Right)
            {
                return false;
            }

            if (peo.Move_Left)
            {
                if (LeftWall.Right > peo.Left_pos - 5)
                {
                    return true;
                }
            }
            else if (peo.Move_Right)
            {
                if (peo.Right_pos + 5 > RightWall.Left)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckRoof(player peo)
        {
            if (peo.Top_pos < roof.Bottom)
            {
                return true;
            }



            return false;
        }

        private bool CheckHell(player peo)
        {
            if (peo.Bottom_pos > MapPanel.Bottom)
            {
                return true;
            }

            return false;
        }

        private bool CheckPeopleDie(player peo)
        {
            if (peo.HP.Value <= 0)
            {
                return true;
            }

            return false;
        }



    }

    public class peopleFill
    {
        public bool control;
        public Bitmap pic;

        public peopleFill(Bitmap bit, bool con)
        {
            pic = bit;
            control = con;
        }
    }

    [Serializable]
    public class MoveInfo
    {
        public bool jump;
        public bool left;
        public bool right;

        public MoveInfo(bool jump, bool left, bool right)
        {
            this.jump = jump;
            this.left = left;
            this.right = right;
        }

        public string ToString()
        {
            string MoveString = jump.ToString() + "," + left.ToString() + "," + right.ToString();

            return MoveString;
        }
    }

    [Serializable]
    public class GroundInfo
    {
        public int x;
        public int type;

        public GroundInfo(int x, int type)
        {
            this.x = x;
            this.type = type;
            
        }

        public string ToString()
        {
            string GroundString = x.ToString() + "," + type.ToString();

            return GroundString;
        }
    }

    public enum MapBackground { LEFT, RIGHT, ROOF };
}
