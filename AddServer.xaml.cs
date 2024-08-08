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
using System.Windows.Shapes;

namespace MultiSSH_Manager
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddServer : Window
    {
        public MyViewModel Model { get; set; }

        public AddServer(MyViewModel model)
        {
            InitializeComponent();
          //  Model = new MyViewModel();
            Model = model;
            this.DataContext = Model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // MainWindow main = new MainWindow();
            // main.MainList.Items.Add(new { Addr = Address.Text,User=Username.Text,Port=Port.Text, Password = Password.Text,Command=Command.Text });
            //main.mMyList.Add( new serverlist { Addr = Address.Text, User = Username.Text, Port = Port.Text, Password = Password.Text, Command = Command.Text });
           // var firstNameColumn = new DataGridViewTextBoxColumn();
           // firstNameColumn.HeaderText = "FirstName";
            Model.MyList.Add(new server { Addr = Address.Text, User = Username.Text, Port = Port.Text, Password = "********************" + Password.Password, Command = Command.Text  });
        }
    }
}
