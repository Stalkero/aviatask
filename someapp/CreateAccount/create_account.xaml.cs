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

namespace someapp
{
    /// <summary>
    /// Interaction logic for create_account.xaml
    /// </summary>
    public partial class create_account
    {

        public struct pilot_info
        {
            public string username { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string password { set; get; }
            public string aircraft_type { set; get; }
            public string country { set; get; }
        }



        public create_account(string username)
        {
            InitializeComponent();
            
            texbox_pilotUsername.Text = username;
        }

        private void button_ConfirmData_Click(object sender, RoutedEventArgs e)
        {
            if (texbox_pilotUsername.Text != "" && texbox_pilotName.Text != "" && texbox_pilotPassword.Password.ToString() != "" && texbox_pilotSurname.Text != "" && combo_aircraftType.Text != "" && combo_Country.Text != "")
            {
                debug_params.debug_tools debug_Tools = new debug_params.debug_tools();
                pilot_info pilot = new pilot_info();

                pilot.username = texbox_pilotUsername.Text;
                pilot.password = texbox_pilotPassword.Password.ToString();
                pilot.name = texbox_pilotName.Text;
                pilot.surname = texbox_pilotSurname.Text;
                pilot.country = combo_Country.Text;
                pilot.aircraft_type = combo_aircraftType.Text;

                if (debug_Tools.debugMsg)
                    MessageBox.Show($"Username: {pilot.username} Password: {pilot.password} Name: {pilot.name} Surname: {pilot.surname} Country: {pilot.country} Type: {pilot.aircraft_type}");

                create_account_airport_selection airport_Selection = new create_account_airport_selection(pilot.username, pilot.password, pilot.name,pilot.surname,pilot.aircraft_type,pilot.country);

                airport_Selection.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Correct your info");
            }


            


            

        }

        private void texbox_pilotName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (texbox_pilotUsername.Text != "" && texbox_pilotName.Text != "" && texbox_pilotPassword.Password.ToString() != "" && texbox_pilotSurname.Text != "" && combo_aircraftType.Text != "" && combo_Country.Text != "")
                {
                    debug_params.debug_tools debug_Tools = new debug_params.debug_tools();
                    pilot_info pilot = new pilot_info();

                    pilot.username = texbox_pilotUsername.Text;
                    pilot.password = texbox_pilotPassword.Password.ToString();
                    pilot.name = texbox_pilotName.Text;
                    pilot.surname = texbox_pilotSurname.Text;
                    pilot.country = combo_Country.Text;
                    pilot.aircraft_type = combo_aircraftType.Text;

                    if (debug_Tools.debugMsg)
                        MessageBox.Show($"Username: {pilot.username} Password: {pilot.password} Name: {pilot.name} Surname: {pilot.surname} Country: {pilot.country} Type: {pilot.aircraft_type}");

                    create_account_airport_selection airport_Selection = new create_account_airport_selection(pilot.username, pilot.password, pilot.name, pilot.surname, pilot.aircraft_type, pilot.country);

                    airport_Selection.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Correct your info");
                }
            }
        }

    }
}
