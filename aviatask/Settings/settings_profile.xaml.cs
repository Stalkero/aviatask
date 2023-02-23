using Newtonsoft.Json;
using Aviatask.CreateAccount;
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

namespace Aviatask.Settings
{
    /// <summary>
    /// Interaction logic for settings_profile.xaml
    /// </summary>
    public partial class settings_profile
    {

        public string pICAO { get; set; }
        public float platDec { get; set; }
        public float plongDec { get; set; }


        public settings_profile(string username)
        {
            InitializeComponent();

            string path = $"profiles/{username}";

            if (Directory.Exists(path) && File.Exists(path + "/profile.json"))
            {
                string profileFilePath = $"profiles/{username}/profile.json";

                string encryptedText = File.ReadAllText(profileFilePath);
                string decryptedText = create_account_utils.DecryptText(encryptedText, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");


                account_classes.PilotDetails pilot = JsonConvert.DeserializeObject<account_classes.PilotDetails>(decryptedText);

                textbox_Username.Text = pilot.Username;
                textbox_Name.Text = pilot.Name;
                textbox_Surname.Text = pilot.Surname;
                combo_Country.Text = pilot.Country;
                passwordbox_password.Text = pilot.Password;
                combo_Type.Text = pilot.Type;
                pICAO = pilot.ICAO;
                platDec = pilot.LatDec;
                plongDec = pilot.LongDec;
            }

        }

        private void button_Save_Click(object sender, RoutedEventArgs e)
        {

            account_classes.PilotDetails pilotSave = new account_classes.PilotDetails();

            pilotSave.Username = textbox_Username.Text;
            pilotSave.Name = textbox_Name.Text;
            pilotSave.Surname = textbox_Surname.Text;
            pilotSave.Password = passwordbox_password.Text;
            pilotSave.Country = combo_Country.Text;
            pilotSave.Type= combo_Type.Text;
            pilotSave.ICAO= pICAO;
            pilotSave.LatDec = platDec;
            pilotSave.LongDec = plongDec;

            string toJson = JsonConvert.SerializeObject(pilotSave);
            string path = $"profiles/{pilotSave.Username}";

            if (Directory.Exists(path))
            {

                string encryptedMsg = create_account_utils.EncryptText(toJson, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");


                File.WriteAllText(path + "/profile.json", encryptedMsg);




                MessageBox.Show("Changes saved");
                

                MainMenu.main_menu main_Menu = new MainMenu.main_menu(pilotSave.Username,pilotSave.Name,pilotSave.Surname);
                main_Menu.Show();

                this.Close();
            }
            else
            {
                Directory.CreateDirectory(path);

                string encryptedMsg = create_account_utils.EncryptText(toJson, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");

                File.WriteAllText(path + "/profile.json", encryptedMsg);

                MessageBox.Show("Changes saved");

                MainMenu.main_menu main_Menu = new MainMenu.main_menu(pilotSave.Username, pilotSave.Name, pilotSave.Surname);
                main_Menu.Show();

                this.Close();


            }
        }
    }
}
