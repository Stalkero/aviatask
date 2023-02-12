using someapp.CreateAccount;
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
                account_classes.PilotDetails pilot = new account_classes.PilotDetails();

                pilot.Username = texbox_pilotUsername.Text;
                pilot.Password = texbox_pilotPassword.Password.ToString();
                pilot.Name = texbox_pilotName.Text;
                pilot.Surname = texbox_pilotSurname.Text;
                pilot.Country = combo_Country.Text;
                pilot.Type = combo_aircraftType.Text;

                if (debug_Tools.debugMsg)
                    MessageBox.Show($"Username: {pilot.Username} Password: {pilot.Password} Name: {pilot.Name} Surname: {pilot.Surname} Country: {pilot.Country} Type: {pilot.Type}");

                create_account_airport_selection airport_Selection = new create_account_airport_selection(pilot.Username, pilot.Password, pilot.Name,pilot.Surname,pilot.Type,pilot.Country);

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
                    account_classes.PilotDetails pilot = new account_classes.PilotDetails();

                    pilot.Username = texbox_pilotUsername.Text;
                    pilot.Password = texbox_pilotPassword.Password.ToString();
                    pilot.Name = texbox_pilotName.Text;
                    pilot.Surname = texbox_pilotSurname.Text;
                    pilot.Country = combo_Country.Text;
                    pilot.Type = combo_aircraftType.Text;

                    if (debug_Tools.debugMsg)
                        MessageBox.Show($"Username: {pilot.Username} Password: {pilot.Password} Name: {pilot.Name} Surname: {pilot.Surname} Country: {pilot.Country} Type: {pilot.Type}");

                    create_account_airport_selection airport_Selection = new create_account_airport_selection(pilot.Username, pilot.Password, pilot.Name, pilot.Surname, pilot.Type, pilot.Country);

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
