using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Children_Down_Stairs_Client {

    public partial class CharactorChooseSingleForm : Form {

        private int myCharactorNumber;
        private StartForm myStartForm;
        private Random random = new Random();
        private Bitmap myBitMap;

        public CharactorChooseSingleForm(string ID, StartForm startForm) {
            InitializeComponent();
            IDTextBox.Text = ID;
            myStartForm = startForm;
        }

        private void charactorPictureBox_Click(object sender, EventArgs e) {
            pictureNameToNumber((sender as PictureBox).Name);
        }

        private void randomButton_Click(object sender, EventArgs e) {
            int number;
            do {
                number = random.Next(1, 7);
            } while (number == myCharactorNumber);
            chooseCharactor(number);
        }

        private void OKButton_Click(object sender, EventArgs e) {
            if (myBitMap == null)
            {
                myBitMap = Properties.Resources.character1;
            }
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            axWindowsMediaPlayer1.Dispose();

            GameSingleForm gameSingleForm = new GameSingleForm(myBitMap,this);
            gameSingleForm.Show();
            this.Hide();
        }

        private void chooseCharactor(int number) {
            switch (number) {
                case 1:
                    myCharactorPictureBox.Image = Properties.Resources.character1;
                    myBitMap = Properties.Resources.character1;
                    break;
                case 2:
                    myCharactorPictureBox.Image = Properties.Resources.character2;
                    myBitMap = Properties.Resources.character2;
                    break;
                case 3:
                    myCharactorPictureBox.Image = Properties.Resources.character3;
                    myBitMap = Properties.Resources.character3;
                    break;
                case 4:
                    myCharactorPictureBox.Image = Properties.Resources.character4;
                    myBitMap = Properties.Resources.character4;
                    break;
                case 5:
                    myCharactorPictureBox.Image = Properties.Resources.character5;
                    myBitMap = Properties.Resources.character5;
                    break;
                case 6:
                    myCharactorPictureBox.Image = Properties.Resources.character6;
                    myBitMap = Properties.Resources.character6;
                    break;
            }
            myCharactorNumber = number;
        }

        private void pictureNameToNumber(String name) {
            int number = 1;
            switch (name) {
                case "charactor_1_PictureBox":
                    number = 1;
                    break;
                case "charactor_2_PictureBox":
                    number = 2;
                    break;
                case "charactor_3_PictureBox":
                    number = 3;
                    break;
                case "charactor_4_PictureBox":
                    number = 4;
                    break;
                case "charactor_5_PictureBox":
                    number = 5;
                    break;
                case "charactor_6_PictureBox":
                    number = 6;
                    break;
            }
            chooseCharactor(number);
        }

        private void CharactorChooseSingleForm_FormClosing(object sender, FormClosingEventArgs e) {
            myStartForm.Close();
        }
        public void playBackgro()
        {
            axWindowsMediaPlayer1.URL = "ChmpSlct_BlindPick.mp3";
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.Ctlcontrols.play();
            axWindowsMediaPlayer1.Visible = false;
        }

        private void CharactorChooseSingleForm_Shown(object sender, EventArgs e)
        {
            playBackgro();
        }
    }
}
