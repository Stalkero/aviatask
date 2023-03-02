using Aviatask.CreateAccount;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Aviatask.LogBook
{
    /// <summary>
    /// Interaction logic for Logbook_page.xaml
    /// </summary>
    public partial class Logbook
    {
        public static bool LogBookClose { get; set; }

        public static int tablet_size { get; set; }

        public string pUsername { get; set; }
        public string pName { get; set; }
        public string pSurname { get; set; }

        public void ThreadedUIUpdates()
        {
            while (!LogBookClose)
            {

                Application.Current.Dispatcher.Invoke(() =>
                {


                    

                    if (tablet_size == 0)
                    {

                        this.Width = 1920;
                        this.Height = 1016;





                        

                        




                    }
                    if (tablet_size == 1)
                    {

                        this.Width = 1600;
                        this.Height = 848;

                        MessageBox.Show("Logbook only works on 1920x1080 windows");
                        LogBookClose = true;
                        NavigationService.GoBack();
                        MainMenu.main_menu.MainMenuClose = false;
                    }
                });


            }
        }


        public Logbook(string username, string name, string surname)
        {
            InitializeComponent();

            pUsername = username;
            pName = name;
            pSurname = surname;

            string path = $"profiles/{username}";
            string logbookFile = path + "/logbook.json";

            if (Directory.Exists(path) && File.Exists(logbookFile))
            {
                string encryptedLogbookFileText = File.ReadAllText(logbookFile);
                string decryptedLogBookFileText = create_account_utils.DecryptText(encryptedLogbookFileText, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");


                List<LogBook.Classes.flightHistory> flights = JsonConvert.DeserializeObject<List<LogBook.Classes.flightHistory>>(decryptedLogBookFileText);

                if (tablet_size != 0)
                {
                    MessageBox.Show("Logbook only works on 1920x1080 windows");
                    LogBookClose = true;
                    MainMenu.main_menu.MainMenuClose = false;
                    
                }
                else
                {
                    if (flights[0].jobID == "FILL")
                    {
                        grid_MainGrid.Children.Remove(stack_Labels);
                        grid_MainGrid.Children.Remove(scroll_Flights);

                        TextBlock text = new TextBlock()
                        {
                            Text = "No flights yet",
                            FontSize = 40,
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center
                        };

                        grid_MainGrid.Children.Add(text);
                    }
                    else
                    {
                        int stackpanelid = 0;

                        foreach (var flight in flights)
                        {
                            StackPanel flightStackPanel = new StackPanel()
                            {
                                Name = $"stack_flight_{stackpanelid}",
                                Orientation = Orientation.Horizontal,
                                Margin = new Thickness(0, 0, 0, 15)
                            };


                            TextBlock text_JobID = new TextBlock()
                            {
                                Name = "text_JobID",
                                Text = flight.jobID,
                                MinWidth = 200,
                                Width = 200,
                                FontSize = 16,
                                TextAlignment = TextAlignment.Center,
                                Foreground = new SolidColorBrush(Color.FromRgb(173, 173, 173))
                            };

                            TextBlock text_JobName = new TextBlock()
                            {
                                Name = "text_JobName",
                                Text = flight.jobName,
                                Width = 250,
                                MinWidth = 250,
                                FontSize = 16,
                                TextAlignment = TextAlignment.Center,
                                Foreground = new SolidColorBrush(Color.FromRgb(173, 173, 173))
                            };

                            TextBlock text_JobType = new TextBlock()
                            {
                                Name = "text_JobType",
                                Text = flight.jobType,
                                Width = 200,
                                MinWidth = 200,
                                TextAlignment = TextAlignment.Center,
                                FontSize = 16,
                                Foreground = new SolidColorBrush(Color.FromRgb(173, 173, 173))
                            };

                            TextBlock text_JobWeight = new TextBlock()
                            {
                                Name = "text_JobWeight",
                                Text = flight.weight,
                                Width = 200,
                                MinWidth = 200,
                                FontSize = 16,
                                TextAlignment = TextAlignment.Center,
                                Foreground = new SolidColorBrush(Color.FromRgb(173, 173, 173))
                            };

                            TextBlock text_JobTime = new TextBlock()
                            {
                                Name = "text_JobTime",
                                Text = flight.time,
                                Width = 250,
                                MinWidth = 250,
                                FontSize = 16,
                                TextAlignment = TextAlignment.Center,
                                Foreground = new SolidColorBrush(Color.FromRgb(173, 173, 173))
                            };

                            TextBlock text_startEndIcao = new TextBlock()
                            {
                                Name = "text_Dep",
                                Width = 200,
                                MinWidth = 200,
                                TextAlignment = TextAlignment.Center,
                                FontSize = 16,
                                Text = $"{flight.startICAO}/{flight.endICAO}",
                                Foreground = new SolidColorBrush(Color.FromRgb(173, 173, 173))
                            };

                            TextBlock text_distance = new TextBlock()
                            {
                                Name = "text_Distance",
                                Width = 200,
                                MinWidth = 200,
                                Text = flight.distance.ToString(),
                                TextAlignment = TextAlignment.Center,
                                FontSize = 16,
                                Foreground = new SolidColorBrush(Color.FromRgb(173, 173, 173))
                            };

                            Wpf.Ui.Controls.Button viewJobBtn = new Wpf.Ui.Controls.Button()
                            {
                                Content = "View contract",
                                MinWidth = 130,
                                FontSize = 16,
                                Name = $"btn_viewJob_{stackpanelid}",
                            };
                            viewJobBtn.Click += ViewJobBtn_Click;

                            flightStackPanel.Children.Add(text_JobID);
                            flightStackPanel.Children.Add(text_JobName);
                            flightStackPanel.Children.Add(text_JobType);
                            flightStackPanel.Children.Add(text_startEndIcao);
                            flightStackPanel.Children.Add(text_distance);
                            flightStackPanel.Children.Add(text_JobWeight);
                            flightStackPanel.Children.Add(text_JobTime);
                            flightStackPanel.Children.Add(viewJobBtn);

                            stack_Flights.Children.Add(flightStackPanel);
                            stackpanelid++;
                        }
                    }
                }

                // MessageBox.Show("Fine");

                


            }
            else
            {
                MessageBox.Show("Profile Corrupted. Directory or logbook file missing");
            }




        }

        private void ViewJobBtn_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.main_menu.MainMenuClose = false;
            LogBookClose = true;
            NavigationService.GoBack();
        }

        private void UiPage_Loaded(object sender, RoutedEventArgs e)
        {
            Thread UIupdates = new Thread(new ThreadStart(ThreadedUIUpdates));
            UIupdates.Start();
        }
    }
}
