using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        private void ExecuteCommand(string command)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            var process = Process.Start(processInfo);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("output>>" + e.Data);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("error>>" + e.Data);
            process.BeginErrorReadLine();

            process.WaitForExit();

            Console.WriteLine("ExitCode: {0}", process.ExitCode);
            process.Close();
        }

        private void init_app()
        {

            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "/data"))
            {
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "/data");
            }

            setup_options(base_path, true);
            setup_options(stored_path, false);

            update_lists();

            get_current_website();
        }

        private void setup_options(Label text, bool basePath)
        {
            if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "/data/" + text.Name + ".dat"))
            {
                text.Content = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/data/" + text.Name + ".dat");

            }
            else
            {
                findXamppInstalation( text, basePath);

            }
        }

        private void resetPath(Label text, bool setBasePath)
        {
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "/data"))
            {
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "/data");
            }

            findXamppInstalation(text, setBasePath);

            
        }

        private void findXamppInstalation(Label text, bool setBasePath = true)
        {
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
                if (setBasePath)
                {
                    text.Content = @"C:\xampp";
                }
                else
                {
                    text.Content = @"C:\xampp\apache\conf\httpd.conf";
                }

            }
            else
            {
                object objRegisteredValue = xampp.GetValue("Location");

                if (objRegisteredValue != null)
                {
                    if (setBasePath)
                    {
                        text.Content = objRegisteredValue.ToString();
                    }
                    else
                    {
                        text.Content = objRegisteredValue.ToString() + "\\apache\\conf\\httpd.conf";
                    }
                   
                }
            }

            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/data/" + text.Name + ".dat", text.Content.ToString());

        }

        private void Reset_Path_Click(object sender, RoutedEventArgs e)
        {
            resetPath( stored_path,  false);
        }

        private void Select_File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == true)
            {
                stored_path.Content = fd.FileName;
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/data/stored_path.dat", stored_path.Content.ToString());
            }
        }

        private void Add_Website_Folder_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "/data"))
            {
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "/data");
            }

            if(url.Text != "" && name.Text != "" && Directory.Exists(url.Text))
            {

                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "/data/website_folders.dat"))
                {
                   set_existing_websites();
                }
                else
                {
                    set_websites();
                }
            }


        }

        private void set_websites()
        {
            List<website_folder> website_folders = new List<website_folder>();
            website_folders = add_stored_websites("", website_folders);
            string error = set_stored_websites("", website_folders);

            display_error(error);

            update_lists();

        }

        private void display_error(string error)
        {
            if (error != "")
            {
                MessageBox.Show("Problems occured during " + error +
                    " please make sure that the app can read and write to its installed location", "ERROR");
            }
            else
            {
                url.Text = "";
                name.Text = "";
            }
        }

        private void set_existing_websites()
        {
            List<website_folder> website_folders = new List<website_folder>();

            string error = "";
            string stored_website_file = "";
            get_stored_websites( ref stored_website_file,ref error);

            website_folders = add_stored_websites(stored_website_file, website_folders);

            error = set_stored_websites(error, website_folders);

            display_error(error);

            update_lists();

        }


        private void update_lists()
        {
            string error = "";
            string stored_website_file = "";
            
            get_stored_websites(ref stored_website_file, ref error);

            List<website_folder> website_folders = get_deserialsed_websites(stored_website_file);
            remove_websites_list.Children.Clear();
            main_website_list.Children.Clear();
            for (int i = 0; i < website_folders.Count; i++)
            {
                add_delete_button(website_folders, i);
                add_update_button(website_folders, i);


            }
        }

        private void add_delete_button(List<website_folder> website_folders, int i)
        {
            Button remove_website = new Button();
            remove_website.Content = website_folders[i].name;
            remove_website.Tag = website_folders[i].url;
            remove_website.Height = 40;
            remove_website.VerticalAlignment = VerticalAlignment.Stretch;
            remove_website.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF7979"));
            remove_website.Click += Remove_website_Click;
            remove_websites_list.Children.Add(remove_website);
        }

        private void add_update_button(List<website_folder> website_folders, int i)
        {
            Button update_button = new Button();
        
            update_button.Tag = website_folders[i].url;
            update_button.Height = 50;
            update_button.VerticalAlignment = VerticalAlignment.Stretch;
            update_button.Click += Update_button_Click;
            update_button.HorizontalContentAlignment = HorizontalAlignment.Stretch;

            Grid button_container = new Grid();
            button_container.Name = "button_container";
            button_container.VerticalAlignment = VerticalAlignment.Stretch;
            button_container.HorizontalAlignment = HorizontalAlignment.Stretch;
         

            Label button_description = new Label();
            button_description.Margin = new Thickness(0f, 0f, 40f, 0f);
            button_container.Children.Add(button_description);
            button_description.VerticalContentAlignment = VerticalAlignment.Center;
            button_description.HorizontalContentAlignment = HorizontalAlignment.Center;
            button_description.Content = website_folders[i].name;


            Image red_icon = new Image();
            red_icon.Name = "red_icon";
            red_icon.Width = 20;
            red_icon.Margin = new Thickness( 0f,13f,10f,10f);
            red_icon.HorizontalAlignment = HorizontalAlignment.Right;


            BitmapImage red_icon_bitmap = new BitmapImage();
            red_icon_bitmap.BeginInit();
            red_icon_bitmap.UriSource = new Uri(@"/XamppDirectoryChanger;component/icons/red_alert.png", UriKind.Relative);
            red_icon_bitmap.EndInit();
            red_icon.Source = red_icon_bitmap;
            red_icon.Visibility = Visibility.Visible;
            button_container.Children.Add(red_icon);


            Image green_icon = new Image();
            green_icon.Name = "green_icon"; 
            green_icon.Width = 20;
            green_icon.Margin = new Thickness(0f, 13f, 10f, 10f);
            green_icon.HorizontalAlignment = HorizontalAlignment.Right;
            BitmapImage green_icon_bitmap = new BitmapImage();
            green_icon_bitmap.BeginInit();
            green_icon_bitmap.UriSource = new Uri(@"/XamppDirectoryChanger;component/icons/green_alert.png", UriKind.Relative);
            green_icon_bitmap.EndInit();
            green_icon.Source = green_icon_bitmap;
            green_icon.Visibility = Visibility.Hidden;
            button_container.Children.Add(green_icon);

            update_button.Content = button_container;
            main_website_list.Children.Add(update_button);
        }


        private void get_current_website()
        {
            StreamReader httpd_conf = new StreamReader(stored_path.Content.ToString());
            string line = httpd_conf.ReadLine();
            while (line != null)
            {
                
                string doc_root_pattern = "^\\s*DocumentRoot\\s*\".*\"\\s*$";
                Regex doc_root_rgx = new Regex(doc_root_pattern);
                bool doc_root = doc_root_rgx.IsMatch(line);
                if (doc_root)
                {
                    doc_root_pattern = "\\\"[^\\\"]*\\\"";
                    doc_root_rgx = new Regex(doc_root_pattern);
                    string value = doc_root_rgx.Match(line).Value.Replace("\"", "");
                    
                    for(int i = 0; i < main_website_list.Children.Count; i++)
                    {
                        Button current_button = (Button)main_website_list.Children[i];
                        if(current_button.Tag.ToString() == value)
                        {
                            //current selected button
                            Grid current_button_container = (Grid)current_button.Content;
                            for (int j = 0; j < current_button_container.Children.Count; j++)
                            {
                                try
                                {
                                    Image current_icon = (Image)current_button_container.Children[j];
                                    if (current_icon != null)
                                    {
                                        if (current_icon.Name == "red_icon")
                                        {
                                            current_icon.Visibility = Visibility.Hidden;
                                        }
                                        else if (current_icon.Name == "green_icon")
                                        {
                                            current_icon.Visibility = Visibility.Visible;
                                        }
                                    }

                                }
                                catch (Exception) { }
                            }

                        }
                    }

                    
                    break;
                }
                line = httpd_conf.ReadLine();
            }
        }
        private void Update_button_Click(object sender, RoutedEventArgs e)
        {

             Button update_button = (Button)sender;
            //stored_path = httpd.conf
            if (File.Exists(stored_path.Content.ToString()))
            {
                string line = "";
                StringBuilder new_httpd_conf = new StringBuilder();
                StreamReader httpd_conf = new StreamReader(stored_path.Content.ToString());
                bool doc_root_found = false;
                bool directory_found = false;
                while ( line != null)
                {
                    line = httpd_conf.ReadLine();
                    
                    if(line != null)
                    {
                        proccess_config(ref update_button, ref line, ref new_httpd_conf, ref doc_root_found, ref directory_found);
                    }
                }
                httpd_conf.Close();

                if (!doc_root_found)
                {
                    doc_root_found = add_doc_root_prompt(update_button, new_httpd_conf, doc_root_found);
                }

                if (!directory_found)
                {
                    directory_found = add_directory_prompt(update_button, new_httpd_conf, directory_found);
                }

                if (!directory_found || !doc_root_found)
                {
                    corrupt_prompt(sender, e);
                }
                else
                {
                    
                    try
                    {
                        File.WriteAllText(stored_path.Content.ToString(), new_httpd_conf.ToString());
                        ReloadXampp();
                        reset_button_icons(update_button);
                    }
                    catch (Exception ex){
                        MessageBox.Show(ex.ToString(), ex.Message);
                    }

                    
                }
            }
            else
            {
                corrupt_prompt(sender, e);
            
            }
        }

        private void reset_button_icons(Button update_button)
        {
            for(int i = 0; i < main_website_list.Children.Count; i++)
            {
                Button current_button = (Button)main_website_list.Children[i];
                Grid current_button_container = (Grid)current_button.Content;
                for(int j = 0; j <  current_button_container.Children.Count; j++)
                {
                    try
                    {
                        Image current_icon = (Image)current_button_container.Children[j];
                        if(current_icon != null)
                        {
                            if(current_icon.Name == "red_icon" )
                            {
                                current_icon.Visibility = Visibility.Visible;
                            }else if (current_icon.Name == "green_icon")
                            {
                                current_icon.Visibility = Visibility.Hidden;
                            }
                        }

                    }
                    catch (Exception) { }
                }           
            }
            Grid button_container = (Grid)update_button.Content;
            for (int j = 0; j < button_container.Children.Count; j++)
            {
                try
                {
                    Image current_icon = (Image)button_container.Children[j];
                    if (current_icon != null)
                    {
                        if (current_icon.Name == "red_icon" )
                        {
                            current_icon.Visibility = Visibility.Hidden;
                        }
                        else if(current_icon.Name == "green_icon")
                        {
                            current_icon.Visibility = Visibility.Visible;
                        }
                    }

                }
                catch (Exception) { }
            }
        }

        private void proccess_config(ref Button update_button, ref string line, ref StringBuilder new_httpd_conf, ref bool doc_root_found, ref bool directory_found)
        {
            string doc_root_pattern = "^\\s*DocumentRoot\\s*\".*\"\\s*$";
            string directory_pattern = "^\\s*<\\s*Directory\\s*\".*\"\\s*>\\s*$";
            Regex doc_root_rgx = new Regex(doc_root_pattern);
            Regex directory_rgx = new Regex(directory_pattern);
            bool doc_root = doc_root_rgx.IsMatch(line);
            bool directory = directory_rgx.IsMatch(line);
            if (doc_root || directory)
            {
                if (doc_root)
                {
                    if (!doc_root_found)
                    {
                        doc_root_found = true;

                        new_httpd_conf.AppendLine("DocumentRoot \"" + update_button.Tag.ToString() + "\"");
                    }
                }
                else
                {
                    if (!directory_found)
                    {
                        

                        string cgi_directory_pattern = "^\\s*<\\s*Directory\\s*\"C:/xampp/cgi-bin\"\\s*>\\s*$";
                        Regex cgi_directory_rgx = new Regex(cgi_directory_pattern);
                        bool cgi_base_directory = cgi_directory_rgx.IsMatch(line);
                        string url = base_path.Content.ToString().Replace('\\', '/');
                        string cgi_custom_directory_pattern = "^\\s*<\\s*Directory\\s*\"" + url + "/cgi-bin" + "\"\\s*>\\s*$";
                        Regex cgi_custom_directory_rgx = new Regex(cgi_custom_directory_pattern);
                        bool cgi_custom_directory = cgi_custom_directory_rgx.IsMatch(line);

                        if(!cgi_base_directory || !cgi_custom_directory)
                        {
                            directory_found = true;
                            new_httpd_conf.AppendLine("<Directory \"" + update_button.Tag.ToString() + "\">");
                        }
                        else
                        {
                            new_httpd_conf.AppendLine(line);
                        }
                        
                    }
                    else
                    {
                        new_httpd_conf.AppendLine(line);
                    }
                }
            }
            else
            {
                new_httpd_conf.AppendLine(line);
            }
        }

        private void corrupt_prompt(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(
                "httpd.conf seems to be corrupted! Would you like to reset the file?",
                "httpd.conf seems corrupted!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (File.Exists(AppContext.BaseDirectory + "/httpd.conf"))
                {
                    string default_httpd_conf = File.ReadAllText(AppContext.BaseDirectory + "/httpd.conf");

                    if (File.Exists(base_path.Content.ToString()))
                    {
                        File.WriteAllText(base_path.Content.ToString(), default_httpd_conf);

                        Update_button_Click(sender, e);
                    }
                }
            }
        }

        private static bool add_doc_root_prompt(Button update_button, StringBuilder new_httpd_conf, bool doc_root_found)
        {
            if (MessageBox.Show("Document Root was not found in selected httpd.conf, " +
                "would you like to add it to the file?",
                "Document Root not found!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                new_httpd_conf.AppendLine("DocumentRoot \"" + update_button.Tag.ToString() + "\"");
                doc_root_found = true;
            }

            return doc_root_found;
        }

        private static bool add_directory_prompt(Button update_button, StringBuilder new_httpd_conf, bool directory_found)
        {
            if (MessageBox.Show("Directory tag was not found in selected httpd.conf, " +
                "would you like to add it to the file?",
                "Directory tag not found!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                new_httpd_conf.AppendLine("<Directory \"" + update_button.Tag.ToString() + "\">");
                new_httpd_conf.AppendLine("     Options Indexes FollowSymLinks Includes ExecCGI");
                new_httpd_conf.AppendLine("     AllowOverride All");
                new_httpd_conf.AppendLine("     Require all granted");
                new_httpd_conf.AppendLine("</Directory>");


                directory_found = true;
            }

            return directory_found;
        }

        private void ReloadXampp()
        {
            ExecuteCommand(base_path.Content.ToString() + @"\apache_stop.bat");
            string start_apache_script = base_path.Content.ToString() + @"\apache_start.bat";
            Thread start_apache_thread = new Thread(() => ExecuteCommand(start_apache_script));
            start_apache_thread.Start();
            ExecuteCommand(base_path.Content.ToString() + @"\mysql_stop.bat");
            string start_sql_script = base_path.Content.ToString() + @"\mysql_start.bat";
            Thread start_sql_thread = new Thread(() => ExecuteCommand(start_sql_script));
            start_sql_thread.Start();
        }

        private void Remove_website_Click(object sender, RoutedEventArgs e)
        {
            Button current = ((Button)sender);

            string error = "";
            string stored_website_file = "";

            get_stored_websites(ref stored_website_file, ref error);

            List<website_folder> website_folders = get_deserialsed_websites(stored_website_file);

            for (int i = 0; i < website_folders.Count; i++)
            {
                if(website_folders[i].url == current.Tag.ToString() && website_folders[i].name == current.Content.ToString())
                {
                    website_folders.RemoveAt(i);
                    break;
                }
            }

            error = set_stored_websites(error, website_folders);


            update_lists();
            display_error(error);


        }

        private static string set_stored_websites(string error, List<website_folder> website_folders)
        {
            try
            {
                var json = JsonConvert.SerializeObject(website_folders);
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/data/website_folders.dat", json);
            }
            catch
            {
                if (error != "")
                {
                    error += "and writing";
                }
                else
                {
                    error = "writing";
                }
            }

            return error;
        }

        private List<website_folder> add_stored_websites(string stored_website_file, List<website_folder> website_folders)
        {
            website_folders = get_deserialsed_websites(stored_website_file);
            website_folders.Add(new website_folder(name.Text, url.Text));
            return website_folders;
        }

        private static List<website_folder> get_deserialsed_websites(string stored_website_file)
        {
            List<website_folder> website_folders = new List<website_folder>();
            if (stored_website_file != "")
            {
                website_folders = JsonConvert.DeserializeObject<List<website_folder>>(stored_website_file);
                if (website_folders == null)
                {
                    website_folders = new List<website_folder>();
                }
            }

            return website_folders;
        }

        private static void get_stored_websites(ref string stored_website_file, ref string error)
        {
            try
            {
                if (new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + "/data/website_folders.dat").Length != 0)
                {
                    stored_website_file = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/data/website_folders.dat");
                }

                
            }
            catch
            {
                error = "reading";
            }
        }

        private void Select_base_path_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                base_path.Content = fd.SelectedPath;
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/data/base_path.dat", base_path.Content.ToString());
            }
        }

        private void Reset_base_Path_Click(object sender, RoutedEventArgs e)
        {
            resetPath(base_path, true);
        }
    }
}
