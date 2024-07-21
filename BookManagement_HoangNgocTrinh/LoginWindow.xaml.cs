using Repositories.Entities;
using Services;
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
using System.Windows.Shapes;

namespace BookManagement_HoangNgocTrinh
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UserAccountService _service = new();
        
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //check email & pass, check role in DB
            //role == 1 || 2, vào mh chính
            //ko thì popup chửi
            //giả bộ show main trc
            UserAccount user = _service.GetOne(EmailTextBox.Text, PasswordTextBox.Text);
            // Check if a user was found and if their role allows access
            if (user == null)
            {
                MessageBox.Show("Invalid username or password!", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (user != null)
            {
               MainWindow mainWindow = new MainWindow(user);
                mainWindow.Show(); 
                this.Hide(); 
            }
            else
            {
               MessageBox.Show("You have no permission!", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

		private void Quit_Click(object sender, RoutedEventArgs e)
		{
            Application.Current.Shutdown();
            //Winform: Application.Exit
		}
	}
}
