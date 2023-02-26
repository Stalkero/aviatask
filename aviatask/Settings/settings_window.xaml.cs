using Aviatask.Settings;
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

namespace Aviatask.Settings_window
{
    /// <summary>
    /// Interaction logic for settings.xaml
    /// </summary>
    public partial class Settings
    {
        public string pUsername { get; set; }
        public string pName { get; set; }
        public string pSurname { get; set; }



        public Settings(string username,string name,string surname)
        {
            InitializeComponent();
            pUsername = username;
            pName = name;
            pSurname = surname;
        }

        private void btn_close_settings_Click(object sender, RoutedEventArgs e)
        {

           // MainMenu.main_menu main_Menu = new MainMenu.main_menu(pUsername, pName,pSurname);
           // main_Menu.Show();
            this.Close();


        }

        private void btn_pilot_data_Click(object sender, RoutedEventArgs e)
        {
            settings_profile _Profile = new settings_profile(pUsername);
            _Profile.Show();

            this.Close();

        }

        private void btn_generation_options_Click(object sender, RoutedEventArgs e)
        {
            settings_generation generationWnd = new settings_generation(pUsername,pName,pSurname);
            generationWnd.Show();

            this.Close();

        }
    }
}
