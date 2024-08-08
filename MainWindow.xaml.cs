using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Renci.SshNet;
using System;
using System.Diagnostics;
using System.Windows.Threading;
using System.Security.Policy;

namespace MultiSSH_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class server
    {
        public string Addr { get; set; }
        public string User { get; set; }
        public string Port { get; set; }
        public string Password { get; set; }
        public string Command { get; set; }
        public string Status { get; set; }

    }

    public partial class MainWindow : Window
    {

        public MyViewModel Model { get; set; }
        public string logfile;

        public MainWindow()
        {
            InitializeComponent();

            Model = new MyViewModel();

            this.DataContext = Model;
            this.MainList.ItemsSource = Model.MyList;
            //this.MainList.Columns[3].Visibility = Visibility.Hidden;
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DragBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //  if (e.ChangedButton == MouseButton.Left)
            // { this.DragMove(); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddServer(Model).Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new ImportIP(Model).Show();
        }

        private void DeleteRow(object sender, RoutedEventArgs e)
        {
            if (MainList.SelectedIndex >= 0)
            {
                Model.MyList.RemoveAt(this.MainList.SelectedIndex);
            }
        }
        public async Task Runcmd(string address, string user , int port,string pass, string command,int index)
        {
            var log="";
            Model.MyList[index].Status = "In Progress";
            Task task = Task.Run(() =>
            {
                ConnectionInfo connnfo = new ConnectionInfo(address, port, user,
                 new AuthenticationMethod[]{
                     new PasswordAuthenticationMethod(user,pass.Trim()),
                 }
               );
                try
                {
                    var sshclient = new SshClient(connnfo);
                    sshclient.Connect();
                    var cmd = sshclient.CreateCommand(command);
                    string result = cmd.Execute();
                    sshclient.Disconnect();
                    log = "[" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + "] " + "[" + address + "][Output] : " + result + Environment.NewLine;
                    File.AppendAllText(logfile, log);
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => {
                        logs.AppendText(log + Environment.NewLine);
                        Model.MyList[index].Status = "Successful";
                        MainList.ItemsSource = null;
                        MainList.ItemsSource = Model.MyList;
                    }));

                    //   return "Successful";

                }
                catch (Exception except)
                {
                    log = "[" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + "] " + "[" + address + "][Output] : " + except.Message;
                    File.AppendAllText(logfile, log);
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => {
                        logs.AppendText(log + Environment.NewLine);
                        Model.MyList[index].Status = "Failed";
                        MainList.ItemsSource = null;
                        MainList.ItemsSource = Model.MyList;
                    }));
                   ;
                //   return "Failed";

                }
            });
            await task;
           // return "In Progress";
        }

        private async void Start(object sender, MouseButtonEventArgs e)
        {
            this.PowerButton.Source = new BitmapImage(new Uri("Assets/stop.png", UriKind.Relative));
            var res="";
            PowerButton.IsEnabled = false;
            logfile = Directory.GetCurrentDirectory() + "\\"+ DateTime.Now.Day.ToString()+"-"+DateTime.Now.Day.ToString()+"-"+DateTime.Now.Day.ToString()+"-"+DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "-Res.txt";
            File.AppendAllText(logfile, "========= Proccess Started ========= " + Environment.NewLine);
            foreach (var (server, index) in Model.MyList.Select((v, i) => (v, i)))
            {
                Model.MyList[index].Status = "In Progress";
                MainList.ItemsSource = null;
                MainList.ItemsSource = Model.MyList;
                await Runcmd(server.Addr.Trim(), server.User, int.Parse(server.Port), server.Password.Substring(20), server.Command, index);
            }
            File.AppendAllText(logfile,
            "========= FINISH ========= " + Environment.NewLine + "https://github.com/SinaXhpm/MultiSSHCommander" + Environment.NewLine);
            this.logs.AppendText("Log File Saved - "+logfile+".txt" + Environment.NewLine);
            this.PowerButton.Source = new BitmapImage(new Uri("Assets/start.png", UriKind.Relative));
            PowerButton.IsEnabled = true;
        }

        private void OpenLog(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe", logfile);
        }

        private void OpenGit(object sender, RoutedEventArgs e)
        {
            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = "https://github.com/SinaXhpm/MultiSSHCommander";
            myProcess.Start();
        }
    }
}
