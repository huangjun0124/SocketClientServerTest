using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Common;

namespace Server
{
    public partial class ServerForm : Form
    {
        private void LogInfo(string msg)
        {
            listBoxServer.BeginInvoke(new Action(() =>
            {
                listBoxServer.Items.Add(msg);
            }));
        }

        /// 用于保存非对称加密的公钥
        private string publicKey = string.Empty;
        /// 用于保存非对称加密的私钥
        private string pfxKey = string.Empty;

        /// 用于跟客户端通信的Socket
        private Socket serverCommunicateSocket;
        /// 定义接收缓存快的大小
        private static int serverBufferSize = 1024;
        /// 缓存块
        private byte[] bytesReceivedFromClient = new byte[serverBufferSize];
        /// 密钥 K
        private string key = string.Empty;
        StringBuilder messageFromClient = new StringBuilder();

        public ServerForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // 先生称数字证书（模拟，即非对称密钥对
            RSAKeyInit();
            //负责侦听
            StartListen();
        }

        private void RSAKeyInit()
        {
            RASProcesser.CreateRSAKey(ref publicKey, ref pfxKey);
            txtPublicKey.Text = publicKey;
        }

        private void StartListen()
        {
            // IPHostEntry hostEntry = new IPHostEntry();
            IPAddress[] ipadddress = Dns.GetHostAddresses(Dns.GetHostName()); //
            
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 13000);
            //负责侦听的Socket
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(iep);
            listenSocket.Listen(50);
            listenSocket.BeginAccept(new AsyncCallback(Accepted), listenSocket);
            LogInfo("开始侦听客户端的连接...");
            btnStart.Enabled = false;
        }

        /// 负责客户端的连接，并将自己置于接受状态
        private void Accepted(IAsyncResult result)
        {
            //还原传入的原始套接字
            Socket listenSocket = result.AsyncState as Socket;
            // 初始化和客户端进行通信的Socket....在原始套接字上调用EndAccept方法，返回新的套接字
            serverCommunicateSocket = listenSocket.EndAccept(result);
            LogInfo("有客户端连接到...: : ");
            serverCommunicateSocket.BeginReceive(bytesReceivedFromClient, 0, serverBufferSize, SocketFlags.None,
                new AsyncCallback(ReceivedFromClient), null);
        }

        /// 负责处理接收自客户端的数据
        private void ReceivedFromClient(IAsyncResult result)
        {
            try
            {
                int read = serverCommunicateSocket.EndReceive(result);
                if (read > 0)
                {
                    messageFromClient.Append(EncodingInstance.Instance.GetString(bytesReceivedFromClient, 0, read));
                    // 处理并显示数据
                    ProcessAndShowInServer();
                    serverCommunicateSocket.BeginReceive(bytesReceivedFromClient, 0, serverBufferSize, SocketFlags.None,
                    new AsyncCallback(ReceivedFromClient), null);
                }
            }
            catch (Exception e)
            {
                LogInfo(e.Message);
            }
        }

        private void ProcessAndShowInServer()
        {
            string msg = messageFromClient.ToString();
            // 如果接收到 <EOF> 表示完成一次，否则继续将自己置于接收状态
            if (msg.IndexOf("<EOF>") > -1)
            {
                // 如果客户端发送Key，则负责初始化Key
                if (msg.IndexOf("<KEY>") > -1)
                {
                    key = RASProcesser.RSADecrypt(pfxKey, msg.Substring(0, msg.Length - 10));
                    LogInfo(string.Format("接收到客户端密钥：{0}", key));
                }
                else
                {
                    //解密SSL通道发送过来的密文并显示
                    LogInfo(string.Format("接收到客户端消息：{0}",RijndaelProcessor.DecryptString(msg.Substring(0,msg.Length-5),key)));
                }
                messageFromClient.Clear();
            }
        }

        private void btnSendTextToClient_Click(object sender, EventArgs e)
        {
            if (serverCommunicateSocket == null || !serverCommunicateSocket.Connected)
            {
                LogInfo("发送失败，客户端未连接");
                return;
            }
            //加密消息体
            string msg = string.Format("{0}{1}", RijndaelProcessor.EncryptString(DateTime.Now.ToString() + "  " + txtSendTo.Text, key), "<EOF>");
            string input = RijndaelProcessor.DecryptString(msg.Substring(0, msg.Length - 5), key);
            byte[] msgBytes = EncodingInstance.Instance.GetBytes(msg);
            serverCommunicateSocket.BeginSend(msgBytes, 0, msgBytes.Length,SocketFlags.None, null,null);
            LogInfo(string.Format("发送 : {0}", msg));
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (serverCommunicateSocket == null) return;
            serverCommunicateSocket.Close();
        }

        private void listBoxServer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxServer.SelectedItem != null)
            {
                Clipboard.SetText(listBoxServer.SelectedItem.ToString());
            }
        }
    }
}
