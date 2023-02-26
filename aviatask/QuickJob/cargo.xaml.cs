using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Aviatask.CreateAccount;
using System.IO;
using System.Windows.Media.Animation;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Threading;

namespace Aviatask.QuickJob
{
    /// <summary>
    /// Interaction logic for quick_job_summary.xaml
    /// </summary>
    public partial class Cargo
    {
        
        public int status { get; set; }

        public string Pilotusername { get; set; }
        public string Pilotname { get; set; }
        public string Pilotsurname { get; set; }

        public Cargo(string username, string name, string surname, string startICAO,string endICAO,string jobID,string jobType,double distance,string weight,string desc)
        {
            InitializeComponent();

            status = 0;
            Pilotusername = username;
            Pilotsurname = surname;
            textbox_jobID.Text = jobID;
            textbox_jobName.Text = jobType;
            textbox_weight.Text = weight;
            textBox_jobDesc.Text = desc;
            textbox_StartIcao.Text = startICAO;
            textbox_endIcao.Text = endICAO;
            textbox_distanceNM.Text = $"{distance} nm";
            textbox_distanceKM.Text = $"{Math.Round(distance * 1.852, 2)} km";
            browserLeft.Address = $"https://fshub.io/airport/{startICAO}/overview";
            browserRight.Address = $"https://fshub.io/airport/{endICAO}/overview";

            textbox_JobStatus.Text = "Verify cargo loading";

            Wpf.Ui.Controls.Button btn_chk_1 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "1. Verify cargo loading",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_1.Click += CT_Btn_chk_1_Click;

            Wpf.Ui.Controls.Button btn_chk_2 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "2. Confirm cargo weight and balance",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_2.Click += CT_Btn_chk_2_Click;

            Wpf.Ui.Controls.Button btn_chk_3 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "3. Check weather conditions",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_3.Click += CT_Btn_chk_3_Click;

            Wpf.Ui.Controls.Button btn_chk_4 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "4. Get into aircraft",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_4.Click += CT_Btn_chk_4_Click;

            Wpf.Ui.Controls.Button btn_chk_5 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "5.Pre-flight checklist",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_5.Click += CT_Btn_chk_5_Click;

            Wpf.Ui.Controls.Button btn_chk_6 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "6. Check flight plan",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_6.Click += CT_Btn_chk_6_Click;

            Wpf.Ui.Controls.Button btn_chk_7 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "7. Check weather report",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_7.Click += CT_Btn_chk_7_Click;

            Wpf.Ui.Controls.Button btn_chk_8 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "8. Engine start and getting ready to fly",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_8.Click += CT_Btn_chk_8_Click;

            Wpf.Ui.Controls.Button btn_chk_9 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "9. Ready to fly",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_9.Click += CT_Btn_chk_9_Click;

            Wpf.Ui.Controls.Button btn_chk_10 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "10. Flying",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_10.Click += CT_Btn_chk_10_Click;

            Wpf.Ui.Controls.Button btn_chk_11 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "11. Getting ready to land",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_11.Click += CT_Btn_chk_11_Click;

            Wpf.Ui.Controls.Button btn_chk_12 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "12. Finishing job",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_12.Click += CT_Btn_chk_12_Click;

            ChecklistPanel.Children.Add(btn_chk_1);
            ChecklistPanel.Children.Add(btn_chk_2);
            ChecklistPanel.Children.Add(btn_chk_3);
            ChecklistPanel.Children.Add(btn_chk_4);
            ChecklistPanel.Children.Add(btn_chk_5);
            ChecklistPanel.Children.Add(btn_chk_6);
            ChecklistPanel.Children.Add(btn_chk_7);
            ChecklistPanel.Children.Add(btn_chk_8);
            ChecklistPanel.Children.Add(btn_chk_9);
            ChecklistPanel.Children.Add(btn_chk_10);
            ChecklistPanel.Children.Add(btn_chk_11);
            ChecklistPanel.Children.Add(btn_chk_12);
        }

        public void ThreadedChecklist_1()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ToastContentBuilder toastContent1 = new ToastContentBuilder();

