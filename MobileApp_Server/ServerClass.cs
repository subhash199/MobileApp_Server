using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;



namespace MobileApp_Server
{

    public class ServerClass
    {
        public static TcpListener listener;

        public void RunServer()
        {

            Socket connection;
            Handler requesthandler;
            try
            {
                listener = new TcpListener(IPAddress.Any, 5002);
                listener.Start();
                connection = listener.AcceptSocket();
                requesthandler = new Handler();
                Thread t = new Thread(() => requesthandler.ClientRequest(connection));
                t.Start();
            }
            catch
            {
                //var messageDialog = new MessageDialog("Server not responding!");
                //await messageDialog.ShowAsync();
            }
        }



        public class Handler
        {           
            public void ClientRequest(Socket connection)
            {
                NetworkStream stream = new NetworkStream(connection);
                StreamWriter sw = new StreamWriter(stream);
                StreamReader sr = new StreamReader(stream);
                sw.AutoFlush = true;
                connection.Close();
                try
                {
                    string readLine = sr.ReadToEnd();
                }
                catch
                {

                }
            }
        }
    }
}
