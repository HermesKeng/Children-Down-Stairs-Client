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

    public partial class CharactorChooseOnlineForm : Form {

        private StartForm myStartForm;
        public int numberOfConnection;
        public int myCharactorNumber { get; private set;}
        public int[] otherCharactorNumber { get; private set; }
        public int myPlayerIndex { get; private set;}
        public int[] otherPlayerIndex { get; private set;}
        private bool isReady = false;
        private Random random = new Random();
        private Byte[] mainBytes;
        private Byte[] threadBytes;

        private bool endReport = false;
        private bool endWorker = false;

        public CharactorChooseOnlineForm(String ID, StartForm startForm) {
            myStartForm = startForm;
            myCharactorNumber = 1;
            otherCharactorNumber = new int[myStartForm.numberOfPlayers - 1];
            otherPlayerIndex = new int[myStartForm.numberOfPlayers - 1];
            InitializeComponent();
            myIDTextBox.Text = ID;
            conversationTextBox.Text = "這是" + startForm.numberOfPlayers + "人遊戲";
            recevBackgroundWorker.RunWorkerAsync();
        }

        private void charactorPictureBox_Click(object sender, EventArgs e) {
            pictureNameToNumber((sender as PictureBox).Name);
        }

        private void chooseMyCharactor(int number) {
            switch (number) {
                case 1:
                    myCharactorPictureBox.Image = Properties.Resources.character1;
                    break;
                case 2:
                    myCharactorPictureBox.Image = Properties.Resources.character2;
                    break;
                case 3:
                    myCharactorPictureBox.Image = Properties.Resources.character3;
                    break;
                case 4:
                    myCharactorPictureBox.Image = Properties.Resources.character4;
                    break;
                case 5:
                    myCharactorPictureBox.Image = Properties.Resources.character5;
                    break;
                case 6:
                    myCharactorPictureBox.Image = Properties.Resources.character6;
                    break;
            }
            myCharactorNumber = number;
            myStartForm.myClientNetwork.charactorChangeSend(myCharactorNumber, myPlayerIndex);
        }

        private void chooseOtherCharactor(int index, int number) {
            for (int i = 0; i < numberOfConnection; i++) {
                if (index == otherPlayerIndex[i]) {
                    index = i;
                    break;
                }
            }
            switch (index) {
                case 0:
                    switch (number) {
                        case 1:
                            otherCharactorPictureBox1.Image = Properties.Resources.character1;
                            break;
                        case 2:
                            otherCharactorPictureBox1.Image = Properties.Resources.character2;
                            break;
                        case 3:
                            otherCharactorPictureBox1.Image = Properties.Resources.character3;
                            break;
                        case 4:
                            otherCharactorPictureBox1.Image = Properties.Resources.character4;
                            break;
                        case 5:
                            otherCharactorPictureBox1.Image = Properties.Resources.character5;
                            break;
                        case 6:
                            otherCharactorPictureBox1.Image = Properties.Resources.character6;
                            break;
                    }
                    break;
                case 1:
                    switch (number) {
                        case 1:
                            otherCharactorPictureBox2.Image = Properties.Resources.character1;
                            break;
                        case 2:
                            otherCharactorPictureBox2.Image = Properties.Resources.character2;
                            break;
                        case 3:
                            otherCharactorPictureBox2.Image = Properties.Resources.character3;
                            break;
                        case 4:
                            otherCharactorPictureBox2.Image = Properties.Resources.character4;
                            break;
                        case 5:
                            otherCharactorPictureBox2.Image = Properties.Resources.character5;
                            break;
                        case 6:
                            otherCharactorPictureBox2.Image = Properties.Resources.character6;
                            break;
                    }
                    break;
                case 2:
                    switch (number) {
                        case 1:
                            otherCharactorPictureBox3.Image = Properties.Resources.character1;
                            break;
                        case 2:
                            otherCharactorPictureBox3.Image = Properties.Resources.character2;
                            break;
                        case 3:
                            otherCharactorPictureBox3.Image = Properties.Resources.character3;
                            break;
                        case 4:
                            otherCharactorPictureBox3.Image = Properties.Resources.character4;
                            break;
                        case 5:
                            otherCharactorPictureBox3.Image = Properties.Resources.character5;
                            break;
                        case 6:
                            otherCharactorPictureBox3.Image = Properties.Resources.character6;
                            break;
                    }
                    break;
            }
            otherCharactorNumber[index] = number;
        }

        private void changeOtherPlayer(int index, String inID, int charactorNumber) {
            switch (index) {
                    case 0:
                        otherIDTextBox1.Visible = true;
                        otherIDTextBox1.Text = inID;
                        otherCharactorPictureBox1.Visible = true;
                        break;
                    case 1:
                        otherIDTextBox2.Visible = true;
                        otherIDTextBox2.Text = inID;
                        otherCharactorPictureBox2.Visible = true;
                        break;
                    case 2:
                        otherIDTextBox3.Visible = true;
                        otherIDTextBox3.Text = inID;
                        otherCharactorPictureBox3.Visible = true;
                        break;
            }
            chooseOtherCharactor(index, charactorNumber);
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
            chooseMyCharactor(number);
        }

        private void randomButton_Click(object sender, EventArgs e) {
            int number;
            do {
                number = random.Next(1, 7);
            } while (number == myCharactorNumber);
            chooseMyCharactor(number);
        }

        private void sendMessageButton_Click(object sender, EventArgs e) {
            if (myMessageTextBox.Text != "") {
                myStartForm.myClientNetwork.messageSend(myMessageTextBox.Text, myPlayerIndex);
                myMessageTextBox.Text = "";
            }
        }

        private void myMessageTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                sendMessageButton_Click(sender, e);
            }
        }

        private void okButton_Click(object sender, EventArgs e) {
            if (!isReady) {
                myStartForm.myClientNetwork.readySend();
            }
            okButton.Enabled = false;
        }

        private void CharactorChooseOnlineForm_FormClosing(object sender, FormClosingEventArgs e) {
            myStartForm.Close();
        }

        private void recevBackgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
            while (true) {
                
                while (endWorker == true)
                {
                    while(endReport == true)
                    {
                        return;
                    }
                }
                
                threadBytes = myStartForm.myClientNetwork.threadRecvBytes();
                if (threadBytes[0] == 5){
                    endWorker = true;
                }
                recevBackgroundWorker.ReportProgress(0);
                System.Threading.Thread.Sleep(100);
            }
        }

        private void recevBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            if (threadBytes[0] == 0) {
                myPlayerIndex = threadBytes[1];
                numberOfConnection = threadBytes[2];
            }else if (threadBytes[0] == 4) {
                otherPlayerIndex[threadBytes[1] - 1] = threadBytes[1];
                numberOfConnection = threadBytes[3];
                String inID = Encoding.Unicode.GetString(threadBytes, 4, threadBytes[2]);
                switch (threadBytes[1]) {
                    case 1:
                        otherIDTextBox1.Visible = true;
                        otherIDTextBox1.Text = inID;
                        otherCharactorPictureBox1.Visible = true;
                        break;
                    case 2:
                        otherIDTextBox2.Visible = true;
                        otherIDTextBox2.Text = inID;
                        otherCharactorPictureBox2.Visible = true;
                        break;
                    case 3:
                        otherIDTextBox3.Visible = true;
                        otherIDTextBox3.Text = inID;
                        otherCharactorPictureBox3.Visible = true;
                        break;
                }
            }else if (threadBytes[0] == 2) {
                otherPlayerIndex[threadBytes[1]] = threadBytes[1];
                otherCharactorNumber[threadBytes[1]] = threadBytes[2];
                String inID = Encoding.Unicode.GetString(threadBytes, 4, threadBytes[2]);
                changeOtherPlayer(threadBytes[1], inID, threadBytes[3]);
            }else if (threadBytes[0] == 3) {
                String inText = Encoding.Unicode.GetString(threadBytes, 4, threadBytes[3]);
                String message = "";
                if (threadBytes[2] == myPlayerIndex) {
                    message = myIDTextBox.Text + ": " + inText;
                    conversationTextBox.Text = conversationTextBox.Text + Environment.NewLine + message;
                    return;
                }
                for (int i = 0; i < myStartForm.numberOfPlayers - 1; i++) {
                    if (threadBytes[2] == otherPlayerIndex[i]) {
                        switch (i) {
                            case 0:
                                message = otherIDTextBox1.Text + ": ";
                                break;
                            case 1:
                                message = otherIDTextBox2.Text + ": ";
                                break;
                            case 2:
                                message = otherIDTextBox3.Text + ": ";
                                break;
                        }
                        message = message + inText;
                        conversationTextBox.Text = conversationTextBox.Text + Environment.NewLine + message;
                        return;
                    }
                }
            }else if (threadBytes[0] == 1) {
                chooseOtherCharactor(threadBytes[1], threadBytes[2]);
            }else if (threadBytes[0] == 5) {
                if(myCharactorNumber == 0)
                {
                    myCharactorNumber = 1;
                }
                for(int i = 0; i < numberOfConnection - 1; i++)
                {
                    if(otherCharactorNumber[i] == 0)
                    {
                        otherCharactorNumber[i] = 1;
                    }
                }
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                GameOnlineForm gameOnlineForm = new GameOnlineForm(myStartForm, this);
                gameOnlineForm.Show();
                this.Hide();
                endReport = true;
            }
        }

        private void recevBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {

        }

        public void playBackgro()
        {
            axWindowsMediaPlayer1.URL = "ChmpSlct_BlindPick.mp3";
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.Ctlcontrols.play();
            axWindowsMediaPlayer1.Visible = false;
        }

        private void CharactorChooseOnlineForm_Shown(object sender, EventArgs e)
        {
            playBackgro();
        }
    }
}
