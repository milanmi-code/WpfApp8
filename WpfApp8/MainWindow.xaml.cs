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

namespace WpfApp8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ServerConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            start();
        }
        void start()
        {
            connection = new ServerConnection("http://127.1.1.1:3000");
        }
        async void LoginClick(object s, EventArgs e)
        {
            bool temp = await connection.Login(usernameInput.Text, passwordInput.Password);
            if (temp)
            {
                MessageBox.Show("Sikeres bejelentkezés");
                afterlogin a = new afterlogin(connection) { Top = this.Top, Left = this.Left, Visibility = Visibility.Visible };
                Window1 b = new Window1(connection) { Top = this.Top, Left = this.Left, Visibility = Visibility.Visible };
                this.Hide();
                a.Show();
                b.Show();
                a.Closing += (ss, ee) =>
                {
                    this.Show();
                };
            }
        }
        async void RegClick(object s, EventArgs e)
        {
            bool temp = await connection.Register(usernameInput.Text, passwordInput.Password);
            if (temp)
            {
                MessageBox.Show("Sikeres");
            }
        }

    }
}
