using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatMulty.ViewModels;
using ChatMulty.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace ChatMulty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Server server;
        public MainWindow()
        {
            InitializeComponent();

            server = new Server("127.0.0.1", "11000", messageList);
            //DataContext = server.SelectedIndex;


            connectionList.ItemsSource = server.users;


            // connectionList.HorizontalScrollbar = true;
            // messageList.DataContext = server;
            messageList.ItemsSource = server.messages;
            // messageList.Focus();

            // messageList.SelectedIndex = server.SelectedIndex;


            // messageList.Items.Add("Server conected to ip: 127.0.0.1, port: 11000");
            // server.Connect();



            //    string[] animal_array = { "ape", "bear", "cat",
            //"dolphin", "eagle", "fox", "giraffe" };
            //    List <string>animal_list = new List(animal_array);


            // connectionList.DataSource = animal_list;



            //List<TodoItem> items = new List<TodoItem>();
            //items.Add(new TodoItem() { Title = "Complete this WPF tutorial", Completion = 45 });
            //items.Add(new TodoItem() { Title = "Learn C#", Completion = 80 });
            //items.Add(new TodoItem() { Title = "Wash the car", Completion = 0 });

            //lbTodoList.ItemsSource = items;

         //   DataContext = new ListsAndGridsViewModel();

            server.Connect2();
        }

        // public void sMain(){  server.Connect2();}

        private void ServerSendMessage(object sender, RoutedEventArgs e)
        {

            //    sMain();
            // string reply = ServerMessage.Text;
            //server.Send(reply);
        }

        private void Broadcast(object sender, RoutedEventArgs e)
        {

            if (server.users.Count > 0)
            {
                if (SendToAll.Text != "")
                {
                    server.SendToAllClients("Server: " + SendToAll.Text);
                    server.messages.Add("Server: " + DateTime.Now.ToLongTimeString() + System.Environment.NewLine + SendToAll.Text);
                    messageList.SelectedItem = messageList.Items[server.messages.Count - 1];
                    messageList.ScrollIntoView(messageList.SelectedItem);
                }
                else
                {
                    MessageBox.Show("You cant send empty message");
                }
            }
            else
            {
                MessageBox.Show("There are no customers");
            }
            SendToAll.Text = "";


        }









        private void Unicast(object sender, RoutedEventArgs e)
        {
            bool isSelected = false;
            for (int i = 0; i < server.users.Count; i++)
            {
                if (server.users[i].IsSelected== true)
                {
                    if (UnicastCl.Text != "")
                    {
                      
                        server.Unicast(UnicastCl.Text, server.users[i]);
                        server.messages.Add("Server: " + DateTime.Now.ToLongTimeString() + System.Environment.NewLine + UnicastCl.Text);
                        messageList.SelectedItem = messageList.Items[server.messages.Count - 1];
                        messageList.ScrollIntoView(messageList.SelectedItem);
                        isSelected = true;
                    }
                    else
                    {
                        MessageBox.Show("You cant send empty message");
                    }
                }
            }
            if (!isSelected )
            {
                MessageBox.Show("Select user to send message");
            }

            //if (connectionList.SelectedItem != null)
            //{
            //    if (UnicastCl.Text != "")
            //    {
            //        User i = (User)connectionList.SelectedItem;
            //        server.Unicast(UnicastCl.Text, i);
            //        server.messages.Add("Server: " + DateTime.Now.ToLongTimeString() + System.Environment.NewLine + UnicastCl.Text);
            //        //messageList.SelectedItem = messageList.Items[server.messages.Count - 1];
            //        //messageList.ScrollIntoView(messageList.SelectedItem);
            //    }
            //    else
            //    {
            //        MessageBox.Show("You cant send empty message");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Select user to send message");
            //}
            UnicastCl.Text = "";
        }
















        //private void lbTodoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (lbTodoList.SelectedItem != null)
        //        this.Title = (lbTodoList.SelectedItem as TodoItem).Title;
        //}

        //private void btnSelectCSharp_Click(object sender, RoutedEventArgs e)
        //{
        //    foreach (object o in lbTodoList.Items)
        //    {
        //        if ((o is TodoItem) && ((o as TodoItem).Title.Contains("C#")))
        //        {
        //            lbTodoList.SelectedItem = o;
        //            break;
        //        }
        //    }
        //}
    }


    public class TodoItem
    {
        public string Title { get; set; }
        public int Completion { get; set; }
    }




















    public class ListsAndGridsViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<SelectableViewModel> _items1;
        private readonly ObservableCollection<SelectableViewModel> _items2;
        private readonly ObservableCollection<SelectableViewModel> _items3;
        private bool? _isAllItems3Selected;

        public ListsAndGridsViewModel()
        {
            _items1 = CreateData();
            _items2 = CreateData();
            _items3 = CreateData();
        }

        public bool? IsAllItems3Selected
        {
            get { return _isAllItems3Selected; }
            set
            {
                if (_isAllItems3Selected == value) return;

                _isAllItems3Selected = value;

                if (_isAllItems3Selected.HasValue)
                    SelectAll(_isAllItems3Selected.Value, Items3);

                OnPropertyChanged();
            }
        }

        private static void SelectAll(bool select, IEnumerable<SelectableViewModel> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }

        private static ObservableCollection<SelectableViewModel> CreateData()
        {
            return new ObservableCollection<SelectableViewModel>
            {
                new SelectableViewModel
                {
                    Code = 'M',
                    Name = "Material Design",
                    Description = "Material Design in XAML Toolkit"
                },
                new SelectableViewModel
                {
                    Code = 'D',
                    Name = "Dragablz",
                    Description = "Dragablz Tab Control",
                    Food = "Fries"
                },
                new SelectableViewModel
                {
                    Code = 'P',
                    Name = "Predator",
                    Description = "If it bleeds, we can kill it"
                }
            };
        }

        public ObservableCollection<SelectableViewModel> Items1 => _items1;
        public ObservableCollection<SelectableViewModel> Items2 => _items2;

        public ObservableCollection<SelectableViewModel> Items3 => _items3;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerable<string> Foods
        {
            get
            {
                yield return "Burger";
                yield return "Fries";
                yield return "Shake";
                yield return "Lettuce";
            }
        }
    }



    //public class ListsAndGridsViewModel : INotifyPropertyChanged
    //{
    //    private readonly ObservableCollection<User> _items2;
      
    //    private static void SelectAll(bool select, IEnumerable<SelectableViewModel> models)
    //    {
    //        foreach (var model in models)
    //        {
    //            model.IsSelected = select;
    //        }
    //    }

    //    public ListsAndGridsViewModel()
    //    {
          
    //        _items2 = CreateData();
            
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    private static ObservableCollection<User> CreateData()
    //    {
    //        return new ObservableCollection<User>
    //        {
    //            new User
    //            {

    //                Name = "Dragablz",
    //                ConnectionTime = DateTime.Now

    //            },
    //            new User
    //            {
    //                   Name = "Dragablz",
    //                ConnectionTime = DateTime.Now
    //            },
    //            new User
    //            {

    //                Name = "Predator",
    //                ConnectionTime = DateTime.Now
    //            }
    //        };
    //    }

    //    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }

  

    //    public ObservableCollection<User> Items2 => _items2;


    //}
}
