using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using DownStairs;

namespace Children_Down_Stairs_Client {

    public class ClientNetwork {

        public TcpClient myTcpClient { get; private set;}
        public NetworkStream myNetworkStream { get; private set;}
        private int port;
        private Byte[] mainBytes;
        private Byte[] threadBytes;

        public ClientNetwork() {
            myTcpClient = new TcpClient();
            port = 8888;
        }

        public bool connect(String IP) {
            try {
                myTcpClient.Connect(IP, port);
                myNetworkStream = myTcpClient.GetStream();
                return true;
            }catch {
                System.Windows.Forms.MessageBox.Show("Connection fail, please try again.");
                return false;
            }
        }

        public void sendString(String inString) {
            mainBytes = Encoding.Unicode.GetBytes(inString);
            myNetworkStream.Write(mainBytes, 0, mainBytes.Length);
            myNetworkStream.Flush();
        }

        public String recvString() {
            int length;
            mainBytes = new Byte[myTcpClient.ReceiveBufferSize];
            length = myNetworkStream.Read(mainBytes, 0, myTcpClient.ReceiveBufferSize);
            myNetworkStream.Flush();
            return Encoding.Unicode.GetString(mainBytes, 0, length);
        }

        public int recvInt() {
            mainBytes = new Byte[myTcpClient.ReceiveBufferSize];
            myNetworkStream.Read(mainBytes, 0, myTcpClient.ReceiveBufferSize);
            myNetworkStream.Flush();
            return BitConverter.ToInt32(mainBytes, 0);
        }

        public Byte[] recvBytes() {
            mainBytes = new Byte[myTcpClient.ReceiveBufferSize];
            myNetworkStream.Read(mainBytes, 0, myTcpClient.ReceiveBufferSize);
            myNetworkStream.Flush();
            return mainBytes;
        }

        public Byte[] threadRecvBytes() {
            threadBytes = new Byte[myTcpClient.ReceiveBufferSize];
            myNetworkStream.Read(threadBytes, 0, myTcpClient.ReceiveBufferSize);
            myNetworkStream.Flush();
            return threadBytes;
        }

        public void messageSend(String message, int playerNumber) {
            Byte[] tempBytes = Encoding.Unicode.GetBytes(message);
            mainBytes = new Byte[3 + tempBytes.Length];
            mainBytes[0] = (Byte)0;
            mainBytes[1] = (Byte)playerNumber;
            mainBytes[2] = (Byte)tempBytes.Length;
            System.Buffer.BlockCopy(tempBytes, 0, mainBytes, 3, tempBytes.Length);
            myNetworkStream.Write(mainBytes, 0, mainBytes.Length);
            myNetworkStream.Flush();
        }

        public void charactorChangeSend(int charactorNumber, int playerNumber) {
            mainBytes = new Byte[3];
            mainBytes[0] = (Byte)1;
            mainBytes[1] = (Byte)playerNumber;
            mainBytes[2] = (Byte)charactorNumber;
            myNetworkStream.Write(mainBytes, 0, 3);
            myNetworkStream.Flush();
        }

        public void readySend()
        {
            mainBytes = new Byte[1];
            mainBytes[0] = (Byte)2;
            myNetworkStream.Write(mainBytes, 0, 1);
            myNetworkStream.Flush();
        }



        //----------------------------------------------------------------------------------------------
        public void sendMoveInfo(int myMoveIndex, MoveInfo newMove)
        {
            Byte[] tempBytes = Encoding.Unicode.GetBytes(newMove.ToString()); //法二
            mainBytes = new Byte[3 + tempBytes.Length];
            mainBytes[0] = (Byte)100;
            mainBytes[1] = (Byte)myMoveIndex;
            mainBytes[2] = (Byte)tempBytes.Length;
            System.Buffer.BlockCopy(tempBytes, 0, mainBytes, 3, tempBytes.Length);
            myNetworkStream.Write(mainBytes, 0, mainBytes.Length);
        }

        public void sendEndGame()
        {
            mainBytes = new Byte[1];
            mainBytes[0] = (Byte)200;
            myNetworkStream.Write(mainBytes, 0, mainBytes.Length);
        }



    }
    /*
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

    */
}

