using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Text.RegularExpressions;

namespace Children_Down_Stairs_Server {

    class Program {

        static private int NumberOfConnection = 2;
        static private int CurrentNumberOfConnection = 0;
        static private int MyPort = 8888;
        static private int ReadyNumber = 0;

        static private TcpListener MyTcpListener;
        
        static private TcpClient[] MyTcpClient;
        static private Byte[] MainBytes;
        static private Byte[][] ThreadBytes;
        static private NetworkStream[] MyNetworkStream;
        static private String[] MyClientID;
        static private int[] MyClientCharactor;
        static private BackgroundWorker[] ClientBackgroundWorker;
        static private bool GameStart = false;

        //
        //建議改法
        static private bool[] endReport;
        static private bool endSend = true;
        static private bool[] endGameSend;
        //


        //下列部分是game下需要紀錄的
        //--------------------------------
        static private int RecvCount = 0;
        static private int NewGround = 0;
        static private int MoveCount = 0;
        static private BackgroundWorker SendBackgroundWorker;
        static private BackgroundWorker[] RecvBackgroundWorker;
        static private MoveInfo[] MyMoveInfoArray;
        static private bool gameover = false;
        //--------------------------------


        static void Main(string[] args) {
            while (true)
            {
                Console.WriteLine("Please enter the number of connection this game");
                int input;
                do
                {
                    input = Console.Read();
                    if (input <= '0' || '4' < input)
                    {
                        Console.WriteLine("The number must between 2~4");
                    }
                } while (input <= '0' || '4' < input);

                NumberOfConnection = input - '0';

                AcceptConnection();
                MapTransmit();
            }
 
        }

        static private void AcceptConnection() {
            endReport = new bool[NumberOfConnection];
            endGameSend = new bool[NumberOfConnection];

            MyTcpListener = new TcpListener(MyPort);
            Console.WriteLine("Start listening");
            MyTcpListener.Start();
            
            MyTcpClient = new TcpClient[NumberOfConnection];
            MyNetworkStream = new NetworkStream[NumberOfConnection];
            MyClientID = new String[NumberOfConnection];
            MyClientCharactor = new int[NumberOfConnection];
            ClientBackgroundWorker = new BackgroundWorker[NumberOfConnection];
            ThreadBytes = new Byte[NumberOfConnection][];
            setBackgroundWorker();

            RecvBackgroundWorker = new BackgroundWorker[NumberOfConnection];

            for (int i = 0; i < NumberOfConnection; i++) {
                MyTcpClient[i] = default(TcpClient);
                MyClientCharactor[i] = 1;

                Console.WriteLine(" >> Wait for connection from client " + (i + 1) + "...");
                MyTcpClient[i] = MyTcpListener.AcceptTcpClient();
                Console.WriteLine(" >> Accept connection from client " + (i + 1));
                CurrentNumberOfConnection++;

                MyNetworkStream[i] = MyTcpClient[i].GetStream();
                
                // 接收client端ID
                MyClientID[i] = recvString(i);
                Console.WriteLine("Client " + (i + 1) + " ID: " + MyClientID[i]);
                
                // 告訴client遊戲需要玩家數
                
                sendInt(i);
                System.Threading.Thread.Sleep(100);
                sendConnectionInfo(i);
                System.Threading.Thread.Sleep(100);

                for (int j = 0; j < i; j++) {
                    // 告知client新進的連線
                    sendNewOtherConnectionInfo(j, i);
                    System.Threading.Thread.Sleep(100);
                    // 告知client舊的連線
                    sendOldOtherConnectionInfo(i, j);
                    System.Threading.Thread.Sleep(100);
                }
                
                ClientBackgroundWorker[i].RunWorkerAsync();
            }
            while (!GameStart)
            {
            }
            closeBackgroundWorker();
        }

