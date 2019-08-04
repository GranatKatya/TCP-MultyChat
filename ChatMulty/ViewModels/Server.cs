using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ChatMulty.Model;


namespace ChatMulty.ViewModels
{
    public class Server : INotifyPropertyChanged
    {
        public Server()
        {}
        //private int? mySelectedIndex = 0;

        //public int? MySelectedIndex
        //{
        //    get
        //    {
        //        return mySelectedIndex;
        //    }

        //    set
        //    {
        //        mySelectedIndex = value;
        //    }
        //}

        ListBox mylistBox;

        public string Name { get; set; }
        TcpListener list;

        public event PropertyChangedEventHandler PropertyChanged;

        //List<User> users;
        public ObservableCollection<User> users { get; set; }
        public ObservableCollection<string> messages { get; set; }

        public int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("SelectedIndex");
            }
        }

        // ListBox messageList;

        public Server(string ip, string port,  ListBox list1)//connect
        {
            users = new ObservableCollection<User>();
            messages = new ObservableCollection<string>();
            //создание экземпляра класса TcpListener //данные о хосте и порте читаются  //из текстовых окон
            list = new TcpListener(IPAddress.Parse(ip), Convert.ToInt32(port));

            mylistBox = list1;
        }

        public void Connect()
        {
            // messageList = messageLista;         
            try
            {
                //начало прослушивания клиентов
                list.Start();
                //создание отдельного потока для чтения сообщения
                Thread thread = new Thread(new ThreadStart(ReadMessageThread));  //(delegate () { ReadMessageThread(); });//Thread thread = new Thread(() => download(filename)); thread.Start();
                thread.IsBackground = true;
                //запуск потока
                thread.Start();
            }
            catch (SocketException sockEx)
            {
                MessageBox.Show("Ошибка сокета: " + sockEx.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка : " + Ex.Message);
            }
            messages.Add("Server conected to ip: 127.0.0.1, port: 11000");
        }

        private void ReadMessageThread()// another thread
        {
            while (true)
            {
                //сервер сообщает клиенту о готовности  //к соединению
                TcpClient cl = list.AcceptTcpClient();
                //чтение данных из сети в формате Unicode

                //  while (true)
                // {
                StreamReader sr = new StreamReader(cl.GetStream(), Encoding.Unicode);
                string s = sr.ReadLine();

                User user = new User();
                user.Pars(s);
                //существует ли УЖЕ соединение  таким user-ом
                bool isExist = false;
                int currentUser = -1;
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].UnicNimber == user.UnicNimber)
                    {
                        isExist = true;
                        currentUser = i;
                    }
                }
                if (isExist != true)
                {
                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {
                        users.Add(user);
                    });
                    // connectionList.Items.Add(user.ToString());
                }




                // // string identity = s;
                //   bool IsDisconnect = false;
                //  int  IndexOf = s.IndexOf("<<E!N!D>>");//9
                // string enddelete = "<<E!N!D<<D!I!S!C1ONNECT>>";
                // int IndexOfDelete = s.IndexOf(enddelete);
                // if (IndexOfDelete > -1  )
                //{
                //   // IsDisconnect = true;
                //    s = s.Substring(IndexOf + 9, s.Length - (IndexOf + 9));//IndexOf
                //    IndexOfDelete = s.IndexOf(enddelete);
                //    s = s.Substring(0, IndexOfDelete);//"<<E!N!D<<D!I!S!C1ONNECT>>"
                //}
                if (user.Status.IndexOf("Disconnect") > -1)
                {
                    // s = "User Disconnect";
                    s = user.Message;
                }
                //   else if(IndexOf>-1)//"<<E!N!D>>"
                else if (user.Status.IndexOf("SendMessage") > -1)
                {
                    // s = s.Substring(IndexOf + 9, s.Length - (IndexOf + 9));//IndexOf
                    s = user.Message;

                    // tpl !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    for (int i = 0; i < users.Count; i++)//server send messages to all connected clients
                    {
                        if (users[i].UnicNimber != user.UnicNimber)//Besides// except the sender
                        {
                            try
                            {
                                //  users[i].Stream.Write(data, 0, data.Length); //передача данных
                                //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.0"), Convert.ToInt32("11000"));//создание экземпляра класса IPEndPoint
                                //TcpClient client = new TcpClient();

                                ////установка соединения с использованием  //данных IP и номера порта 
                                //client.Connect(endPoint);
                                ////получение сетевого потока
                                //NetworkStream nstream = client.GetStream();
                                ////преобразование строки сообщения в массив байт
                                //byte[] barray = Encoding.Unicode.GetBytes("Client: " + user.Name + " UnicNimber: " + user.UnicNimber + " Status: " + user.Status + " ConnectionTime: " + user.ConnectionTime.ToString("h:mm:ss tt") + " Message: " + user.Message + "<<E!N!D>> " + "\r\n");// "<<E!N!D>> " );//+ message);
                                //                                                                                                                                                                                                                                  //запись в сетевой поток всего массива 
                                //nstream.Write(barray, 0, barray.Length);
                                ////закрытие клиента
                                //client.Close();

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
                    }


                }
                else if (user.Status.IndexOf("Connection") > -1)
                {
                    s = user.Message;
                }



                //if (IndexOf > -1)
                //{
                //    s = s.Substring(IndexOf+9, s.Length -(IndexOf +9));//IndexOf
                //}


                //добавление полученного сообщения в список 
                //Application.Current.Dispatcher.Invoke(() => messageList.Items.Add("dfsdfds"));

                if (s != null)
                {
                    Application.Current.Dispatcher.Invoke(() => messages.Add(s));
                }
                // }

                // IndexOf = s.IndexOf("<<D!I!S!C1ONNECT>>");//9
                //  if (IsDisconnect)
                if (user.Status.IndexOf("Disconnect") > -1)
                {
                    if (currentUser == -1)//if user not old
                    {
                        currentUser = users.Count;// if user new
                    }

                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {
                        users.RemoveAt(currentUser);
                    });
                }
                currentUser = -1;

                //  messageList.Items.Add(s);
                cl.Close();
            }

        }




        public void Connect2()
        {
            list.Start();


            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    var client = list.AcceptTcpClient();


                    Task.Factory.StartNew(() =>
                    {
                        var sr = new StreamReader(client.GetStream(), Encoding.Unicode);
                        while (client.Connected)
                        {
                            var line = sr.ReadLine();

                            User user = new User();
                            user.Pars(line);

                            if (users.FirstOrDefault(s => s.UnicNimber == user.UnicNimber) == null)// нет такого usera
                            {

                                User user1 = new User(client);
                                user1.Pars(line);
                                //    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                //    {
                                //    //  users.Add(user1);
                                //    //  users.Add(new User (client));
                                //});

                                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                {
                                    users.Add(user1);
                                    messages.Add("User: " + user1.Name +user1.ConnectionTime.ToString() + System.Environment.NewLine + " connected");
                                   // messages.Add(user1.Name + ": " + user1.ConnectionTime.ToString() + System.Environment.NewLine + user1.Message);
                                    //SelectedIndex = messages.Count;

                                    //  mylistBox.Focus();
                                    //  mylistBox.SelectedIndex = messages.Count - 1;

                                    //   var el = mylistBox.Item;
                                    mylistBox.SelectedItem = mylistBox.Items[messages.Count - 1];
                                    mylistBox.ScrollIntoView(mylistBox.SelectedItem);

                                //   mylistBox.TopIndex = mylistBox.Items.Count - 1;

                                    //mylistBox.UpdateLayout();   

                                    //var listBoxItem = (ListBoxItem)mylistBox.ItemContainerGenerator.ContainerFromItem(mylistBox.SelectedItem);
                                    //listBoxItem.Focus();
                                  
                                    // ((ListBoxItem)mylistBox.SelectedItem).Focus();

                                    // MySelectedIndex = messages.Count-1;

                                    //  messages.Add(line);
                                });
                                SendToAllClients($"{user1.Name}: connected");
                                break;
                            }
                            else
                            {
                                var sw = new StreamWriter(client.GetStream(), Encoding.Unicode);
                                sw.AutoFlush = true;

                                sw.WriteLine("You already connected");
                                client.Client.Disconnect(false);
                            }
                        }


                        while (client.Connected)
                        {
                            try
                            {
                                sr = new StreamReader(client.GetStream(), Encoding.Unicode);
                                var line = sr.ReadLine();

                                if (line != null)
                                {
                                    User user1 = new User(client);
                                    user1.Pars(line);

                                    if (user1.Status.IndexOf("Disconnected") > -1)
                                    {
                                        for (int i = 0; i < users.Count; i++)
                                        {
                                            if (users[i].UnicNimber == user1.UnicNimber)
                                            {
                                                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                                {
                                                    users.RemoveAt(i);
                                                });
                                            }

                                        }
                                    }


                                    //if (user1.Status.IndexOf("Disconnected") > -1)
                                    //{
                                    //    if (users.FirstOrDefault(s => s.UnicNimber == user1.UnicNimber) != null)// есть такого usera
                                    //    {
                                    //        User user2 = users.FirstOrDefault(s => s.UnicNimber == user1.UnicNimber);
                                    //        user2.Status = "Disconnected";
                                    //    }

                                    //}

                                    SendToAllClients($"{user1.Name}: {user1.Message}");
                                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                    {
                                        messages.Add(user1.Name + ": " + user1.ConnectionTime.ToString() + System.Environment.NewLine+ user1.Message);
                                        // SelectedIndex = messages.Count;
                                        //MySelectedIndex = messages.Count - 1;


                                        //   var el = mylistBox.Item;
                                        mylistBox.SelectedItem = mylistBox.Items[messages.Count - 1];

                                        //mylistBox.UpdateLayout(); // Pre-generates item containers 

                                        //var listBoxItem = (ListBoxItem)mylistBox.ItemContainerGenerator.ContainerFromItem(mylistBox.SelectedItem);
                                        //listBoxItem.Focus();
                                        mylistBox.ScrollIntoView(mylistBox.SelectedItem);


                                        //mylistBox.Focus();
                                        //mylistBox.SelectedIndex = messages.Count - 1;
                                        //((ListBoxItem)mylistBox.SelectedItem).Focus();    
                                    });

                                }

                            }
                            catch (Exception)//если клиент закрыл окно и не disconnected
                            {
                                for (int i = 0; i < users.Count; i++)
                                {
                                    if (users[i].Client.Connected == false)
                                    {
                                        App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                          {

                                              messages.Add("User: " + users[i].Name + users[i].ConnectionTime.ToString() + System.Environment.NewLine + " disconnected");

                                              
                                              mylistBox.SelectedItem = mylistBox.Items[messages.Count - 1];
                                              mylistBox.ScrollIntoView(mylistBox.SelectedItem);
                                              //  messages.Add("User: " + user1.Name + user1.ConnectionTime.ToString() + System.Environment.NewLine + " connected");
                                              users.RemoveAt(i);
                                              i--;

                                          });
                                    }
                                    else
                                    {
                                        SendToAllClients(users[i].Name + " disconnected");
                                    }
                                }

                              // MessageBox.Show("прога закрылась некорректно нужно удалить соедтнение со стороны сервера , на клиенте уже удалился сам ");
                                //возможно коиент разорвал соелинение , тю.е. окно закрыл
                                // throw;
                            }
                        }

                    });
                }
            });
        }

        public async void Unicast(string message, User selectedItem)
        {

            await Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < users.Count; i++)
                {
                    if ( selectedItem.ToString().IndexOf(users[i].UnicNimber.ToString())>-1)
                    {
                        try
                        {
                            var sw = new StreamWriter(users[i].Client.GetStream(), Encoding.Unicode);
                            sw.AutoFlush = true;
                            sw.WriteLine("Server: " + message);
                        }
                        catch
                        {
                        }

                    }
                }


            });
        }
            


        public async void SendToAllClients(string message)
        {
            await Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < users.Count; i++)
                {
                    try
                    {// new StreamWriter(client.GetStream());
                        var sw = new StreamWriter(users[i].Client.GetStream(), Encoding.Unicode);
                        sw.AutoFlush = true;

                        sw.WriteLine(message);

                        //if (users[i].Status.IndexOf("Disconnected") == -1)
                        //{
                        //    sw.WriteLine(message);
                        //}
                        //else {
                        //    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                        //    {
                        //        users.RemoveAt(i);
                        //    });
                        //}
                    }
                    catch (Exception)
                    {
                        //if (users[i].Status.IndexOf("Disconnected") > -1)
                        //{

                        //}
                        //..                    //delete
                        //                      throw;
                    }
                }
            });
        }


        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

}
