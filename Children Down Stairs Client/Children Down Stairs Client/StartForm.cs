using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Children_Down_Stairs_Client {

    public partial class StartForm : Form {

        public ClientNetwork myClientNetwork { get; private set;}
        public int numberOfPlayers { get; private set;}

        public StartForm() {
            InitializeComponent();
            myClientNetwork = new ClientNetwork();
        }

        private void IDTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                startButton_Click(sender, e);
            }
        }

        private void serverIPTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                startButton_Click(sender, e);
            }
        }

        private void startButton_Click(object sender, EventArgs e) {
            if (onlineCheckBox.Checked) {
                if (myClientNetwork.connect(serverIPTextBox.Text)) {
                    if (IDTextBox.Text == "") {
                        myClientNetwork.sendString(" ");
                    }else {
                        myClientNetwork.sendString(IDTextBox.Text);
                    }
                    numberOfPlayers = myClientNetwork.recvInt();
                    CharactorChooseOnlineForm charactorChooseOnlineForm = new CharactorChooseOnlineForm(IDTextBox.Text, this);
                    charactorChooseOnlineForm.Show();
                }else {
                    return;
                }
            }else {
                CharactorChooseSingleForm charactorChooseSingleForm = new CharactorChooseSingleForm(IDTextBox.Text, this);
                charactorChooseSingleForm.Show();
            }
            this.Hide();
        }

        private void exitButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void onlineCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (onlineCheckBox.Checked) {
                serverIPTextBox.Clear();
                serverIPTextBox.Enabled = true;
            }else {
                serverIPTextBox.Text = "Click to unlock";
                serverIPTextBox.Enabled = false;
            }
        }
    }
}
