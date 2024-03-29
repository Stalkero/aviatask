﻿using Newtonsoft.Json;
using Aviatask.Settings;
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

namespace Aviatask.CreateAccount
{
    /// <summary>
    /// Interaction logic for create_account_summary.xaml
    /// </summary>
    public partial class create_account_summary
    {


        account_classes.PilotDetails pilot = new account_classes.PilotDetails();
        List<LogBook.Classes.flightHistory> flights = new List<LogBook.Classes.flightHistory>();

        public create_account_summary(string Username, string Password, string Name, string Surname, string Country, string Type, string ICAO, float Lat, float Lon)
        {
            InitializeComponent();

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

            //Default quick job generation settings
            settings_generation_classes.quick_job_generation job_Generationn = new settings_generation_classes.quick_job_generation();
            job_Generationn.maxDistance = 150;
            job_Generationn.AirportPeopleIterations = 3;
            job_Generationn.CargoJobGenIterations = 3;


            LogBook.Classes.flightHistory fillMeInFlight = new LogBook.Classes.flightHistory();

            fillMeInFlight.jobID = "FILL";
            fillMeInFlight.jobName = "FILL";
            fillMeInFlight.jobDescription = "FILL";
            fillMeInFlight.jobType = "FILL";
            fillMeInFlight.weight = "FILL";
            fillMeInFlight.time = "FILL";
            fillMeInFlight.startICAO = "FILL";
            fillMeInFlight.endICAO = "FILL";
            fillMeInFlight.distance = 0;

            flights.Add(fillMeInFlight);



            



            //Create here some saving mechanism
            string toJsonAccount = JsonConvert.SerializeObject(pilot);
            string toJsonQuickJobGenerationSettings = JsonConvert.SerializeObject(job_Generationn);
            string toJsonLogbook = JsonConvert.SerializeObject(flights);

            MessageBox.Show(toJsonLogbook);

            string path = $"profiles/{pilot.Username}";

            if (Directory.Exists(path))
            {

                string encryptedAccount = create_account_utils.EncryptText(toJsonAccount, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");
                string encryptedQuickJobGenerationSettings = create_account_utils.EncryptText(toJsonQuickJobGenerationSettings, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");
                string encryptedLogbook = create_account_utils.EncryptText(toJsonLogbook, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");

                File.WriteAllText(path + "/profile.json", encryptedAccount); 
                File.WriteAllText(path + "/quickjob_settings.json", encryptedQuickJobGenerationSettings); 
                File.WriteAllText(path + "/logbook.json", encryptedLogbook); 

                MessageBox.Show("Account Created. You can now login");
                //MainWindow mainWindow = new MainWindow();   

                //mainWindow.Show();
                this.Close();

            }
            else
            {
                Directory.CreateDirectory(path);

                string encryptedAccount = create_account_utils.EncryptText(toJsonAccount, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");
                string encryptedQuickJobGenerationSettings = create_account_utils.EncryptText(toJsonQuickJobGenerationSettings, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");
                string encryptedLogbook = create_account_utils.EncryptText(toJsonLogbook, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");

                File.WriteAllText(path + "/profile.json", encryptedAccount);
                File.WriteAllText(path + "/quickjob_settings.json", encryptedQuickJobGenerationSettings);
                File.WriteAllText(path + "/logbook.json", encryptedLogbook);


                MessageBox.Show("Account Created. You can now login");
                //MainWindow mainWindow = new MainWindow();

                //mainWindow.Show();
                this.Close();
            }
        }
    }
}
