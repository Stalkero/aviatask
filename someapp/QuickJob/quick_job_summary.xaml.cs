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

namespace someapp.QuickJob
{
    /// <summary>
    /// Interaction logic for quick_job_summary.xaml
    /// </summary>
    public partial class quick_job_summary
    {
        public int status { get; set; }
        private static Stopwatch _stopwatch;

        public quick_job_summary(string username, string startICAO,string endICAO,string jobID,string jobType,double distance,string weight,string desc)
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

            _stopwatch = new Stopwatch();


            if (jobID.StartsWith("PT_"))
            {
                textbox_JobStatus.Text = "Get into aircraft";

                Wpf.Ui.Controls.Button btn_chk_1 = new Wpf.Ui.Controls.Button();
                btn_chk_1.Content = "1. Get into aircraft";
                btn_chk_1.Width = 440;
                btn_chk_1.Click += Btn_chk_1_Click;
                btn_chk_1.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_2 = new Wpf.Ui.Controls.Button();
                btn_chk_2.Content = "2. Pre-flight checklist";
                btn_chk_2.Width = 440;
                btn_chk_2.Click += Btn_chk_2_Click;
                btn_chk_2.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_3 = new Wpf.Ui.Controls.Button();
                btn_chk_3.Content = "3. Check flight plan";
                btn_chk_3.Width = 440;
                btn_chk_3.Click += Btn_chk_3_Click;
                btn_chk_3.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_4 = new Wpf.Ui.Controls.Button();
                btn_chk_4.Content = "4. Check weather report";
                btn_chk_4.Width = 440;
                btn_chk_4.Click += Btn_chk_4_Click;
                btn_chk_4.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_5 = new Wpf.Ui.Controls.Button();
                btn_chk_5.Content = "5. Engine start nad getting ready to fly";
                btn_chk_5.Width = 440;
                btn_chk_5.Click += Btn_chk_5_Click;
                btn_chk_5.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_6 = new Wpf.Ui.Controls.Button();
                btn_chk_6.Content = "6. Ready to fly";
                btn_chk_6.Width = 440;
                btn_chk_6.Click += Btn_chk_6_Click;
                btn_chk_6.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_7 = new Wpf.Ui.Controls.Button();
                btn_chk_7.Content = "7. Flying";
                btn_chk_7.Width = 440;
                btn_chk_7.Click += Btn_chk_7_Click;
                btn_chk_7.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_8 = new Wpf.Ui.Controls.Button();
                btn_chk_8.Content = "8. Getting ready to land";
                btn_chk_8.Width = 440;
                btn_chk_8.Click += Btn_chk_8_Click;
                btn_chk_8.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_9 = new Wpf.Ui.Controls.Button();
                btn_chk_9.Content = "9. Finishing job";
                btn_chk_9.Width = 440;
                btn_chk_9.Click += Btn_chk_9_Click;
                btn_chk_9.Margin = new Thickness(0, 5, 0, 5);

                ChecklistPanel.Children.Add(btn_chk_1);
                ChecklistPanel.Children.Add(btn_chk_2);
                ChecklistPanel.Children.Add(btn_chk_3);
                ChecklistPanel.Children.Add(btn_chk_4);
                ChecklistPanel.Children.Add(btn_chk_5);
                ChecklistPanel.Children.Add(btn_chk_6);
                ChecklistPanel.Children.Add(btn_chk_7);
                ChecklistPanel.Children.Add(btn_chk_8);
                ChecklistPanel.Children.Add(btn_chk_9);
            }
            if (jobID.StartsWith("CT_"))
            {
                textbox_JobStatus.Text = "Verify cargo loading";

                Wpf.Ui.Controls.Button btn_chk_1 = new Wpf.Ui.Controls.Button();
                btn_chk_1.Content = "1. Verify cargo loading";
                btn_chk_1.Width = 440;
                btn_chk_1.Click += Btn_chk_1_Click;
                btn_chk_1.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_2 = new Wpf.Ui.Controls.Button();
                btn_chk_2.Content = "2. Confirm cargo weight and balance";
                btn_chk_2.Width = 440;
                btn_chk_2.Click += Btn_chk_2_Click;
                btn_chk_2.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_3 = new Wpf.Ui.Controls.Button();
                btn_chk_3.Content = "3. Check weather conditions";
                btn_chk_3.Width = 440;
                btn_chk_3.Click += Btn_chk_3_Click;
                btn_chk_3.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_4 = new Wpf.Ui.Controls.Button();
                btn_chk_4.Content = "4. Get into aircraft";
                btn_chk_4.Width = 440;
                btn_chk_4.Click += Btn_chk_4_Click;
                btn_chk_4.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_5 = new Wpf.Ui.Controls.Button();
                btn_chk_5.Content = "5. Pre-flight checklist";
                btn_chk_5.Width = 440;
                btn_chk_5.Click += Btn_chk_5_Click;
                btn_chk_5.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_6 = new Wpf.Ui.Controls.Button();
                btn_chk_6.Content = "6. Check flight plan";
                btn_chk_6.Width = 440;
                btn_chk_6.Click += Btn_chk_6_Click;
                btn_chk_6.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_7 = new Wpf.Ui.Controls.Button();
                btn_chk_7.Content = "7. Check weather report";
                btn_chk_7.Width = 440;
                btn_chk_7.Click += Btn_chk_7_Click;
                btn_chk_7.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_8 = new Wpf.Ui.Controls.Button();
                btn_chk_8.Content = "8. Engine start and getting ready to fly";
                btn_chk_8.Width = 440;
                btn_chk_8.Click += Btn_chk_8_Click;
                btn_chk_8.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_9 = new Wpf.Ui.Controls.Button();
                btn_chk_9.Content = "9. Ready to fly";
                btn_chk_9.Width = 440;
                btn_chk_9.Click += Btn_chk_9_Click;
                btn_chk_9.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_10 = new Wpf.Ui.Controls.Button();
                btn_chk_10.Content = "10. Flying";
                btn_chk_10.Width = 440;
                btn_chk_10.Click += Btn_chk_10_Click;
                btn_chk_10.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_11 = new Wpf.Ui.Controls.Button();
                btn_chk_11.Content = "11. Getting ready to land";
                btn_chk_11.Width = 440;
                btn_chk_11.Click += Btn_chk_11_Click;
                btn_chk_11.Margin = new Thickness(0, 5, 0, 5);

                Wpf.Ui.Controls.Button btn_chk_12 = new Wpf.Ui.Controls.Button();
                btn_chk_12.Content = "12. Finishing job";
                btn_chk_12.Width = 440;
                btn_chk_12.Click += Btn_chk_12_Click;
                btn_chk_12.Margin = new Thickness(0, 5, 0, 5);

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


        }