        static private void MapTransmit(){
            gameover = false;
            setGameBackgroundWorker();
            MyMoveInfoArray = new MoveInfo[NumberOfConnection];

            Console.WriteLine("All connection is full");
            Console.WriteLine("SendBackgroundWorker begin work for map");
            for(int i = 0; i < NumberOfConnection; i++)
            {
                RecvBackgroundWorker[i].RunWorkerAsync();
            }
            SendBackgroundWorker.RunWorkerAsync();
            while (gameover == false)
            {

            }
        }

        static private void sendString(int index, String inString) {
            MainBytes = Encoding.Unicode.GetBytes(inString);
            MyNetworkStream[index].Write(MainBytes, 0, MainBytes.Length);
            MyNetworkStream[index].Flush();
        }

        static private void sendInt(int index) {
            MainBytes = BitConverter.GetBytes(NumberOfConnection);
            MyNetworkStream[index].Write(MainBytes, 0, MainBytes.Length);
            MyNetworkStream[index].Flush();
        }

        static private String recvString(int index) {
            int length;
            MainBytes = new Byte[MyTcpClient[index].ReceiveBufferSize];
            length = MyNetworkStream[index].Read(MainBytes, 0, MyTcpClient[index].ReceiveBufferSize);
            MyNetworkStream[index].Flush();
            return Encoding.Unicode.GetString(MainBytes, 0, length);
        }

        /*  [0] = 0 // 告訴連線玩家他的編號
            [1] = ? // 連線為玩家幾  */
        static private void sendConnectionInfo(int index) {
            MainBytes = new Byte[3];
            MainBytes[0] = (Byte)0;
            MainBytes[1] = (Byte)index;
            MainBytes[2] = (Byte)CurrentNumberOfConnection;
            MyNetworkStream[index].Write(MainBytes, 0, MainBytes.Length);
            MyNetworkStream[index].Flush();
        }

        /*  [0] = 1 // 告訴練線玩家新增連線編號
            [1] = ? // 新連線為玩家幾
            [2] =   // ID長度
            [3] ~	// 新連線玩家ID  */
        static private void sendNewOtherConnectionInfo(int index, int newIndex) {
            Byte[] tempBytes = Encoding.Unicode.GetBytes(MyClientID[newIndex]);
            MainBytes = new Byte[4 + tempBytes.Length];
            MainBytes[0] = (Byte)4;
            MainBytes[1] = (Byte)newIndex;
            MainBytes[2] = (Byte)tempBytes.Length;
            MainBytes[3] = (Byte)CurrentNumberOfConnection;
            System.Buffer.BlockCopy(tempBytes, 0, MainBytes, 4, tempBytes.Length);
            MyNetworkStream[index].Write(MainBytes, 0, MainBytes.Length);
            MyNetworkStream[index].Flush();
        }

        /*  [0] = 2 // 告訴練線玩家舊的連線編號
            [1] = ? // 新連線為玩家幾
            [2] =   // ID長度
            [3] =   // charactor number
            [4] ~	// 新連線玩家ID  */
        static private void sendOldOtherConnectionInfo(int index, int oldIndex) {
            Byte[] tempBytes = Encoding.Unicode.GetBytes(MyClientID[oldIndex]);
            MainBytes = new Byte[4 + tempBytes.Length];
            MainBytes[0] = (Byte)2;
            MainBytes[1] = (Byte)oldIndex;
            MainBytes[2] = (Byte)tempBytes.Length;
            MainBytes[3] = (Byte)MyClientCharactor[oldIndex];
            System.Buffer.BlockCopy(tempBytes, 0, MainBytes, 4, tempBytes.Length);
            MyNetworkStream[index].Write(MainBytes, 0, MainBytes.Length);
            MyNetworkStream[index].Flush();
        }

        static private void mainRecvBytes(int index) {
            MainBytes = new Byte[MyTcpClient[index].ReceiveBufferSize];
            MyNetworkStream[index].Read(MainBytes, 0, MyTcpClient[index].ReceiveBufferSize);
            MyNetworkStream[index].Flush();
        }

