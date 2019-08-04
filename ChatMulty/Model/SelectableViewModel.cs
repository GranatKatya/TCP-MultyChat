using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChatMulty.Model
{
    public class SelectableViewModel : INotifyPropertyChanged
    {
        public SelectableViewModel()
        {

        }

        private bool _isSelected;
        private string _name;
        private string _description;
        private char _code;
        private double _numeric;
        private string _food;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public char Code
        {
            get { return _code; }
            set
            {
                if (_code == value) return;
                _code = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public double Numeric
        {
            get { return _numeric; }
            set
            {
                if (_numeric == value) return;
                _numeric = value;
                OnPropertyChanged();
            }
        }

        public string Food
        {
            get { return _food; }
            set
            {
                if (_food == value) return;
                _food = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }






    public class User : INotifyPropertyChanged
    {

        public User() { }
        public User(TcpClient client) { Client = client; }



        public User(string ip, string port, string name, Guid guid)
        {
            Name = name;
            Ip = ip;
            Port = port;

            // Create and display the value of two GUIDs.
            UnicNimber = guid;
            IsBusy = 1;//busy
        }




        private bool _isSelected;

        private string _name;
        private string _ip;
        private string _port;
        private Guid _unicNimber;
        private DateTime _connectionTime;
        private string _status;
        private string _message;
        private TcpClient _clien;





        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Ip
        {
            get { return _ip; }
            set
            {
                if (_ip == value) return;
                _ip = value;
                OnPropertyChanged();
            }
        }
        public string Port
        {
            get { return _port; }
            set
            {
                if (_port == value) return;
                _port = value;
                OnPropertyChanged();
            }
        }
        public Guid UnicNimber
        {
            get { return _unicNimber; }
            set
            {
                if (_unicNimber == value) return;
                _unicNimber = value;
                OnPropertyChanged();
            }
        }
        public DateTime ConnectionTime
        {
            get { return _connectionTime; }
            set
            {
                if (_connectionTime == value) return;
                _connectionTime = value;
                OnPropertyChanged();
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                if (_status == value) return;
                _status = value;
                OnPropertyChanged();
            }
        }
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message == value) return;
                _message = value;
                OnPropertyChanged();
            }
        }
        //public NetworkStream Nstream { get; set; }
        public TcpClient Client
        {
            get { return _clien; }
            set
            {
                if (_clien == value) return;
                _clien = value;
                OnPropertyChanged();
            }
        }

        // public TcpClient client { get; set; } = null;
        public int IsBusy { get; set; } = 1;

        public void Pars(string str)
        {
            string str3 = str;
            string str2 = str;
            string str4 = str; string str5 = str; string str6 = str; string str7 = str;

            int IndexOfName = str.IndexOf("Client: ");//8
            int IndexOfUnicNumber = str.IndexOf("UnicNimber: ");//12
            int IndexOfEnd = str.IndexOf("<<E!N!D>>");
            int IndexOfStatus = str.IndexOf("Status: ");
            int IndexOfConnectionTime = str.IndexOf("ConnectionTime: ");//16
            int IndexOfMessage = str.IndexOf("Message: ");//9
                                                          //int IndexOfNstream = str.IndexOf("Nstream: ");//9
            if (IndexOfName > -1 && IndexOfUnicNumber > -1 && IndexOfEnd > -1)
            {

                Name = str3.Substring(IndexOfName + 8, IndexOfUnicNumber - (IndexOfName + 8)); //-1 space
                string g = str2.Substring(IndexOfUnicNumber + 12, IndexOfStatus - (IndexOfUnicNumber + 12));
                UnicNimber = new Guid(str2.Substring(IndexOfUnicNumber + 12, IndexOfStatus - (IndexOfUnicNumber + 12)));//Guid.Parse
                Status = str4.Substring(IndexOfStatus + 8, IndexOfConnectionTime - (IndexOfStatus + 8));
                ConnectionTime = DateTime.Parse(str5.Substring(IndexOfConnectionTime + 16, IndexOfMessage - (IndexOfConnectionTime + 16)));
                Message = str6.Substring(IndexOfMessage + 9, IndexOfEnd - (IndexOfMessage + 9));
                //   Nstream =  ( str7.Substring(IndexOfNstream+9, IndexOfEnd-(IndexOfNstream + 9)));
            }
        }

        public override string ToString()
        {
            return "Client: " + Name + System.Environment.NewLine + " Unicnumber: " + UnicNimber.ToString();
        }






        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
