using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace Aviatask
{
    /// <summary>
    /// Interaction logic for window_AviaTask_main.xaml
    /// </summary>
    public partial class window_AviaTask_main : Window
    {
        public int size = 0;

        public window_AviaTask_main(string username, string name, string surname)
        {

            InitializeComponent();



            main_frame.Content = new MainMenu.main_menu(username,name,surname);
            MainMenu.main_menu.tablet_size = size;
            Settings.Main.tablet_size = size;
            Settings.Generation.tablet_size = size;
            Settings.Profile.tablet_size = size;
            LogBook.Logbook.tablet_size = size;


        }

        private void UiWindow_StateChanged(object sender, EventArgs e)
        {

        }

        private void btn_Shutdown_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.main_menu.MainMenuClose = true;
            Settings.Main.SettingsClose = true;
            Settings.Generation.SettingsClose = true;
            Settings.Profile.SettingsClose = true;
            LogBook.Logbook.LogBookClose = true;
            this.Close();
        }

        private void Btn_Size_Plus_Click(object sender, RoutedEventArgs e)
        {
            size--;

            MainMenu.main_menu.tablet_size = size;
            Settings.Main.tablet_size = size;
            Settings.Generation.tablet_size = size;
            Settings.Profile.tablet_size = size;
            LogBook.Logbook.tablet_size = size;

            switch (size)
            {
                case 0:
                    window.Width = 1920;
                    window.Height = 1032;
                    main_frame.Margin = new Thickness(0,16,0,0);

                    break;
                case 1:
                    window.Width = 1600;
                    window.Height = 848;
                    main_frame.Margin = new Thickness(0, 13, 0, 0);

                    break;
            }
            gridd.Width = window.Width;
            gridd.Height = window.Height;


            window.UpdateLayout();
        }

        private void Btn_Size_Minus_Click(object sender, RoutedEventArgs e)
        {
            if (size != 1)
                size++;

            MainMenu.main_menu.tablet_size = size;
            Settings.Main.tablet_size = size;
            Settings.Generation.tablet_size = size;
            Settings.Profile.tablet_size = size;
            LogBook.Logbook.tablet_size = size;

            switch (size)
            {//Aspect ratio close to 1.889
                case 0:
                    window.Width = 1920;
                    window.Height = 1032;
                    main_frame.Margin = new Thickness(0, 16, 0, 0);

                    break;
                case 1:
                    window.Width = 1600;
                    window.Height = 848;
                    main_frame.Margin = new Thickness(0, 13, 0, 0);
                    break;
            }
            gridd.Width = window.Width;
            gridd.Height = window.Height;
            window.UpdateLayout();

        }

        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

    }
}
