using someapp.CreateAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
using System.IO;

namespace someapp
{
    /// <summary>
    /// Interaction logic for create_account_airport_selection.xaml
    /// </summary>
    public partial class create_account_airport_selection
    {
        account_classes.PilotDetails pilot = new account_classes.PilotDetails();
        create_account_utils __utils = new create_account_utils();
        debug_params.debug_tools debug_Tools = new debug_params.debug_tools();

        public create_account_airport_selection(string Username,string Password,string Name,string Surname,string Type,string Country)
        {
            InitializeComponent();
            __utils.getAirportsFromCountry(Country);

            textbox_SelectedCountry.Text = Country;

            pilot.Username = Username;
            pilot.Name = Name;
            pilot.Surname = Surname;
            pilot.Type = Type;
            pilot.Country = Country;
            pilot.Password = Password;

            foreach (var airport in __utils.AirportsFromCountry)
            {
                combo_selectedICAO.Items.Add(airport.ICAO);
            }

            if(debug_Tools.debugMsg)
                    MessageBox.Show($"Username: {Username} Password: {Password} Name: {Name} Surname: {Surname} Country: {Country} Type: {Type}");


        }

        public void combo_selectedICAO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = combo_selectedICAO.SelectedIndex;

            textbox_SelectedAirportName.Text = __utils.AirportsFromCountry[selectedIndex].Name;
            texbox_Location.Text = $"{__utils.AirportsFromCountry[selectedIndex].LatDec} {__utils.AirportsFromCountry[selectedIndex].LongDec}";
                
        }

        private void button_next_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = combo_selectedICAO.SelectedIndex;

            pilot.ICAO = combo_selectedICAO.Text;

            pilot.LatDec = __utils.AirportsFromCountry[selectedIndex].LatDec;
            pilot.LongDec = __utils.AirportsFromCountry[selectedIndex].LongDec;


            create_account_summary summary = new create_account_summary(pilot.Username, pilot.Password, pilot.Name, pilot.Surname, pilot.Country, pilot.Type, pilot.ICAO,pilot.LatDec,pilot.LongDec);
            summary.Show();

            if (debug_Tools.debugMsg)
            {
                MessageBox.Show(combo_selectedICAO.Text);
                MessageBox.Show($"final  Username: {pilot.Username} Password: {pilot.Password} Name: {pilot.Name} Surname: {pilot.Surname} Country: {pilot.Country} Type: {pilot.Type} ICAO: {combo_selectedICAO.Text}");
            }
               

            this.Close();

        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            create_account create = new create_account(pilot.Username);
            create.Show();
            this.Close();   
        }
    }
}
