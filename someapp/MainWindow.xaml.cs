using System;
using System.IO;
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
using Newtonsoft.Json;
using someapp.CreateAccount;
using System.Net.NetworkInformation;

namespace someapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            if (CheckForInternetConnection())
                InitializeComponent();
            else
                MessageBox.Show("No internet connection. Program requires internet connection");
        }

        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            string username = textbox_username.Text; ;
            string password = passwordbox_password.Password.ToString();


            if (passwordbox_password.Password.ToString() == "" && textbox_username.Text != "")
            {
                create_account create_Account = new create_account(username);
                create_Account.Show();
                this.Close();
            }
            else if (textbox_username.Text != "" && passwordbox_password.Password.ToString() != "")
            {
                string path = $"profiles/{username}";
                

                if (Directory.Exists(path) && File.Exists(path +  "/profile.json"))
                {
                    string profileFilePath = $"profiles/{username}/profile.json";
                    string profileFileDecrytpedPath = $"profiles/{username}/profileDecrypted.json";

                    string encryptedText = File.ReadAllText(profileFilePath);
                    string decryptedText = create_account_utils.DecryptText(encryptedText, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");


                    CreateAccount.account_classes.PilotDetails pilot = JsonConvert.DeserializeObject<CreateAccount.account_classes.PilotDetails>(decryptedText);

                    if (password == pilot.Password)
                    {
                        MainMenu.main_menu main_Menu = new MainMenu.main_menu(pilot.Username, pilot.Username, pilot.Surname);

                        main_Menu.Show();
                        this.Close();
                    }
                    if (password != pilot.Password)
                    {
                        MessageBox.Show("Incorrect Password");
                    }
                        
                }
            }
            else
            {
                MessageBox.Show("Correct your credentials");
            }
        }



        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var result = ping.Send("8.8.8.8"); // Use Google's DNS server IP address
                    return (result.Status == IPStatus.Success);
                }
            }
            catch
            {
                return false;
            }
        }



        //Just copy everything from previous void
        private void passwordbox_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string username = textbox_username.Text; ;
                string password = passwordbox_password.Password.ToString();

                if (textbox_username.Text != "" && passwordbox_password.Password.ToString() != "")
                {
                    string path = $"profiles/{username}";


                    if (Directory.Exists(path) && File.Exists(path + "/profile.json"))
                    {

                        string profileFilePath = $"profiles/{username}/profile.json";
                        string profileFileDecrytpedPath = $"profiles/{username}/profileDecrypted.json";

                        string encryptedText = File.ReadAllText(profileFilePath);
                        string decryptedText = create_account_utils.DecryptText(encryptedText, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");


                        CreateAccount.account_classes.PilotDetails pilot = JsonConvert.DeserializeObject<CreateAccount.account_classes.PilotDetails>(decryptedText);

                        if (password == pilot.Password)
                        {
                            MainMenu.main_menu main_Menu = new MainMenu.main_menu(pilot.Username,pilot.Name,pilot.Surname);
                            main_Menu.Show();

                            this.Close();   
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Correct your credentials");
                }
            }
        }

        private void textbox_username_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string username = textbox_username.Text; ;
                string password = passwordbox_password.Password.ToString();

                if (passwordbox_password.Password.ToString() == "" && textbox_username.Text != "")
                {
                    create_account create_Account = new create_account(username);
                    create_Account.Show();
                    this.Close();
                }
                else if (textbox_username.Text != "" && passwordbox_password.Password.ToString() != "")
                {
                    string path = $"profiles/{username}";


                    if (Directory.Exists(path) && File.Exists(path + "/profile.json"))
                    {

                        string profileFilePath = $"profiles/{username}/profile.json";
                        string profileFileDecrytpedPath = $"profiles/{username}/profileDecrypted.json";

                        string encryptedText = File.ReadAllText(profileFilePath);
                        string decryptedText = create_account_utils.DecryptText(encryptedText, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");


                        CreateAccount.account_classes.PilotDetails pilot = JsonConvert.DeserializeObject<CreateAccount.account_classes.PilotDetails>(decryptedText);

                        if (password == pilot.Password)
                        {

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Correct your credentials");
                }
            }

        }
    }
}
