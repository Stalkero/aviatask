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
using System.Windows.Media.Animation;
using Wpf.Ui.Appearance;

namespace someapp.QuickJob
{
    /// <summary>
    /// Interaction logic for quick_job_summary.xaml
    /// </summary>
    public partial class people
    {
        public int status { get; set; }

        public people(string username, string startICAO,string endICAO,string jobID,string jobType,double distance,string weight,string desc)
        {
            InitializeComponent();

            status = 0;
            textbox_jobID.Text = jobID;
            textbox_jobName.Text = jobType;
            textbox_weight.Text = weight;
            textBox_jobDesc.Text = desc;
            textbox_StartIcao.Text = startICAO;
            textbox_endIcao.Text = endICAO;
            textbox_distanceNM.Text = $"{Math.Round((distance / 1852)),2} nm";
            textbox_distanceKM.Text = $"{Math.Round((distance/ 1000), 2)} km";
            browserLeft.Address = $"https://fshub.io/airport/{startICAO}/overview";
            browserRight.Address = $"https://fshub.io/airport/{endICAO}/overview";

            textbox_JobStatus.Text = "Check weather conditions";

            Wpf.Ui.Controls.Button btn_chk_1 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "1. Check weather conditions",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5),
            };
            btn_chk_1.Click += PT_Btn_chk_1_Click;

            Wpf.Ui.Controls.Button btn_chk_2 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "2. Get into aircraft",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_2.Click += PT_Btn_chk_2_Click;

            Wpf.Ui.Controls.Button btn_chk_3 = new Wpf.Ui.Controls.Button() 
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "3. Pre-flight checklist",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_3.Click += PT_Btn_chk_3_Click;

            Wpf.Ui.Controls.Button btn_chk_4 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "4. Check flight plan",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_4.Click += PT_Btn_chk_4_Click;

            Wpf.Ui.Controls.Button btn_chk_5 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "5. Check weather report",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_5.Click += PT_Btn_chk_5_Click;

            Wpf.Ui.Controls.Button btn_chk_6 = new Wpf.Ui.Controls.Button() 
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "6. Engine start nad getting ready to fly",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_6.Click += PT_Btn_chk_6_Click;

            Wpf.Ui.Controls.Button btn_chk_7 = new Wpf.Ui.Controls.Button() 
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "7. Ready to fly",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_7.Click += PT_Btn_chk_7_Click;

            Wpf.Ui.Controls.Button btn_chk_8 = new Wpf.Ui.Controls.Button() 
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "8. Flying",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_8.Click += PT_Btn_chk_8_Click;

            Wpf.Ui.Controls.Button btn_chk_9 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "9. Getting ready to land",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_9.Click += PT_Btn_chk_9_Click;

            Wpf.Ui.Controls.Button btn_chk_10 = new Wpf.Ui.Controls.Button()
            {
                Appearance = Wpf.Ui.Common.ControlAppearance.Primary,
                Content = "10. Finishing job",
                Width = 440,
                Margin = new Thickness(0, 5, 0, 5)
            };
            btn_chk_10.Click += PT_Btn_chk_10_Click;


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



        }

        private void PT_Btn_chk_1_Click(object sender, RoutedEventArgs e)
        {
            if (status == 0)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += " ✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 1;
                textbox_JobStatus.Text = "Get into aircraft";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void PT_Btn_chk_2_Click(object sender, RoutedEventArgs e)
        {
            if (status == 1)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += " ✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 2;
                textbox_JobStatus.Text = "Pre-flight checklist";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void PT_Btn_chk_3_Click(object sender, RoutedEventArgs e)
        {
            if (status == 2)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += " ✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 3;
                textbox_JobStatus.Text = "Check flight plan";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void PT_Btn_chk_4_Click(object sender, RoutedEventArgs e)
        {
            if (status == 3)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += " ✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 4;
                textbox_JobStatus.Text = "Check weather report";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void PT_Btn_chk_5_Click(object sender, RoutedEventArgs e)
        {
            if (status == 4)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += " ✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 5;
                textbox_JobStatus.Text = "Engine start nad getting ready to fly";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void PT_Btn_chk_6_Click(object sender, RoutedEventArgs e)
        {
            if (status == 5)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += " ✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 6;
                textbox_JobStatus.Text = "Ready to fly";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void PT_Btn_chk_7_Click(object sender, RoutedEventArgs e)
        {
            if (status == 6)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += " ✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 7;
                textbox_JobStatus.Text = "Flying";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void PT_Btn_chk_8_Click(object sender, RoutedEventArgs e)
        {
            if (status == 7)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += " ✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 8;
                textbox_JobStatus.Text = "Getting ready to land";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void PT_Btn_chk_9_Click(object sender, RoutedEventArgs e)
        {
            if (status == 8)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += " ✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 9;
                textbox_JobStatus.Text = "Finishing job";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void PT_Btn_chk_10_Click(object sender, RoutedEventArgs e)
        {
            if (status == 9)
            {
                Button clickedButton = (Button)sender;
                clickedButton.Content += " ✓";
                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 10;
                textbox_JobStatus.Text = "Job finished";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
    }
}
