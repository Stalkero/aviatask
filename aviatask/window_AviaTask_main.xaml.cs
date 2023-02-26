using System;
using System.Collections.Generic;
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

namespace Aviatask
{
    /// <summary>
    /// Interaction logic for window_AviaTask_main.xaml
    /// </summary>
    public partial class window_AviaTask_main
    {



        public window_AviaTask_main(string username, string name, string surname)
        {
            InitializeComponent();

            main_frame.Content = new MainMenu.main_menu(username,name,surname);
        }

        private void TitleBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void UiWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
        }
    }
}
