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
using System.Text.RegularExpressions;
using System.Diagnostics;
using DownStairs;
using global::Children_Down_Stairs_Client.Properties;

namespace Children_Down_Stairs_Client
{
    public partial class GameOnlineForm : Form
    {
        private StartForm myStartForm;
        private CharactorChooseOnlineForm myChooseOnlineForm;
        private Byte[] mainBytes;
        private Byte[] threadBytes;
        private MoveInfo[] MyMoveInfoArray;
        private int NumberOfConnection;

        Map DownStairs;
        ulong frame;

        private int TestCount = 100;
        private int count = 0;
        private bool inGame = true;

        public GameOnlineForm(StartForm startForm, CharactorChooseOnlineForm chooseOnlineForm)
        {
            myStartForm = startForm;
            myChooseOnlineForm = chooseOnlineForm;
            NumberOfConnection = myStartForm.numberOfPlayers;
            InitializeComponent();
            recevBackgroundWorker.RunWorkerAsync();
            MyMoveInfoArray = new MoveInfo[NumberOfConnection];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void receByteToGame()
        {
            /*
            mainBytes = myStartForm.myClientNetwork.recvBytes();
            
            TestForMap.AppendText("Receive server package , mainBytes[0] ==" + mainBytes[0]);
            
            MyMoveInfoArray = bytesToMapInfo(mainBytes);
            
            count++;
            TestForMap.AppendText(" count :"+ count.ToString() + Environment.NewLine);
            for (int i = 0; i < NumberOfConnection; i++)
            {
                TestForMap.AppendText(" Move" + i + MyMoveInfoArray[i].ToString() + Environment.NewLine);
            }
            TestForMap.AppendText(Environment.NewLine);*/

            mainBytes = myStartForm.myClientNetwork.recvBytes();
            bytesToMapInfo(mainBytes);
        }

        public MoveInfo[] bytesToMapInfo(Byte[] mybyte)
        {
            string[] MoveString = new string[NumberOfConnection];
            string GroundString = "";
            string[] sArray;

            int mybyteArrayLength = 6;
            for (int i = 0; i < NumberOfConnection; i++)
            {
                MoveString[i] = Encoding.Unicode.GetString(mainBytes, mybyteArrayLength, mainBytes[i + 1]);
                mybyteArrayLength += mainBytes[i + 1];

                sArray = Regex.Split(MoveString[i], ",", RegexOptions.IgnoreCase);
                bool[] rv = new bool[3];
                int count = 0;
                foreach (string j in sArray)
                {
                    if (j.Equals("True"))
                    {
                        rv[count] = true;
                    }
                    else
                    {
                        rv[count] = false;
                    }
                    count++;
                }
                MyMoveInfoArray[i] = new MoveInfo(rv[0], rv[1], rv[2]);
            }
            GroundString = Encoding.Unicode.GetString(mainBytes, mybyteArrayLength, mainBytes[5]);
            sArray = Regex.Split(GroundString, ",", RegexOptions.IgnoreCase);
            int gro_x = Convert.ToInt32(sArray[0]);
            int gro_type = Convert.ToInt32(sArray[1]);
            if(gro_x != -1)
            {
                DownStairs.SetGorund(new GroundInfo(gro_x, gro_type));
            }

            return MyMoveInfoArray;
        }

        private void GameOnlineForm_Load(object sender, EventArgs e)
        {
            DownStairs = new Map(MapPanel,this);
            (MapPanel as Control).KeyDown += new KeyEventHandler(MapPanel_KeyDown);
            (MapPanel as Control).KeyUp += new KeyEventHandler(MapPanel_KeyUp);
        }

        private void MapPanel_KeyDown(Object sender, KeyEventArgs e)
        {
            DownStairs.KeyDown(e);

        }
        private void MapPanel_KeyUp(Object sender, KeyEventArgs e)
        {
            DownStairs.KeyUp(e);
        }

        private void GameOnlineForm_Shown(object sender, EventArgs e)
        {
            playBackgro();

            DownStairs.SetMaxPlayer(myChooseOnlineForm.numberOfConnection);

            for(int i = 0; i < NumberOfConnection; i++)
            {
                bool control = false;
                Bitmap playerPic;
                int indexCounter = 0;
                if (myChooseOnlineForm.myPlayerIndex == i)
                {
                    playerPic = Map.playerBitmap[myChooseOnlineForm.myCharactorNumber - 1];
                    control = true;
                }
                else
                {
                    playerPic = Map.playerBitmap[myChooseOnlineForm.otherCharactorNumber[indexCounter] - 1];
                    indexCounter++;
                }
                DownStairs.Fill_peopleList(playerPic, control);

                switch (i)
                {
                    case 0:
                        P1Label.Enabled = true;
                        break;
                    case 1:
                        P2Label.Enabled = true;
                        break;
                    case 2:
                        P3Label.Enabled = true;
                        break;
                    case 3:
                        P4Label.Enabled = true;
                        break;
                }
            }

            MapPanel.Focus();

            DownStairs.MapStart();

            MapPanel.Focus();

            MapTimer.Enabled = true;

            count = 0;
        }

        private void MapTimer_Tick(object sender, EventArgs e)
        {      
            frame = DownStairs.MapUpdate();
            if(frame == ulong.MaxValue)
            {
                DownStairs.MapEnd();
                myStartForm.myClientNetwork.sendEndGame(); 
                MapTimer.Enabled = false;
                EndButton.Visible = true;
                EndLabel.Visible = true;
                return;
            }
            MoveInfo MI = DownStairs.GetPeopleMove();
            myStartForm.myClientNetwork.sendMoveInfo(myChooseOnlineForm.myPlayerIndex, MI);
            receByteToGame();
            DownStairs.SetPeopleMove(MyMoveInfoArray);
        }

        public void playBackgro()
        {
            axWindowsMediaPlayer1.URL = "Lulu Theme.mp3";
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.Ctlcontrols.play();
            axWindowsMediaPlayer1.Visible = false;
        }

        private void GameOnlineForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            myChooseOnlineForm.Close();
        }
    }
}
