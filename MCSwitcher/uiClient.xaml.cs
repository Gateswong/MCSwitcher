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

namespace MCSwitcher
{
    /// <summary>
    /// uiClient.xaml 的交互逻辑
    /// </summary>
    public partial class uiClient : UserControl
    {
        public uiClient()
        {
            InitializeComponent();

            imgIcon.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/Minecraft.png"));
        }

        public string ClientName
        {
            get { return lbName.Content as string; }
            set { lbName.Content = value; }
        }

        public string ClientInfo
        {
            get { return lbInfo.Content as string; }
            set { lbInfo.Content = value; }
        }

        private string folderName;
        public string FolderName
        {
            get { return folderName; }
            set
            {
                folderName = value;

                System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(@"(.*) \((.*)\)\s*$");
                if (regEx.IsMatch(value))
                {
                    ClientName = regEx.Match(value).Groups[1].Value;
                    ClientInfo = "版本 " + regEx.Match(value).Groups[2].Value;
                }
                else
                {
                    ClientName = value;
                    ClientInfo = "";
                }

                if (System.IO.File.Exists(Environment.CurrentDirectory + @"\clients\" + FolderName + @"\Icon.png"))
                {
                    BitmapImage img = new BitmapImage();
                    Uri u = new Uri(Environment.CurrentDirectory + @"\clients\" + FolderName + @"\Icon.png", UriKind.Absolute);
                    imgIcon.Source = new BitmapImage(u);
                }
                
            }
        }

        public string ClientIcon
        {
            set
            {
                (imgIcon.Source as BitmapImage).UriSource = new Uri(@"clients\" + FolderName + @"\Icon.png", UriKind.Relative);
            }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            border1.Background = (Resources["mov_bg"] as LinearGradientBrush);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsSelected)
            {
                border1.Background = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                border1.Background = (Resources["sel_bg"] as LinearGradientBrush);
                border1.BorderBrush = (Resources["sel_br"] as LinearGradientBrush);
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                if (!IsSelected)
                {
                    border1.Background = new SolidColorBrush(Colors.Transparent);
                }
                else
                {
                    border1.Background = (Resources["sel_bg"] as LinearGradientBrush);
                    border1.BorderBrush = (Resources["sel_br"] as LinearGradientBrush);
                }
            }
        }
    }
}
