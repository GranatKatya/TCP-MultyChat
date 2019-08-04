using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClientChat
{


    public  static class Colections {       
        static public ObservableCollection<string> messages { get; set; } = new ObservableCollection<string>();
    }
    public  class Client
    {   

        //TcpListener usertcpListener;

        // //NetworkStream stream;

        //    public Client(string ip, string port)//connect
        //    {
        //        //создание экземпляра класса TcpListener //данные о хосте и порте читаются  //из текстовых  
        //        usertcpListener = new TcpListener(IPAddress.Parse(ip), Convert.ToInt32(port));
        //       // messages = new ObservableCollection<string>();
        //    }
        //    public void ClientListening()
        //    {
        //        // messageList = messageLista;
        //        try
        //        {
        //            //начало прослушивания клиентов
        //            usertcpListener.Start();
        //            //создание отдельного потока для чтения сообщения
        //            Thread thread = new Thread(new ThreadStart(ReadMessageThreadClient));  //(delegate () { ReadMessageThread(); });//Thread thread = new Thread(() => download(filename)); thread.Start();
        //            thread.IsBackground = true;
        //            //запуск потока
        //            thread.Start();
        //        }
        //        catch (SocketException sockEx)
        //        {
        //            MessageBox.Show("Ошибка сокета: " + sockEx.Message);
        //        }
        //        catch (Exception Ex)
        //        {
        //            MessageBox.Show("Ошибка : " + Ex.Message);
        //        }
        //    }

        //    private void ReadMessageThreadClient()
        //    {
        //        while (true)
        //        {
        //            //сервер сообщает клиенту о готовности  //к соединению
        //            TcpClient cl = usertcpListener.AcceptTcpClient();
        //            //чтение данных из сети в формате Unicode

        //            //  while (true)
        //            // {
        //            StreamReader sr = new StreamReader(cl.GetStream(), Encoding.Unicode);
        //            string s = sr.ReadLine();

        //            //if (user.Status.IndexOf("Disconnect") > -1)
        //            //{
        //            //    // s = "User Disconnect";
        //            //    s = user.Message;
        //            //}

        //            if (s != null)
        //            {
        //                Application.Current.Dispatcher.Invoke(() => messages.Add(s));
        //            }
        //            cl.Close();
        //        }

        //    }













        //22





        public void ConnectCommand(string ip, string port)//connection 1 
        {

            if (TcpClient == null  || TcpClient?.Connected == false)//КОГДА НЕ ПОДКЛЮЧЕНЫ К СКРВЕРУ
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        TcpClient = new TcpClient();
                        TcpClient.Connect(IPAddress.Parse(ip), Convert.ToInt32(port));
                        sr = new StreamReader(TcpClient.GetStream(), Encoding.Unicode);
                        sw = new StreamWriter(TcpClient.GetStream(), Encoding.Unicode);
                        sw.AutoFlush = true;
                        

                        string s = "Client: " + Name + " UnicNimber: " + UnicNimber + " Status: " + Status + " ConnectionTime: " + ConnectionTime.ToString("h:mm:ss tt") +
                            " Message: " + "Connecction" + "<<E!N!D>> ";
                        sw.WriteLine(s);

                        //App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                        //{
                        //    Colections.messages.Add("Client:"+ Name+" connected");
                        //});
                    }
                    catch (Exception )
                    {
                        MessageBox.Show("Unable to connect to server");
                      //  MessageBox.Show(EX.Message);
                    }
                });
            }
            else
            {
                MessageBox.Show("You connect already");
            }
        }



        //222
        public void send(string Message)//send
        {
            if (TcpClient?.Connected == true && !string.IsNullOrWhiteSpace(Message))//когда подключен и когда сообщение не пуссто
            {
                Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            string s = "Client: " + Name + " UnicNimber: " + UnicNimber + " Status: " + Status + " ConnectionTime: " + ConnectionTime.ToString("h:mm:ss tt") +
                              " Message: " + Message + "<<E!N!D>> ";
                            sw.WriteLine(s);
                            //clian text field

                        }
                        catch (Exception)
                        {
                            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                            {
                                Colections.messages.Add(DateTime.Now.ToLongTimeString() + Environment.NewLine + "Message dont send");
                                mylistBox.SelectedItem = mylistBox.Items[Colections.messages.Count - 1];
                                mylistBox.ScrollIntoView(mylistBox.SelectedItem);
                            });
                        }
                    });
            }//  +users[i].ConnectionTime.ToString() + System.Environment.NewLine
            else
            {
                if (TcpClient == null)
                {
                    MessageBox.Show("Server crashed or disconected!"  + "Try to connect again");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Message))
                    {
                        MessageBox.Show("You cant send empty message");
                    }
                    else
                    {
                        MessageBox.Show("server crashed or disconected!" + String.Empty + " Try to connect again");
                    }
                }
                            
            }
        }


        ListBox mylistBox;
        //2
        TcpClient TcpClient;
        StreamReader sr;
        StreamWriter sw;
        public Client(string ip, string port, string name,ListBox listBox)
        {
         //   IPAddress ipv4 = IPAddress.Any;
         //   usertcpListener = new TcpListener(ipv4, Convert.ToInt32("11000"));

            Name = name;
            Ip = ip;
            Port = port;
            //IsBusy = false;
            //client = null;

            // Create and display the value of two GUIDs.
            UnicNimber = Guid.NewGuid();
            ConnectionTime = DateTime.Now;// ToString("h:mm:ss tt");
            Status = "Connection";

            mylistBox = listBox;

        }
        public void MainViewModel() {  //listen

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        if (TcpClient?.Connected ==true)
                        {
                            string line = sr.ReadLine();

                            if (line !=null)
                            {
                                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                {
                                    Colections.messages.Add(DateTime.Now.ToLongTimeString() + Environment.NewLine + line );
                                    mylistBox.SelectedItem = mylistBox.Items[Colections.messages.Count - 1];
                                    mylistBox.ScrollIntoView(mylistBox.SelectedItem);

                                });
                               
                            }
                            else
                            {//сервер не мог прислатьь пустую строку
                                TcpClient.Close();
                                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                {
                                    Colections.messages.Add("Connected error" + DateTime.Now.ToLongTimeString());
                                    mylistBox.SelectedItem = mylistBox.Items[Colections.messages.Count - 1];
                                    mylistBox.ScrollIntoView(mylistBox.SelectedItem);
                                });
                            }
                        }
                        Task.Delay(10).Wait();
                      
                    }
                    catch (Exception)
                    {
                        //TcpClient = null;
                        //sw = null;
                        //sr = null;
                        //Colections.messages = new ObservableCollection<string>();
                        if (TcpClient.Connected==false)
                        {
                            // MessageBox.Show("Server crashed or disconected!" + " Try to connect again");
                            MessageBox.Show("Connection close");
                        }
                        //  throw;
                    }

                }
            });




           // ClientListening();
        }

        public void Disconnect2()
        {
            if (TcpClient != null)
            {
                if (TcpClient?.Connected == true)
                {
                    Message = "disconected";
                    Status = "Disconnected";
                    string s = "Client: " + Name + " UnicNimber: " + UnicNimber + " Status: " + Status + " ConnectionTime: " + ConnectionTime.ToString("h:mm:ss tt") +
                            " Message: " + Message + "<<E!N!D>> ";
                    sw.WriteLine(s);
                    TcpClient.Close();
                    Colections.messages.Add("Сlient:  " + Name + ConnectionTime.ToString() + System.Environment.NewLine + " disconnected");
                    mylistBox.SelectedItem = mylistBox.Items[Colections.messages.Count - 1];
                    mylistBox.ScrollIntoView(mylistBox.SelectedItem);

                    TcpClient = null;
                    sw = null;
                    sr = null;
                    Colections.messages = new ObservableCollection<string>();
                }
            }
            else {
               MessageBox.Show("You already dissconect");
            }
        }

       
        public string Name { get; set; }
        public string Ip { get; set; }
        public string Port { get; set; }
        public Guid UnicNimber { get; set; }
        public DateTime ConnectionTime { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

        // public TcpClient client { get; set; } = null;
        public int IsBusy { get; set; } = 1;

        TcpClient client =null;
        IPEndPoint endPoint;

        NetworkStream nstream;
        public void Connection()//message
        {
            try
            {

                //создание экземпляра класса IPEndPoint
                endPoint = new IPEndPoint( IPAddress.Parse(Ip), Convert.ToInt32(Port));
                client = new TcpClient();
                IsBusy = 2;//busy
                //установка соединения с использованием  //данных IP и номера порта 
                client.Connect(endPoint);

                //NetworkStream nstream1 = client.GetStream();
                //byte[] barray = Encoding.Unicode.GetBytes(message);
                //nstream1.Write(barray, 0, barray.Length);

                //получение сетевого потока
                nstream = client.GetStream();
                //преобразование строки сообщения в массив байт
                byte[] barray = Encoding.Unicode.GetBytes("Client: " + Name + " UnicNimber: " + UnicNimber + " Status: " + Status + " ConnectionTime: " + ConnectionTime.ToString("h:mm:ss tt") +
                  " Message: " + Message + "<<E!N!D>> "+ "\r\n");// "<<E!N!D>> " );//+ message);
                //запись в сетевой поток всего массива 
                nstream.Write(barray, 0, barray.Length);
                //закрытие клиента
               //  client.Close();           

            }
            catch (SocketException sockEx)
            {
                MessageBox.Show("Ошибка сокета:" + sockEx.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка :" + Ex.Message);
            }
        }

        public void Disconnection()
        {
            client.Close();
            Message = "Сlient:  " + Name + "  disconnected from server!";
            //  Connection("Сlient:  " + Name + "  disconnected from server!"+ "<<E!N!D<<D!I!S!C1ONNECT>>" + "\r\n");
            Connection();

           
       //     nstream = client.GetStream();

            
           // nstream.Write(barray, 0, barray.Length);
            //nstream.Flush();


          
            //закрытие клиента
            if (client != null)
                client.Close();

            client = null;
            IsBusy = 1;//free

        }
    }
}
