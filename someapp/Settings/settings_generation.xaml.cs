using Newtonsoft.Json;
using someapp.CreateAccount;
using someapp.MainMenu;
using someapp.Settings_window;
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

namespace someapp.Settings
{
    /// <summary>
    /// Interaction logic for settings_generation.xaml
    /// </summary>
    public partial class settings_generation
    {

        settings_generation_classes.quick_job_generation newJobSettings = new settings_generation_classes.quick_job_generation();
        string pUsername { get; set; }
        string pName { get; set; }
        string pSurname { get; set; }

        public settings_generation(string username,string name,string surname)
        {
            InitializeComponent();


            string path = $"profiles/{username}";
            pUsername= username ;
            pName = name ;
            pSurname= surname ;


            if (Directory.Exists(path) && File.Exists(path + "/quickjob_settings.json"))
            {
                string profileFilePath = $"profiles/{username}/quickjob_settings.json";

                string encryptedText = File.ReadAllText(profileFilePath);
                string decryptedText = create_account_utils.DecryptText(encryptedText, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");


                settings_generation_classes.quick_job_generation ReadGenerationSettings = JsonConvert.DeserializeObject<settings_generation_classes.quick_job_generation>(decryptedText);


                numberBox_distance.Text = ReadGenerationSettings.maxDistance.ToString();
                numberBox_AiportPeopleIterations.Text = ReadGenerationSettings.AirportPeopleIterations.ToString();
                numberBox_cargojob_iterations.Text = ReadGenerationSettings.CargoJobGenIterations.ToString();
                numberBox_cargojobhelicopterLoad.Text = ReadGenerationSettings.CargoJobHelicopterLoadCount.ToString();
                numberBox_cargojobPlaneLoad.Text = ReadGenerationSettings.CargoJobPlaneLoadCount.ToString();

            }
        }

        private void btn_saveGenOptions_Click(object sender, RoutedEventArgs e)
        {
            settings_generation_classes.quick_job_generation genOptions = new settings_generation_classes.quick_job_generation();

            genOptions.maxDistance = double.Parse(numberBox_distance.Text);
            genOptions.AirportPeopleIterations = int.Parse(numberBox_AiportPeopleIterations.Text);
            genOptions.CargoJobGenIterations = int.Parse(numberBox_cargojob_iterations.Text);
            genOptions.CargoJobHelicopterLoadCount = int.Parse(numberBox_cargojobhelicopterLoad.Text);
            genOptions.CargoJobPlaneLoadCount = int.Parse(numberBox_cargojobPlaneLoad.Text);

            string genOptionsToJson = JsonConvert.SerializeObject(genOptions);
            string encryptedGenOptions = create_account_utils.EncryptText(genOptionsToJson, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");
            string profileFilePath = $"profiles/{pUsername}/quickjob_settings.json";

            File.WriteAllText(profileFilePath,encryptedGenOptions);

            main_menu menu = new main_menu(pUsername,pName,pSurname);
            menu.Show();
            this.Close();

        }
    }
}