                toastContent1.SetToastDuration(0);
                toastContent1.AddHeader("cargo_Checklist_1", "Getting into aircraft", "ct_chk_1");
                toastContent1.AddText("Please wait while you're confirming cargo weight and balance");
                toastContent1.Show();
                textbox_JobStatus.Text = "Confirming cargo weight and balance";
            });

            Thread.Sleep(8000);

            Application.Current.Dispatcher.Invoke(() =>
            {
                textbox_JobStatus.Text = "Cargo weight and balance confirmed";
            });
        }
        public void ThreadedChecklist_2()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ToastContentBuilder toastContent1 = new ToastContentBuilder();

                toastContent1.SetToastDuration(0);
                toastContent1.AddHeader("cargo_Checklist_2", "Checking weahter conditions", "ct_chk_2");
                toastContent1.AddText("Please wait while you'checking the weather conditions");
                toastContent1.Show();
                textbox_JobStatus.Text = "Checking weather conditions";
            });

            Thread.Sleep(8000);

            Application.Current.Dispatcher.Invoke(() =>
            {
                textbox_JobStatus.Text = "Weather conditions checked";
            });
        }
        public void ThreadedChecklist_3()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ToastContentBuilder toastContent1 = new ToastContentBuilder();

                toastContent1.SetToastDuration(0);
                toastContent1.AddHeader("ct_Checklist_3", "Getting into aircraft", "pt_chk_3");
                toastContent1.AddText("Please wait while you're getting into aircraft");
                toastContent1.Show();
                textbox_JobStatus.Text = "Getting into aircraft";
            });

            Thread.Sleep(8000);

            Application.Current.Dispatcher.Invoke(() =>
            {
                textbox_JobStatus.Text = "Got into aircraft";
            });
        }
        public void ThreadedChecklist_4()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ToastContentBuilder toastContent1 = new ToastContentBuilder();

                toastContent1.SetToastDuration(0);
                toastContent1.AddHeader("cargo_Checklist_4", "Pre-flight checklist", "ct_chk_4");
                toastContent1.AddText("Please wait while you're proceeding with pre-flight checklist");
                toastContent1.Show();
                textbox_JobStatus.Text = "Pre-flight checklist";
            });

            Thread.Sleep(8000);

            Application.Current.Dispatcher.Invoke(() =>
            {
                textbox_JobStatus.Text = "Pre-flight checklist done";
            });
        }
        public void ThreadedChecklist_5()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ToastContentBuilder toastContent1 = new ToastContentBuilder();

                toastContent1.SetToastDuration(0);
                toastContent1.AddHeader("cargo_Checklist_5", "Pre-flight checklist", "ct_chk_5");
                toastContent1.AddText("Please wait while you're proceeding with your flight plan");
                toastContent1.Show();
                textbox_JobStatus.Text = "Flight plan check";
            });

            Thread.Sleep(8000);

            Application.Current.Dispatcher.Invoke(() =>
            {
                textbox_JobStatus.Text = "Flight plan checked";
            });
        }
        public void ThreadedChecklist_6()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ToastContentBuilder toastContent1 = new ToastContentBuilder();

                toastContent1.SetToastDuration(0);
                toastContent1.AddHeader("cargo_Checklist_6", "Checking weather report", "ct_chk_6");
                toastContent1.AddText("Please wait while you're checking weather report");
                toastContent1.Show();
                textbox_JobStatus.Text = "Weather report check";
            });

            Thread.Sleep(8000);

            Application.Current.Dispatcher.Invoke(() =>
            {
                textbox_JobStatus.Text = "Weather report checked";
            });
        }
        public void ThreadedChecklist_7()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ToastContentBuilder toastContent1 = new ToastContentBuilder();

                toastContent1.SetToastDuration(0);
                toastContent1.AddHeader("cargo_Checklist_7", "Starting engine and getting ready to fly", "ct_chk_7");
                toastContent1.AddText("Please wait while you're starting engine and getting ready to fly");
                toastContent1.Show();
                textbox_JobStatus.Text = "Engine start and gettting ready to fly";
            });

            Thread.Sleep(8000);

            Application.Current.Dispatcher.Invoke(() =>
            {
                textbox_JobStatus.Text = "Engine start and gettting ready to fly done";
            });
        }
        public void ThreadedChecklist_8()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ToastContentBuilder toastContent1 = new ToastContentBuilder();

                toastContent1.SetToastDuration(0);
                toastContent1.AddHeader("cargo_Checklist_8", "Ready to fly", "ct_chk_8");
                toastContent1.AddText("Wait 8 secs and you are ready to fly");
                toastContent1.Show();
                textbox_JobStatus.Text = "Ready to fly";
            });

            Thread.Sleep(8000);

            Application.Current.Dispatcher.Invoke(() =>
            {
                textbox_JobStatus.Text = "Ready to fly";
            });
        }
        public void ThreadedChecklist_9()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ToastContentBuilder toastContent1 = new ToastContentBuilder();

                toastContent1.SetToastDuration(0);
                toastContent1.AddHeader("cargo_Checklist_9", "Flying", "ct_chk_9");
                toastContent1.AddText("You are airbone now");
                toastContent1.Show();
                textbox_JobStatus.Text = "Flying";
            });

            Thread.Sleep(8000);

            Application.Current.Dispatcher.Invoke(() =>
            {
                textbox_JobStatus.Text = "Flying have a safe flight";
            });
        }
        public void ThreadedChecklist_10()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ToastContentBuilder toastContent1 = new ToastContentBuilder();

                toastContent1.SetToastDuration(0);
                toastContent1.AddHeader("cargo_Checklist_10", "Getting ready to land", "ct_chk_10");
                toastContent1.AddText("Prepare yourself for landing");
                toastContent1.Show();
                textbox_JobStatus.Text = "Getting ready to land";
            });

            Thread.Sleep(8000);

            Application.Current.Dispatcher.Invoke(() =>
            {
                textbox_JobStatus.Text = "Getting ready to land";
            });
        }

        public void ThreadedChecklist_11()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ToastContentBuilder toastContent1 = new ToastContentBuilder();

                toastContent1.SetToastDuration(0);
                toastContent1.AddHeader("cargo_Checklist_11", "Finishing job", "ct_chk_11");
                toastContent1.AddText("Landed, now finish your job");
                toastContent1.Show();
                textbox_JobStatus.Text = "Finishing job";
            });

            Thread.Sleep(8000);

            Application.Current.Dispatcher.Invoke(() =>
            {
                textbox_JobStatus.Text = "Getting ready to land";
            });
        }
        public void ThreadedChecklist_12()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ToastContentBuilder toastContent1 = new ToastContentBuilder();

                toastContent1.SetToastDuration(0);
                toastContent1.AddHeader("cargo_Checklist_12", "Job finished", "ct_chk_12");
                toastContent1.AddText("Job finished, adding flight to your logbook");
                toastContent1.Show();
                textbox_JobStatus.Text = "Job finished";
            });

        }

        private void CT_Btn_chk_1_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            clickedButton.Content += "✓";
            clickedButton.IsEnabled = false;
            Progress_ReadyToFly.Value += 10;

            Thread thread = new Thread(new ThreadStart(ThreadedChecklist_1));
            thread.Start();

            status = 1;

            Storyboard sb = (Storyboard)FindResource("FlashingStoryboard");
            sb.Begin(textbox_JobStatus);

        }
        private void CT_Btn_chk_2_Click(object sender, RoutedEventArgs e)
        {
            if (status == 1)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += "✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;

                Thread thread = new Thread(new ThreadStart(ThreadedChecklist_2));
                thread.Start();

                status = 2;
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void CT_Btn_chk_3_Click(object sender, RoutedEventArgs e)
        {
            if (status == 2)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += "✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;

                Thread thread = new Thread(new ThreadStart(ThreadedChecklist_3));
                thread.Start();

                status = 3;
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void CT_Btn_chk_4_Click(object sender, RoutedEventArgs e)
        {
            if (status == 3)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += "✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;

                Thread thread = new Thread(new ThreadStart(ThreadedChecklist_4));
                thread.Start();

                status = 4;
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void CT_Btn_chk_5_Click(object sender, RoutedEventArgs e)
        {
            if (status == 4)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += "✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;

                Thread thread = new Thread(new ThreadStart(ThreadedChecklist_5));
                thread.Start();

                status = 5;
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void CT_Btn_chk_6_Click(object sender, RoutedEventArgs e)
        {
            if (status == 5)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += "✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;

                Thread thread = new Thread(new ThreadStart(ThreadedChecklist_6));
                thread.Start();

                status = 6;
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void CT_Btn_chk_7_Click(object sender, RoutedEventArgs e)
        {
            if (status == 6)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += "✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;

                Thread thread = new Thread(new ThreadStart(ThreadedChecklist_7));
                thread.Start();

                status = 7;
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void CT_Btn_chk_8_Click(object sender, RoutedEventArgs e)
        {
            if (status == 7)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += "✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;

                Thread thread = new Thread(new ThreadStart(ThreadedChecklist_8));
                thread.Start();


                status = 8;
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void CT_Btn_chk_9_Click(object sender, RoutedEventArgs e)
        {
            if (status == 8)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += "✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;

                Thread thread = new Thread(new ThreadStart(ThreadedChecklist_9));
                thread.Start();

                status = 9;
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void CT_Btn_chk_10_Click(object sender, RoutedEventArgs e)
        {
            if (status == 9)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += "✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;

                Thread thread = new Thread(new ThreadStart(ThreadedChecklist_10));
                thread.Start();

                status = 10;
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void CT_Btn_chk_11_Click(object sender, RoutedEventArgs e)
        {
            if (status == 10)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += "✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;

                Thread thread = new Thread(new ThreadStart(ThreadedChecklist_11));
                thread.Start();

                status = 11;
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void CT_Btn_chk_12_Click(object sender, RoutedEventArgs e)
        {
            if (status == 11)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += "✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;

                Thread thread = new Thread(new ThreadStart(ThreadedChecklist_12));
                thread.Start();

                status = 12;

                Storyboard sb = (Storyboard)FindResource("FlashingStoryboard");
                sb.Stop(textbox_JobStatus);
                jobFinished();
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }

        private void jobFinished()
        {
            string path = $"profiles/{Pilotusername}";
            string logbookFile = path + "/logbook.json";

            if (Directory.Exists(path) && File.Exists(logbookFile))
            {
                DateTime date = DateTime.Now;
                string timeToJson = $"{date.Year}/{date.Month}/{date.Day} - {date.Hour}:{date.Minute}:{date.Second}";

                char[] charsToTrim = { ' ', 'n', 'm' };
                string distanceToJson = textbox_distanceNM.Text.TrimEnd(charsToTrim);

                string encryptedLogbookFileText = File.ReadAllText(logbookFile);
                string decryptedLogBookFile = create_account_utils.DecryptText(encryptedLogbookFileText, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");

                List<LogBook.Classes.flightHistory> flights = JsonConvert.DeserializeObject<List<LogBook.Classes.flightHistory>>(decryptedLogBookFile);
                LogBook.Classes.flightHistory flight = new LogBook.Classes.flightHistory()
                {
                    jobID = textbox_jobID.Text,
                    jobName = textbox_jobName.Text,
                    jobType = "CT",
                    weight = textbox_weight.Text,
                    time = timeToJson,
                    startICAO = textbox_StartIcao.Text,
                    endICAO = textbox_endIcao.Text,
                    distance = double.Parse(distanceToJson)
                };


                if (flights[0].jobID == "FILL")
                    flights[0] = flight;
                else
                    flights.Add(flight);


                string flightsToJson = JsonConvert.SerializeObject(flights);
                string encryptedFlightsToJson = create_account_utils.EncryptText(flightsToJson, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");

                File.WriteAllText(logbookFile, encryptedFlightsToJson);

                MainMenu.main_menu menu = new MainMenu.main_menu(Pilotusername, Pilotname, Pilotsurname);

                menu.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Profile Corrupted. Directory or logbook file missing");
            }


        }
    }

    //
}
