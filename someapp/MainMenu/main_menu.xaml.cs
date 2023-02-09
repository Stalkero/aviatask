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

namespace someapp.MainMenu
{
    /// <summary>
    /// Interaction logic for main_menu.xaml
    /// </summary>
    public partial class main_menu 
    {
        public struct PilotDetails
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Country { get; set; }
            public string Type { get; set; }
            public string ICAO { get; set; }
        }


        public struct CompanyDetails
        {
            public string Name { get; set; }
            public int Balance { get; set; }
            public string CountryOfResidence { get; set; }


        }

        PilotDetails pilot = new PilotDetails();

        public main_menu(string username,string name, string surname)
        {
            InitializeComponent();

            pilot.Username= username;
            pilot.Name= name;
            pilot.Surname= surname;
            


            textbox_welcome.Text = $"Welcome, {name} {surname}";
        }

        private void button_my_settings_Click(object sender, RoutedEventArgs e)
        {
            
            Settings_window.settings settings= new Settings_window.settings(pilot.Username,pilot.Name,pilot.Surname);
            settings.Show();

            this.Close();

        }

        private void button_quick_job_Click(object sender, RoutedEventArgs e)
        {
            QuickJob.quick_job_window quick_Job = new QuickJob.quick_job_window(pilot.Username);
            quick_Job.Show();
        }
    }
}
