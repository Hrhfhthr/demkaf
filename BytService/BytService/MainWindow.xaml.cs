using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace BytService
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int userID;
        public int UserCheck(string login,string password)
        {
            int checkResult = 0;
            foreach(var user in App.DB.Users)
            {
                if (user.login == login)
                {
                    if (user.password == password)
                    {
                        userID = user.userID;
                        if (user.type == "Менеджер")
                        {
                            checkResult = 1;
                        }
                        else if(user.type == "Мастер")
                        {
                            checkResult = 2;
                        }
                        else if (user.type == "Оператор")
                        {
                            checkResult = 3;
                        }
                        else if (user.type == "Заказчик")
                        {
                            checkResult = 4;
                        }
                    }
                }
            }


            return checkResult;
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegistrButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrWindow registrWindow = new RegistrWindow();
            registrWindow.ShowDialog();
        }

        private void AuthoButton_Click(object sender, RoutedEventArgs e)
        {
            int userRole=UserCheck(LoginTextBox.Text, PassTextBox.Text);
            if (userRole == 0)
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (userRole == 1)
            {
                ManagerWindow managerWindow = new ManagerWindow();
                managerWindow.Show();
                this.Close();
            }
            else if (userRole == 2)
            {
                MasterWindow masterWindow = new MasterWindow();
                masterWindow.Show();
                this.Close();
            }
            else if (userRole == 3)
            {
                OperaterWindow operaterWindow = new OperaterWindow();
                operaterWindow.Show();
                this.Close();
            }
            else if (userRole == 4)
            {
                ClientWindow clientWindow = new ClientWindow();
                clientWindow.Show();
                this.Close();
            }
        }
    }
}
