using Aviatask.CreateAccount;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
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
using System.Windows.Media.Animation;
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

        bool doLoadLogbook = false;
        bool updateInProgress = false;

        List<LogBook.Classes.flightHistory> flights { get; set; }

        public void ThreadedPageSizeUpdates()
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

                    }
                });


            }
        }

        public void ReloadUIelements()
        {
            if (doLoadLogbook)
            {
                if (tablet_size == 0)
                {
                    grid_MainGrid.Margin = new Thickness(0, 0, 0, 0);
                    stack_Labels.Margin = new Thickness(130, 0, 130,10);
                    stack_Labels.VerticalAlignment = VerticalAlignment.Top;

                    stack_Flights.Margin = new Thickness(130, 40, 130, 87);
                    stack_Flights.VerticalAlignment = VerticalAlignment.Top;
                    stack_Flights.Height = 600;

                    scroll_Flights.Margin = new Thickness(0, 0, 0, 10);
                    scroll_Flights.Height = 900;

                    stack_Main.Margin = new Thickness(0, 0, 0, 0);

                    btn_Close.Margin = new Thickness(0, 0, 0, 0);
                    btn_Close.VerticalAlignment = VerticalAlignment.Bottom;

                    txt_JobID.FontSize = 19;
                    txt_Job.FontSize = 19;
                    txt_dep.FontSize = 19;
                    txt_distance.FontSize = 19;
                    txt_weight.FontSize = 19;
                    txt_type.FontSize = 19;
                    btn_viewRep.FontSize = 19;

                    txt_date.FontSize = 16;

                    txt_Job.Width = 250;
                    txt_date.Width = 250;
                    txt_JobID.Width = 200;
                    txt_type.Width = 200;
                    txt_dep.Width = 200;
                    txt_distance.Width = 200;
                    txt_weight.Width = 200;


                    Thread showLogBook = new Thread(new ThreadStart(ThreadedLoadLogbook));

                    if (!updateInProgress)
                        showLogBook.Start();
                    else
                        stack_Flights.Children.Clear();



                }
                if (tablet_size == 1)
                {

                    grid_MainGrid.Margin = new Thickness(0, 0, 0, 0);

                    stack_Labels.Margin = new Thickness(108, 0, 108, 10);
                    stack_Labels.VerticalAlignment = VerticalAlignment.Top;

                    stack_Flights.Margin = new Thickness(108, 0, 108,0);
                    stack_Flights.VerticalAlignment = VerticalAlignment.Top;
                    stack_Flights.Height = 600;

                    stack_Main.Margin = new Thickness(0, -85, 0, 0);

                    scroll_Flights.Margin = new Thickness(0, 0, 0, 10);
                    scroll_Flights.Height = 700;

                    btn_Close.Margin = new Thickness(0, 0, 0, 0);
                    btn_Close.VerticalAlignment = VerticalAlignment.Bottom;

                    txt_JobID.FontSize = 16;
                    txt_Job.FontSize = 16;
                    txt_dep.FontSize = 16;
                    txt_distance.FontSize = 16;
                    txt_weight.FontSize = 16;
                    txt_type.FontSize = 16;
                    btn_viewRep.FontSize = 16;

                    txt_date.FontSize = 13;

                    txt_Job.Width = 240;
                    txt_date.Width = 240;
                    txt_JobID.Width = 190;
                    txt_type.Width = 140;
                    txt_dep.Width = 120;
                    txt_distance.Width = 170;
                    txt_weight.Width = 130;


                    Thread showLogBook = new Thread(new ThreadStart(ThreadedLoadLogbook));


                    if (!updateInProgress)
                        showLogBook.Start();
                    else
                        stack_Flights.Children.Clear();
                }
            }



        }

        public void  ThreadedLoadLogbook()
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                int stackpanelid = 0;
                Storyboard sb = (Storyboard)FindResource("FadeInAnimation");
                stack_Flights.Children.Clear();

                updateInProgress = true;

                if (tablet_size == 0)
                {
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
                        sb.Begin(flightStackPanel);

                        await Task.Delay(100);

                        stackpanelid++;
                    }
                }
                if (tablet_size == 1)
                {
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
                            MinWidth = 190,
                            Width = 190,
                            FontSize = 13,
                            TextAlignment = TextAlignment.Center,
                            Foreground = new SolidColorBrush(Color.FromRgb(173, 173, 173))
                        };

                        TextBlock text_JobName = new TextBlock()
                        {
                            Name = "text_JobName",
                            Text = flight.jobName,
                            Width = 240,
                            MinWidth = 240,
                            FontSize = 13,
                            TextAlignment = TextAlignment.Center,
                            Foreground = new SolidColorBrush(Color.FromRgb(173, 173, 173))
                        };

                        TextBlock text_JobType = new TextBlock()
                        {
                            Name = "text_JobType",
                            Text = flight.jobType,
                            Width = 140,
                            MinWidth = 140,
                            TextAlignment = TextAlignment.Center,
                            FontSize = 13,
                            Foreground = new SolidColorBrush(Color.FromRgb(173, 173, 173))
                        };

                        TextBlock text_JobWeight = new TextBlock()
                        {
                            Name = "text_JobWeight",
                            Text = flight.weight,
                            Width = 130,
                            MinWidth = 130,
                            FontSize = 13,
                            TextAlignment = TextAlignment.Center,
                            Foreground = new SolidColorBrush(Color.FromRgb(173, 173, 173))
                        };

                        TextBlock text_JobTime = new TextBlock()
                        {
                            Name = "text_JobTime",
                            Text = flight.time,
                            Width = 240,
                            MinWidth = 240,
                            FontSize = 13,
                            TextAlignment = TextAlignment.Center,
                            Foreground = new SolidColorBrush(Color.FromRgb(173, 173, 173))
                        };

                        TextBlock text_startEndIcao = new TextBlock()
                        {
                            Name = "text_Dep",
                            Width = 120,
                            MinWidth = 120,
                            TextAlignment = TextAlignment.Center,
                            FontSize = 13,
                            Text = $"{flight.startICAO}/{flight.endICAO}",
                            Foreground = new SolidColorBrush(Color.FromRgb(173, 173, 173))
                        };

                        TextBlock text_distance = new TextBlock()
                        {
                            Name = "text_Distance",
                            Width = 170,
                            MinWidth = 170,
                            Text = flight.distance.ToString(),
                            TextAlignment = TextAlignment.Center,
                            FontSize = 13,
                            Foreground = new SolidColorBrush(Color.FromRgb(173, 173, 173))
                        };

                        Wpf.Ui.Controls.Button viewJobBtn = new Wpf.Ui.Controls.Button()
                        {
                            Content = "View contract",
                            MinWidth = 130,
                            FontSize = 13,
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
                        sb.Begin(flightStackPanel);

                        await Task.Delay(100);

                        stackpanelid++;
                    }
                }

                updateInProgress = false;
               
            });


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
                flights = JsonConvert.DeserializeObject<List<LogBook.Classes.flightHistory>>(decryptedLogBookFileText);

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
                    doLoadLogbook = true;
                }
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
            Thread PageSizeUptades = new Thread(new ThreadStart(ThreadedPageSizeUpdates));
            PageSizeUptades.Start();
        }

        private void UiPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReloadUIelements();
        }
    }
}
