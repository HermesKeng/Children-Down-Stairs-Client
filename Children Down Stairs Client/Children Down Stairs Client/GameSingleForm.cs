using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DownStairs;

namespace Children_Down_Stairs_Client
{
    public partial class GameSingleForm : Form
    {
        Bitmap myCharactorImage;
        Map downstairs;
        int life = 3;
        CharactorChooseSingleForm mySin;

        public GameSingleForm(Bitmap myImage,CharactorChooseSingleForm sin)
        {
            InitializeComponent();
            myCharactorImage = myImage;
            mySin = sin;
            
        }

        private void GameSingleForm_Load(object sender, EventArgs e)
        {
            downstairs = new Map(MapPanel, this);
            (MapPanel as Control).KeyDown += new KeyEventHandler(MapPanel_KeyDown);
            (MapPanel as Control).KeyUp += new KeyEventHandler(MapPanel_KeyUp);
        }
        private void MapPanel_KeyDown(Object sender, KeyEventArgs e)
        {
            downstairs.KeyDown(e);

        }
        private void MapPanel_KeyUp(Object sender, KeyEventArgs e)
        {
            downstairs.KeyUp(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ulong frame;
            frame = downstairs.MapUpdate();
            if(downstairs.singleLife < life)
            {
                life--;
                switch (life)
                {
                    case 2:
                        pictureBox4.Hide();
                        break;
                    case 1:
                        pictureBox3.Hide();
                        break;
                    case 0:
                        pictureBox2.Hide();
                        break;
                }                
            }
            if (frame == ulong.MaxValue)
            {
                downstairs.MapEnd();
                timer1.Enabled = false;
                EndButton.Visible = true;
                EndLabel.Visible = true;
                return;
            }
        }

        private void GameSingleForm_Shown(object sender, EventArgs e)
        {
            playBackgro();

            downstairs.SetMaxPlayer(1);

            downstairs.single = true;

            downstairs.Fill_peopleList(myCharactorImage, true);

            MapPanel.Focus();

            downstairs.MapStart();
        }

        public void playBackgro()
        {
            axWindowsMediaPlayer1.URL = "Lulu Theme.mp3";
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.Ctlcontrols.play();
            axWindowsMediaPlayer1.Visible = false;
        }

        private void EndButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GameSingleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mySin.Close();
        }
    }
}
