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

       public static DatabaseRequest request = new DatabaseRequest();
        public void RunServer()
        {
            

            Socket connection;
            Handler requesthandler;
            try
            {
                listener = new TcpListener(IPAddress.Any, 5002);
                listener.Start();
                while(true)
                {
                    connection = listener.AcceptSocket();
                    requesthandler = new Handler();
                    // requesthandler.ClientRequest(connection);
                    Thread t = new Thread(() => requesthandler.ClientRequest(connection));
                    t.Start();
                }
                
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
                try
                {
                    string readLine = sr.ReadLine();
                    
                    string[] info = readLine.Split('|');
                    switch (info[0])
                    {
                        case "logIn":
                           sw.WriteLine( UserLogIn(info));                            
                            break;
                        case "register":
                           sw.WriteLine( RegisterUser(info));
                            break;
                        default:

                            break;

                    }
                        

                }
                catch(Exception e)
                {

                }
                finally
                {
                    sr.Close();
                    sw.Close();
                    stream.Close();
                    connection.Close();
                    
                }
            }

            private string RegisterUser(string[] info)
            {
              string value=  request.RegisterUser(info);
               return value;
            }

            private string UserLogIn(string[] info)
            {
                string value = request.LogInUser(info);
                return value;
            }
        }
    }
}
