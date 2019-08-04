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

namespace ClientChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       // public string Message{ get; set; }

        Client client = null;
        public MainWindow()
        {
            InitializeComponent();

            /////// client = new Client();
            //  client = new Client(Ip.Text, Port.Text);

            client = new Client(Ip.Text, Port.Text,Name.Text, messageList);
            client.MainViewModel();
            messageList.ItemsSource = Colections.messages;
        }

        private void CategoriestabItem1_Clicked(object sender, MouseButtonEventArgs e)
        {

        }

        private void ClientSendMessage(object sender, RoutedEventArgs e)
        {
            if (client!=null)
            {
                //client.Status = "SendMessage";
                client.send(CommentTextBox.Text);
                //clian text field
                CommentTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("you need to connect to the server");
                CommentTextBox.Text = "";
            }
           


        }
        //private void ClientSendMessage(object sender, RoutedEventArgs e)
        //{
        //    //if (client != null)
        //    //{
        //    //    client.Status = "SendMessage";
        //    //    client.Message = client.Name.ToString() + ": " + CommentTextBox.Text;
        //    //    // client.Connection( client.Name.ToString()+": "+ CommentTextBox.Text+ "\r\n");
        //    //    client.Connection();
        //    //    //  messageList.Items.Add(client.Name.ToString()+": "+ CommentTextBox.Text);
        //    //    client.messages.Add(client.Name.ToString() + ": " + CommentTextBox.Text);
        //    //}
        //    //else
        //    //{
        //    //    MessageBox.Show("You need to connect to the server");
        //    //}
        //}






        private void SetConnection(object sender, RoutedEventArgs e)
        {
            if (client==null)
            {
                client = new Client(Ip.Text, Port.Text, Name.Text, messageList);
                client.MainViewModel();
                messageList.ItemsSource = Colections.messages;
            }

            client.Name = Name.Text;
            client.ConnectCommand(Ip.Text, Port.Text);
            client.Message = "Сlient: " + client.Name.ToString() + " connected to server!";

        }


        //private void SetConnection(object sender, RoutedEventArgs e)
        //{
        //    if (client==null)
        //    {               
        //        client = new Client(Ip.Text, Port.Text, Name.Text);  //client  Status = "Connection";
        //    }
        //    messageList.ItemsSource = client.messages;

        //        if (client.IsBusy == 1)//free
        //    {           //client  Status = "Connection";
        //        client.Message = "Сlient: " + client.Name.ToString() + " connected to server!";
        //        //  client.Connection("Сlient: " + client.Name.ToString() + " connected to server!\r\n");
        //        client.Connection();

        //        client.messages.Add("Сlient: " + client.Name.ToString() + " connected to server!");
        //       // messageList.Items.Add("Сlient: " + client.Name.ToString() + " connected to server!");
        //        //   toglebutton.IsChecked = false;
        //    }
        //    else {
        //       MessageBox.Show("You cant connected to server. Connection has already been established");
        //    }
        //}



        private void Disconnection(object sender, RoutedEventArgs e)
        {
            if (client != null)
            {
                client.Disconnect2();
                client = null;
                Name.Text = "";
                //Flipper.FrontContent = focus
               // Flipper.
            }
            else
            {
                MessageBox.Show("you need to connect to the server");
            }
        }

        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tabControl1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }


        //private void Disconnection(object sender, RoutedEventArgs e)
        //{
        //    if (client!=null)
        //    {
        //        client.Status = "Disconnect";

        //        client.messages.Add("Сlient:  " + client.Name.ToString() + "  disconnected from server!");
        //        //  messageList.Items.Add("Сlient:  " + client.Name.ToString() + "  disconnected from server!");
        //        client.Disconnection();
        //        client = null;
        //    }
        //    else
        //    {
        //        MessageBox.Show("You already disconected");
        //    }


        //}
    }
}
