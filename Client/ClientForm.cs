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

namespace Client
{
    public partial class ClientForm : Form
    {
        private void LogInfo(string msg)
        {
            listBoxClient.BeginInvoke(new Action(() =>
            {
                listBoxClient.Items.Add(msg);
            }));
        }

        private Socket clientCommunicateSocket;
        private StringBuilder messageFromServer = new StringBuilder();
        private static int clientBufferSize = 1024;
        byte[] bytesReceivedFromServer = new byte[clientBufferSize];

        /// 随机生成的Key，在这里硬编码
        private string keyCreateRandom = "Keith123";

        public ClientForm()
        {
            InitializeComponent();
        }

        private void btnConnectAndReceive_Click(object sender, EventArgs e)
        {
            // IPHostEntry hostEntry = new IPHostEntry();
            IPAddress[] ipadddress = Dns.GetHostAddresses(Dns.GetHostName()); //
            
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 13000);
            Socket connectSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            connectSocket.BeginConnect(iep, new AsyncCallback(Connected), connectSocket);
            btnConnectAndReceive.Enabled = false;
        }

        private void Connected(IAsyncResult result)
        {
            clientCommunicateSocket = result.AsyncState as Socket;
            try
            {
                clientCommunicateSocket.EndConnect(result);
            }
            catch (Exception e)
            {
                LogInfo(e.Message);
            }
            clientCommunicateSocket.BeginReceive(bytesReceivedFromServer, 0, clientBufferSize, SocketFlags.None,
                new AsyncCallback(ReceivedFromServer), null);
            LogInfo("客户端连接上服务器");
            //连接成功发送密钥给服务器
            SendKey();
        }

        private void ReceivedFromServer(IAsyncResult result)
        {
            try
            {
                int read = clientCommunicateSocket.EndReceive(result);
                if (read > 0)
                {
                    messageFromServer.Append(EncodingInstance.Instance.GetString(bytesReceivedFromServer, 0, read));
                    // 处理并显示数据
                    ProcessAndShowInfoClient();
                    clientCommunicateSocket.BeginReceive(bytesReceivedFromServer, 0, clientBufferSize, SocketFlags.None,
                    new AsyncCallback(ReceivedFromServer), null);
                }
            }
            catch (Exception e)
            {
                LogInfo(e.Message);
            }
            
        }

        private void ProcessAndShowInfoClient()
        {
            // 如果接收到 <EOF> 表示完成一次，否则继续将自己置于接收状态
            if (messageFromServer.ToString().IndexOf("<EOF>") > -1)
            {
                //解密SSL通道发送过来的密文并显示
                LogInfo(string.Format("接收到服务器消息：{0}", RijndaelProcessor.DecryptString(messageFromServer.ToString().Substring(0, messageFromServer.ToString().Length - 5), keyCreateRandom)));
                messageFromServer.Clear();
            }
        }

        private void btnSendTextToClient_Click(object sender, EventArgs e)
        {
            if (clientCommunicateSocket == null)
            {
                LogInfo("发送失败，客户端未连接");
                return;
            }
            //加密消息体
            string msg = string.Format("{0}{1}", RijndaelProcessor.EncryptString(DateTime.Now.ToString() + "  " + txtSendTo.Text, keyCreateRandom), "<EOF>");
            byte[] msgBytes = EncodingInstance.Instance.GetBytes(msg);
            clientCommunicateSocket.BeginSend(msgBytes, 0, msgBytes.Length, SocketFlags.None, null, null);
            LogInfo(string.Format("发送 : {0}", msg));
        }

        private void SendKey()
        {
            keyCreateRandom = string.IsNullOrEmpty(txtPassword.Text) ? keyCreateRandom : txtPassword.Text;
            string msg = RASProcesser.RSAEncrypt(txtPublicKey.Text, keyCreateRandom) + "<KEY><EOF>";
            byte[] msgBytes = EncodingInstance.Instance.GetBytes(msg);
            clientCommunicateSocket.BeginSend(msgBytes, 0, msgBytes.Length, SocketFlags.None, null, null);
            LogInfo(string.Format("发送 ： {0}", keyCreateRandom));
        }

        private void listBoxClient_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxClient.SelectedItem != null)
            {
                Clipboard.SetText(listBoxClient.SelectedItem.ToString());
            }
        }
    }
}