        private void Btn_chk_1_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            clickedButton.IsEnabled = false;
            Progress_ReadyToFly.Value += 10;
            status = 1;
            textbox_JobStatus.Text = "Confirm cargo weight and balance";

        }
        private void Btn_chk_2_Click(object sender, RoutedEventArgs e)
        {
            if (status == 1)
            {
                Button clickedButton = (Button)sender;

                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 2;
                textbox_JobStatus.Text = "Check weather conditions";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void Btn_chk_3_Click(object sender, RoutedEventArgs e)
        {
            if (status == 2)
            {
                Button clickedButton = (Button)sender;

                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 3;
                textbox_JobStatus.Text = "Get into aircraft";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void Btn_chk_4_Click(object sender, RoutedEventArgs e)
        {
            if (status == 3)
            {
                Button clickedButton = (Button)sender;

                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status= 4;
                textbox_JobStatus.Text = "Pre-flight checklist";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void Btn_chk_5_Click(object sender, RoutedEventArgs e)
        {
            if (status == 4)
            {
                Button clickedButton = (Button)sender;

                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 5;
                textbox_JobStatus.Text = "Check flight plan";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void Btn_chk_6_Click(object sender, RoutedEventArgs e)
        {
            if (status == 5)
            {
                Button clickedButton = (Button)sender;

                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 6;
                textbox_JobStatus.Text = "Check weather report";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void Btn_chk_7_Click(object sender, RoutedEventArgs e)
        {
            if (status == 6)
            {
                Button clickedButton = (Button)sender;

                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 7;
                textbox_JobStatus.Text = "Engine start nad getting ready to fly";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void Btn_chk_8_Click(object sender, RoutedEventArgs e)
        {
            if (status == 7)
            {
                Button clickedButton = (Button)sender;

                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 8;
                textbox_JobStatus.Text = "Ready to fly";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void Btn_chk_9_Click(object sender, RoutedEventArgs e)
        {
            if (status == 8)
            {
                Button clickedButton = (Button)sender;

                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 9;
                textbox_JobStatus.Text = "Flying";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void Btn_chk_10_Click(object sender, RoutedEventArgs e)
        {
            if (status == 9)
            {
                Button clickedButton = (Button)sender;

                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 10;
                textbox_JobStatus.Text = "Getting ready to land";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }

        private void Btn_chk_11_Click(object sender, RoutedEventArgs e)
        {
            if (status == 10)
            {
                Button clickedButton = (Button)sender;

                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 11;
                textbox_JobStatus.Text = "Finishing job";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
        private void Btn_chk_12_Click(object sender, RoutedEventArgs e)
        {
            if (status == 11)
            {
                Button clickedButton = (Button)sender;

                clickedButton.IsEnabled = false;
                Progress_ReadyToFly.Value += 10;
                status = 12;
                textbox_JobStatus.Text = "Job finished";
            }
            else
                MessageBox.Show("Please comlete previous checklist steps");
        }
    }
}
