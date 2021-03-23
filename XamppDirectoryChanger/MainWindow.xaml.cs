using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace XamppDirectoryChanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            init_app();

        }

        private void init_app()
        {
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "/data"))
            {
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "/data");
            }

            if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "/data/stored_path.dat"))
            {
                httpd_conf_path.Text = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/data/stored_path.dat");
            }
            else
            {
                if (!findXamppInstalation())
                {
                    httpd_conf_path.Text = @"C:\xampp\apache\conf\httpd.conf";
                }
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/data/stored_path.dat", httpd_conf_path.Text.ToString());
            }
        }

        private void resetPath()
        {
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "/data"))
            {
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "/data");
            }

            if (!findXamppInstalation())
            {
                httpd_conf_path.Text = @"C:\xampp\apache\conf\httpd.conf";
            }
            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/data/stored_path.dat", httpd_conf_path.Text.ToString());
            
        }

        private bool findXamppInstalation()
        {
            bool isBasePathSet = false;
            RegistryKey BaseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey Software = null;
            RegistryKey xampp = null;
            if (BaseKey != null)
            {
                Software = BaseKey.OpenSubKey("SOFTWARE");
                if (Software != null)
                {
                    xampp = Software.OpenSubKey("xampp");

                }
            }
            if (xampp == null)
            {
                MessageBox.Show("xampp was not found a default value will be set! please install or specify the location of the httpd.conf in options!");
            }
            else
            {
                object objRegisteredValue = xampp.GetValue("Location");

                if (objRegisteredValue != null)
                {
                    httpd_conf_path.Text = objRegisteredValue.ToString() + "\\apache\\conf\\httpd.conf";
                    isBasePathSet = true;
                }
            }
            return isBasePathSet;
        }

        private void Reset_Path_Click(object sender, RoutedEventArgs e)
        {
            resetPath();
        }

        private void Select_File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == true)
            {
                httpd_conf_path.Text = fd.FileName;
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/data/stored_path.dat", httpd_conf_path.Text.ToString());
            }
        }
    }
}
