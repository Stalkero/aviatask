using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Xml.Linq;
using Windows.Devices.Bluetooth.Background;

namespace Aviatask.Settings
{
    /// <summary>
    /// Interaction logic for main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public string pUsername { get; set; }
        public string pName { get; set; }
        public string pSurname { get; set; }
        public static int tablet_size { get; set; }
        public static bool SettingsClose { get; set; }
        public void ThreadedUIUpdates()
        {
            while (!SettingsClose)
            {

                Application.Current.Dispatcher.Invoke(() =>
                {

                    if (tablet_size == 0)
                    {
                        txt_SettingsLabel.FontSize = 48;

                        btn_pilot_data.Width = 300;
                        btn_pilot_data.Height = 300;
                        btn_pilot_data.Margin = new Thickness(10, 0, 10, 0);

                        btn_generation_options.Width = 300;
                        btn_generation_options.Height = 300;
                        btn_generation_options.Margin = new Thickness(10, 0, 10, 0);

                        btn_other.Width = 300;
                        btn_other.Height = 300;
                        btn_other.Margin = new Thickness(10, 0, 10, 0);


                        btn_close_settings.Width = 300;
                        btn_close_settings.Height = 96;


                        this.Width = 1920;
                        this.Height = 1016;
                    }
                    if (tablet_size == 1)
                    {
                        txt_SettingsLabel.FontSize = 40;

                        btn_pilot_data.Width = 250;
                        btn_pilot_data.Height = 250;
                        btn_pilot_data.Margin = new Thickness(8, 0, 8, 0);

                        btn_generation_options.Width = 250;
                        btn_generation_options.Height = 250;
                        btn_generation_options.Margin = new Thickness(8, 0, 8, 0);

                        btn_other.Width = 250;
                        btn_other.Height = 250;
                        btn_other.Margin = new Thickness(8, 0, 8, 0);

                        btn_close_settings.Width = 250;
                        btn_close_settings.Height = 80;
                        btn_close_settings.Margin = new Thickness(0,0,0,100);

                        stack_settings.Margin = new Thickness(0, 0, 0, 100);

                        this.Width = 1600;
                        this.Height = 848;
                    }
                });


            }
        }

        public Main(string username, string name, string surname)
        {
            InitializeComponent();
            pUsername = username;
            pName = name;
            pSurname = surname;
        }

        private void btn_close_settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsClose = true;
            NavigationService.GoBack();
            MainMenu.main_menu.MainMenuClose = false;
        }

        private void btn_pilot_data_Click(object sender, RoutedEventArgs e)
        {
            SettingsClose = true;
            Settings.Profile.SettingsClose = false;
            NavigationService.Content = new Settings.Profile(pUsername);
        }

        private void btn_generation_options_Click(object sender, RoutedEventArgs e)
        {
            SettingsClose = true;
            Settings.Generation.SettingsClose = false;
            NavigationService.Content = new Settings.Generation(pUsername, pName, pSurname);


        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Thread UIupdates = new Thread(new ThreadStart(ThreadedUIUpdates));
            UIupdates.Start();
        }
    }
}