        static private void threadRecvBytes(int index) {
            try
            {
                ThreadBytes[index] = new Byte[MyTcpClient[index].ReceiveBufferSize];
                int size = MyNetworkStream[index].Read(ThreadBytes[index], 0, MyTcpClient[index].ReceiveBufferSize);
                MyNetworkStream[index].Flush();
                Array.Resize<Byte>(ref ThreadBytes[index], size);
            }
            catch
            {
                return;
            }
        }

        static private void mainSendMessage(int index) {
            Console.WriteLine(Encoding.Unicode.GetString(ThreadBytes[index], 3, ThreadBytes[index][2]));
            MainBytes = new Byte[1 + ThreadBytes[index].Length];
            MainBytes[0] = 3;
            System.Buffer.BlockCopy(ThreadBytes[index], 0, MainBytes, 1, ThreadBytes[index].Length);
            for (int i = 0; i < CurrentNumberOfConnection; i++) {
                MyNetworkStream[i].Write(MainBytes, 0, MainBytes.Length);
                MyNetworkStream[i].Flush();
                System.Threading.Thread.Sleep(100);
            }
        }
        
        static private void mainSendCharactorChoose(int index) {
            MainBytes = ThreadBytes[index];
            for (int i = 0; i < CurrentNumberOfConnection; i++) {
                if (i == index) {
                    continue;
                }
                MyNetworkStream[i].Write(MainBytes, 0, MainBytes.Length);
                MyNetworkStream[i].Flush();
                System.Threading.Thread.Sleep(100);
            }
        }

        static private void mainSendStartGame() {
            MainBytes = new Byte[1];
            MainBytes[0] = 5;
            for (int i = 0; i < NumberOfConnection; i++) {
                MyNetworkStream[i].Write(MainBytes, 0, MainBytes.Length);
                MyNetworkStream[i].Flush();
                System.Threading.Thread.Sleep(100);
            }
        }

        static private void setBackgroundWorker() {
            for (int i = 0; i < NumberOfConnection; i++) {
                ClientBackgroundWorker[i] = new BackgroundWorker();
                ClientBackgroundWorker[i].WorkerReportsProgress = true;
                ClientBackgroundWorker[i].WorkerSupportsCancellation = true;
                ClientBackgroundWorker[i].DoWork += new DoWorkEventHandler(clientBackgroundWorker_DoWork);
                ClientBackgroundWorker[i].ProgressChanged += new ProgressChangedEventHandler(clientBackgroundWorker_ProgressChanged);
                ClientBackgroundWorker[i].RunWorkerCompleted += new RunWorkerCompletedEventHandler(clientBackgroundWorker_RunWorkerCompleted);
            }
        }

        static private void closeBackgroundWorker() {
            for (int i = 0; i < NumberOfConnection; i++) {
                ClientBackgroundWorker[i].CancelAsync();
            }
        }

