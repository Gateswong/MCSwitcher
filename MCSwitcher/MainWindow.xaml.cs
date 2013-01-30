using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Xml.Linq;

namespace MCSwitcher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RefreshFolders();
            SetLastSelection();
            LoadButtons();
        }

        private void LoadButtons()
        {
            try
            {
                XDocument xdoc = XDocument.Load(@"buttons");
                foreach (XElement x in xdoc.Element("Buttons").Elements("Button"))
                {
                    Button btn = new Button();
                    btn.Content = x.Attribute("Text").Value;
                    btn.Width = Double.NaN;
                    string launchCommand = x.Attribute("Cmd").Value;
                    int flags;
                    if (x.Attribute("Flags") != null)
                    {
                        flags = int.Parse(x.Attribute("Flags").Value);
                    }
                    else { flags = 0; }
                    btn.Margin = new Thickness(8, 8, 8, 0);
                    btn.Click += new RoutedEventHandler(new Action<object, EventArgs>((object sender, EventArgs e) =>
                    {
                        string cmd = launchCommand;
                        Launch(cmd, flags);
                    }));
                    panelButtons.Children.Add(btn);
                }
            } catch {
                MessageBox.Show("打开配置文件出错！请检查buttons文件是否存在，以及其中的内容是否正确！或者联系作者！");
                Button btn = new Button();
                btn.Content = "Minecraft.exe";
                btn.Width = Double.NaN;
                btn.Margin = new Thickness(8, 8, 8, 8);
                btn.Click += new RoutedEventHandler(new Action<object, EventArgs>(
                    (object sender, EventArgs e) => {
                        Launch("Minecraft.exe", 0);
                    }));
                panelButtons.Children.Add(btn);
            }
        }

        private void SetLastSelection()
        {
            try
            {
                lock (f)
                {
                    System.IO.StreamReader r = new System.IO.StreamReader(@"lastclient");
                    LastSelected = r.ReadLine();
                    r.Close();
                }
            }
            catch { }
        }

        private bool MakeLink(string dstFolder)
        {
            System.IO.DirectoryInfo link = new System.IO.DirectoryInfo(
                String.Format(@"{0}\.minecraft",
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)));
            Cmd("rd " + link.FullName);

            System.IO.DirectoryInfo link2 = new System.IO.DirectoryInfo(
                String.Format(@".minecraft"));
            Cmd("rd " + link2.FullName);

            string cmd1 = string.Format(@"mklink /D ""{0}"" ""{1}""", link.FullName, dstFolder);
            string cmd2 = string.Format(@"mklink /D ""{0}\{1}"" ""{2}""", Environment.CurrentDirectory, link.Name, dstFolder);


            Cmd(cmd1);
            Cmd(cmd2);
            return true;
        }

        private void Cmd(string cmd)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;

            //MessageBox.Show(cmd);
            p.Start();
            p.StandardInput.WriteLine(cmd);
            p.WaitForExit(200);
        }


        System.IO.FileInfo f = new System.IO.FileInfo(@"lastclient");

        private void Launch(string filename, int flags)
        {
            try
            {
                if ((flags & 0x0002) == 0)
                {
                    MakeLink(string.Format(@"{0}\clients\{1}",
                        Environment.CurrentDirectory,
                        CurrentSelection));
                }
                System.Diagnostics.Process.Start(filename);
                lock (f)
                {
                    if (f.Exists) { f.Delete(); }
                }
                lock (f)
                {
                    System.IO.StreamWriter lastClient = new System.IO.StreamWriter(@"lastclient");
                    lastClient.WriteLine(CurrentSelection);
                    lastClient.Close();
                }
                if ((flags & 0x0001) == 0) { Environment.Exit(0); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("程序出错了！" +
                    (Environment.GetCommandLineArgs().Count(x => x == "-debug") > 0 ?
                    ex.Message + Environment.NewLine + ex.StackTrace : "")
                    );
                System.Diagnostics.Debug.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private string CurrentSelection { get; set; }
        private string LastSelected { get; set; }
        private List<uiClient> Clients = new List<uiClient>();

        private void RefreshFolders()
        {
            System.Threading.Thread t = new System.Threading.Thread(() =>
            {
                
                System.IO.DirectoryInfo folders = new System.IO.DirectoryInfo("clients");
                Clients.Clear();
                CurrentSelection = null;
                this.Dispatcher.Invoke(new Action(() =>
                {
                    panelClients.Children.Clear();
                }));

                if (!folders.Exists) { folders.Create(); }

                foreach (var x in folders.GetDirectories())
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        uiClient newUI = new uiClient();
                        newUI.FolderName = x.Name;
                        newUI.Height = 72;

                        newUI.MouseLeftButtonDown += new MouseButtonEventHandler(newUI_MouseLeftButtonDown);
                        Clients.Add(newUI);
                        panelClients.Children.Add(newUI);
                        if (newUI.FolderName == LastSelected) { newUI.IsSelected = true; CurrentSelection = x.Name; }
                    }));
                }

                if (Clients.Count == 0) { 
                    MessageBox.Show(@"没有发现客户端！请在clients目录下创建文件夹并将"".minecraft""目录下的所有文件移动至该目录中。");
                    System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(Environment.CurrentDirectory);
                    System.Diagnostics.Process.Start("explorer.exe", d.FullName);
                }
                else
                {
                    if (CurrentSelection == null) { this.Dispatcher.Invoke(new Action(() => { Clients[0].IsSelected = true; CurrentSelection = Clients[0].FolderName; })); }
                    else
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            for (int m = 0; m < Clients.Count; m++)
                            {
                                if (Clients[m].FolderName == CurrentSelection)
                                {
                                    scrollViewer2.ScrollToVerticalOffset(m * 72);
                                    break;
                                }
                            }
                        }));
                    }
                }

            });
            t.Start();
        }

        void newUI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CurrentSelection = (sender as uiClient).FolderName;
            try
            {
                Clients.First(x => x.IsSelected == true).IsSelected = false;
            }
            catch { }
            (sender as uiClient).IsSelected = true;
            
        }

        public delegate void SelectClientEventDeleate(object sender, SelectClientEventArgs e);
        public event SelectClientEventDeleate SelectClient;

        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(Environment.CurrentDirectory);
            System.Diagnostics.Process.Start("explorer.exe", d.FullName);
        }

        private void btnRescan_Click(object sender, RoutedEventArgs e)
        {
            RefreshFolders();
        }
    }

    public class SelectClientEventArgs : EventArgs
    {

    }
}
