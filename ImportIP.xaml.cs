using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
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
    public partial class ImportIP : Window
    {
        public MyViewModel Model { get; set; }

        public ImportIP(MyViewModel model)
        {
            InitializeComponent();

            Model = model;

            this.DataContext = Model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            StringReader reader = new StringReader(List.Text);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Model.MyList.Add(new server { Addr = line, User = Model.Username, Port = Model.Port, Password = "********************"+ this.Password.Password, Command = Model.Cmd });
            };
            Close();
        }
    }
}
