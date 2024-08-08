using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace MultiSSH_Manager
{
    public class MyViewModel : INotifyPropertyChanged
    {
        
        private ObservableCollection<server> myList = new ObservableCollection<server>();
        public event PropertyChangedEventHandler PropertyChanged;

     
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public ObservableCollection<server> MyList
        {
            get { return myList; }
            set
            {
                if (value != myList)
                {
                    myList = value;
                    OnPropertyChanged();

                }
            }
        }
       
        private string _Username="root";

        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                _Username = value;
                OnPropertyChanged("Username");
            }
        }
        private string _Port = "22";

        public string Port
        {
            get
            {
                return _Port;
            }
            set
            {
                _Port = value;
                OnPropertyChanged("Port");
            }
        }
        private string _Cmd;

        public string Cmd
        {
            get
            {
                return _Cmd;
            }
            set
            {
                _Cmd = value;
                OnPropertyChanged("Cmd");
            }
        }
    }
}
