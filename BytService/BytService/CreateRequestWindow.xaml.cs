using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

namespace BytService
{
    /// <summary>
    /// Логика взаимодействия для CreateRequestWindow.xaml
    /// </summary>
    public partial class CreateRequestWindow : Window
    {
        public CreateRequestWindow()
        {
            InitializeComponent();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ObjID = 0;
                RepairObjects repairObjects = new RepairObjects
                {
                    homeTechType = TypeTextBox.Text,
                    homeTechModel = ModelTextBox.Text,
                    problemDescryption = ProblemTextBox.Text
                };

                App.DB.RepairObjects.Add(repairObjects);
                App.DB.SaveChanges();
                ObjID = App.DB.RepairObjects.Max(x => x.repairObjectsID);

                Requests requests = new Requests
                {
                    clientID = MainWindow.userID,
                    repairObjectID = ObjID,
                    requestStatus = "Новый заказ",
                    startDate = DateTime.Today
                };
                App.DB.Requests.Add(requests);
                App.DB.SaveChanges();
                MessageBox.Show("Заявка успешно создана.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch
            {
                MessageBox.Show("При создании заявки произошла ошибка!\nПроверьте правильность заполнения полей и подключение к серверу.","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
