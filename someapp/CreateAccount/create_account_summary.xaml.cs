using Newtonsoft.Json;
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
using System.Windows.Shapes;

namespace someapp.CreateAccount
{
    /// <summary>
    /// Interaction logic for create_account_summary.xaml
    /// </summary>
    public partial class create_account_summary
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

            public float LatDec { get; set; }
            public float LongDec { get; set; }
        }

        PilotDetails pilot = new PilotDetails();
        debug_params.debug_tools debug_Tools = new debug_params.debug_tools();

        public create_account_summary(string Username, string Password, string Name, string Surname, string Country, string Type, string ICAO, float Lat, float Lon)
        {
            InitializeComponent();

            

            if (debug_Tools.debugMsg)
                MessageBox.Show($"Summary   Username: {Username} Password: {Password} Name: {Name} Surname: {Surname} Country: {Country} Type: {Type} ICAO: {ICAO}");


            pilot.Name = Name;
            pilot.Surname = Surname;
            pilot.Username = Username;
            pilot.Password = Password;
            pilot.Country = Country;
            pilot.Type = Type;
            pilot.ICAO = ICAO;
            pilot.LatDec = Lat;
            pilot.LongDec = Lon;

            textox_Username.Text = Username;
            textbox_Name.Text = Name;
            textbox_Surname.Text = Surname;
            textbox_Country.Text = Country;
            textbox_Type.Text = Type;
            textbox_ICAO.Text = ICAO;
            texbox_Location.Text = $"{pilot.LatDec} {pilot.LongDec}";





        }

        private void button_RegisterPilot_Click(object sender, RoutedEventArgs e)
        {
            


            //Create here some saving mechanism
            string toJson = JsonConvert.SerializeObject(pilot);
            string path = $"profiles/{pilot.Username}";




            if (Directory.Exists(path))
            {

                string encryptedMsg = create_account_utils.EncryptText(toJson, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");


                File.WriteAllText(path + "/profile.json", encryptedMsg); 

                if (debug_Tools.debugMsg)
                    MessageBox.Show(path);


                MessageBox.Show("Account Created. You can now login");
                MainWindow mainWindow = new MainWindow();   

                mainWindow.Show();
                this.Close();





            }
            else
            {
                Directory.CreateDirectory(path);

                string encryptedMsg = create_account_utils.EncryptText(toJson, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");

                File.WriteAllText(path + "/profile.json", encryptedMsg);

                if (debug_Tools.debugMsg)
                    MessageBox.Show(path);

                MessageBox.Show("Account Created. You can now login");
                MainWindow mainWindow = new MainWindow();

                mainWindow.Show();
                this.Close();
            }
        }
    }
}