        static private void clientBackgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
            for (int i = 0; i < CurrentNumberOfConnection; i++) {
                if (sender.Equals(ClientBackgroundWorker[i])) {
                    while (true)
                    {
                        if (!GameStart)
                        {
                            threadRecvBytes(i);
                        }
                        switch (ThreadBytes[i][0])
                        {
                            case 0:
                                mainSendMessage(i);
                                break;
                            case 1:
                                MyClientCharactor[ThreadBytes[i][1]] = ThreadBytes[i][2];
                                mainSendCharactorChoose(i);
                                break;
                            case 2:
                                ReadyNumber++;
                                if (ReadyNumber == NumberOfConnection)
                                {
                                    mainSendStartGame();
                                    GameStart = true;   
                                }
                                return;
                                break;
                        }

                        //ClientBackgroundWorker[i].ReportProgress(0);
                        System.Threading.Thread.Sleep(100);

                        if (ClientBackgroundWorker[i].CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
        }

        static private void clientBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            for (int i = 0; i < CurrentNumberOfConnection; i++) {
                if (sender.Equals(ClientBackgroundWorker[i])) {
                    switch (ThreadBytes[i][0]) {
                        case 0:
                            mainSendMessage(i);
                            break;
                        case 1:
                            MyClientCharactor[ThreadBytes[i][1]] = ThreadBytes[i][2];
                            mainSendCharactorChoose(i);
                            break;
                        case 2:
                            ReadyNumber++;
                            if (ReadyNumber == NumberOfConnection) {
                                mainSendStartGame();
                                GameStart = true;
                            }
                            break;
                        case 3:
                            Console.WriteLine("not closed");
                            break;
                        case 100:
                            Console.WriteLine("oh no");
                            break;
                    }
                    break;
                }
            }
        }

        static private void clientBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {

        }

        //------------------------------------------------
        //以下是遊戲用

        static private MoveInfo bytesToMoveInfo(Byte[] MyBytes)
        {
            string MoveString = Encoding.Unicode.GetString(MyBytes, 3, MyBytes[2]);
            string[] sArray = Regex.Split(MoveString, ",", RegexOptions.IgnoreCase);
            bool[] rv = new bool[3];
            int count = 0;
            foreach (string i in sArray){
                if (i.Equals("True")){
                    rv[count] = true;
                }
                else{
                    rv[count] = false;
                }
                count++;
            }
            MoveInfo recvMove = new MoveInfo(rv[0], rv[1], rv[2]);

            MyMoveInfoArray[MyBytes[1]] = recvMove;

            return recvMove;
        }

        static private void setGameBackgroundWorker(){
            SendBackgroundWorker = new BackgroundWorker();
            SendBackgroundWorker.WorkerReportsProgress = true;
            SendBackgroundWorker.WorkerSupportsCancellation = true;
            SendBackgroundWorker.DoWork += new DoWorkEventHandler(sendBackgroundWorker_DoWork);
            SendBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(sendBackgroundWorker_ProgressChanged);
            SendBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(sendBackgroundWorker_RunWorkerCompleted);
            for(int i = 0; i < NumberOfConnection; i++)
            {
                RecvBackgroundWorker[i] = new BackgroundWorker();
                RecvBackgroundWorker[i].WorkerReportsProgress = true;
                RecvBackgroundWorker[i].WorkerSupportsCancellation = true;
                RecvBackgroundWorker[i].DoWork += new DoWorkEventHandler(recvBackgroundWorker_DoWork);
                RecvBackgroundWorker[i].ProgressChanged += new ProgressChangedEventHandler(recvBackgroundWorker_ProgressChanged);
                RecvBackgroundWorker[i].RunWorkerCompleted += new RunWorkerCompletedEventHandler(recvBackgroundWorker_RunWorkerCompleted);
            }
        }


        static private void sendBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true){
                if(gameover == true)
                {
                    return;
                }
                MoveCount = 0;
                for (int i = 0; i < NumberOfConnection; i++)
                {
                    if(endReport[i] == true)
                    {
                        MoveCount++;
                    }
                }
                if (MoveCount == NumberOfConnection){
                    MoveCount = 0;
                    for (int i = 0; i < NumberOfConnection; i++)
                    {
                        endReport[i] = false;
                    }
                    //Console.WriteLine("MoveCount is reset");
                    //Console.WriteLine("sendBackgroundWorker is sending MoveInfoArray to All");
                    sendMoveInfoArrayToAll(MyMoveInfoArray);

                    /*
                    for (int i = 0; i < NumberOfConnection; i++)
                    {
                        //Console.WriteLine("Set endGameSend " + i + " true");
                        endGameSend[i] = true;
                    }
                    */
                    //Console.WriteLine();

                    
                }
            }
        }
        static private void sendBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        static private void sendBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        static private void recvBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < CurrentNumberOfConnection; i++)
            {
                if (sender.Equals(RecvBackgroundWorker[i]))
                {
                    while (true)
                    {
                        if(gameover == true)
                        {
                            return;
                        }
                        //Console.WriteLine("clientBackgroundWorker " + i + " is receviving");
                        threadRecvBytes(i);
                        //Console.WriteLine("clientBackgroundWorker " + i + " end recevive");
                        /*
                        endGameSend[i] = false;
                        endReport[i] = false;
                        RecvBackgroundWorker[i].ReportProgress(0);
                        
                        while (endGameSend[i] == false)
                        {

                        }
                        */
                        
                        switch (ThreadBytes[i][0])
                        {
                            //--------
                            //case 100 以後的給game用
                            case 100:
                                MoveInfo recvMove = bytesToMoveInfo(ThreadBytes[i]);
                                MyMoveInfoArray[i] = recvMove;
                                RecvCount++;
                                //Console.WriteLine("Recveive code 100 : MoveInfo is recveive from " + i + " ,count :" + RecvCount + ", MoveCount :" + MoveCount);
                                //Console.WriteLine(recvMove.ToString());
                                break;
                            case 200:
                                gameover = true;
                                break;
                        }
                        endReport[i] = true;
                        
                    }
                }
            }
        }
        static private void recvBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            for (int i = 0; i < CurrentNumberOfConnection; i++)
            {
                if (sender.Equals(RecvBackgroundWorker[i]))
                {
                    switch (ThreadBytes[i][0])
                    {
                        //--------
                        //case 100 以後的給game用
                        case 100:
                            MoveInfo recvMove = bytesToMoveInfo(ThreadBytes[i]);
                            MyMoveInfoArray[i] = recvMove;
                            RecvCount++;
                            //Console.WriteLine("Recveive code 100 : MoveInfo is recveive from " + i + " ,count :" + RecvCount + ", MoveCount :" + MoveCount);
                            //Console.WriteLine(recvMove.ToString());
                            break;
                        case 200:
                            break;
                    }
                    endReport[i] = true;
                    break;
                }
            }
        }
        static private void recvBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        static private void sendMoveInfoArrayToAll(MoveInfo[] newMoveArray)
        {

            //做所有人move資訊
            Byte[][] BytesArray = new Byte[NumberOfConnection][];
            int totalLength = 0;
            for (int i = 0; i < NumberOfConnection; i++){
                BytesArray[i] = Encoding.Unicode.GetBytes(newMoveArray[i].ToString());
                totalLength += BytesArray[i].Length;
            }

            //做地板資訊
            Byte[] g_byte;
            if ((RecvCount / NumberOfConnection) > NewGround)
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                int rnd_x = rnd.Next() % 425;
                int rnd_ground = rnd.Next() % 5;

                GroundInfo g_info = new GroundInfo(rnd_x, rnd_ground);
                g_byte = Encoding.Unicode.GetBytes(g_info.ToString());
                totalLength += g_byte.Length;

                NewGround += 150;
            }
            else
            {
                GroundInfo g_info = new GroundInfo(-1, -1);
                g_byte = Encoding.Unicode.GetBytes(g_info.ToString());
                totalLength += g_byte.Length;
            }



            MainBytes = new Byte[6 + totalLength];
            MainBytes[0] = (Byte)100;

            int tempLength = 6;
            for (int i = 0; i < NumberOfConnection; i++){
                MainBytes[i + 1] = (Byte)BytesArray[i].Length;
                System.Buffer.BlockCopy(BytesArray[i], 0, MainBytes, tempLength, BytesArray[i].Length);
                tempLength += BytesArray[i].Length;
            }
            MainBytes[5] = (Byte)g_byte.Length;
            System.Buffer.BlockCopy(g_byte, 0, MainBytes, tempLength, g_byte.Length);

            for (int i = 0; i < NumberOfConnection; i++){
                for(int j = 0; j < 1; j++)//目前這段不知道為什麼，要send兩次對方才有辦法收到
                {
                    MyNetworkStream[i].Write(MainBytes, 0, MainBytes.Length);
                }
            }

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
    
    
}