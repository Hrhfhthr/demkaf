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
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public class RequestsForClients
        {
            public object Id {get; set;}
            public object StartDate { get; set; }
            public object Type { get; set; }
            public object Model { get; set; }
            public object Problem { get; set; }
            public object Status { get; set; }
            public object EndDate { get; set; }
            public object Master { get; set; }

        }
        public void Load()
        {
            List<RequestsForClients> requests = new List<RequestsForClients>();
            foreach (var request in App.DB.Requests)
            {
                if (request.clientID == MainWindow.userID)
                {
                    string typeObj = "";
                    string modelObj = "";
                    string problemObj = "";
                    string masterName = "";
                    string endDate = "";

                    foreach (var repairObj in App.DB.RepairObjects)
                    {
                        if (repairObj.repairObjectsID == request.repairObjectID)
                        {
                            typeObj = repairObj.homeTechType;
                            modelObj = repairObj.homeTechModel;
                            problemObj = repairObj.problemDescryption;
                        }
                    }

                    foreach (var master in App.DB.Users)
                    {
                        if (master.userID == request.masterID)
                        {
                            masterName = master.fio;
                        }
                    }

                    if (request.completionDate != null)
                    {
                        DateTime d = Convert.ToDateTime(request.completionDate);
                        endDate = d.ToString("dd/MM/yy");
                    }
                    else
                    {
                        endDate = null;
                    }

                    RequestsForClients newRequest = new RequestsForClients { Id = request.requestID, StartDate = request.startDate.ToString("dd/MM/yy"), Type = typeObj, Model = modelObj, Problem = problemObj, Status = request.requestStatus, EndDate = endDate, Master = masterName };
                    requests.Add(newRequest);
                }
            }
            CreatedRequestsDataGrid.ItemsSource = requests;
        }
        public ClientWindow()
        {
            

            this.WindowState = WindowState.Maximized;
            InitializeComponent();
            Load();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void CreateRequestButton_Click(object sender, RoutedEventArgs e)
        {
            CreateRequestWindow createRequestWindow = new CreateRequestWindow();
            createRequestWindow.ShowDialog();
            Load();
        }

        private void ChangeRequestButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeRequestWindow changeRequestWindow = new ChangeRequestWindow();
            changeRequestWindow.ShowDialog();
            Load();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            RequestsForClients request=CreatedRequestsDataGrid.SelectedItem as RequestsForClients;
            IDTextBox.Text = request.Id.ToString();
            StartDateTextBox.Text=request.StartDate.ToString();
            TypeTextBox.Text = request.Type.ToString();
            ModelTextBox.Text = request.Model.ToString();
            ProblemTextBox.Text = request.Problem.ToString();
            StatusTextBox.Text = request.Status.ToString();
            if (request.EndDate != null)
            {
                EndDateTextBox.Text = request.EndDate.ToString();
            }
            if(request.Master != null)
            {
                MasterTextBox.Text = request.Master.ToString();
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idd = Convert.ToInt32(IDTextBox.Text);
                var editRequest = App.DB.Requests.Where(x => x.requestID == idd).FirstOrDefault();
                int idReObj = editRequest.repairObjectID;
                var editReObj = App.DB.RepairObjects.Where(x => x.repairObjectsID == idReObj).FirstOrDefault();
                editReObj.homeTechType = TypeTextBox.Text;
                editReObj.homeTechModel = ModelTextBox.Text;
                editReObj.problemDescryption = ProblemTextBox.Text;
                App.DB.SaveChanges();
                MessageBox.Show("Заявка успешно изменена.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                Load();
            }
            catch
            {
                MessageBox.Show("Не удалось изменить заявку.\nПопробуйте повторить попытку позже и проверьте соединение с сервером.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<RequestsForClients> requests = new List<RequestsForClients>();
            foreach (var search in App.DB.RepairObjects)
            {
                if (search.homeTechModel == SearchTextBox.Text)
                {
                    foreach (var request in App.DB.Requests)
                    {
                        if (request.clientID == MainWindow.userID)
                        {
                            string typeObj = "";
                            string modelObj = "";
                            string problemObj = "";
                            string masterName = "";
                            string endDate = "";

                            foreach (var repairObj in App.DB.RepairObjects)
                            {
                                if (repairObj.repairObjectsID == request.repairObjectID)
                                {
                                    typeObj = repairObj.homeTechType;
                                    modelObj = repairObj.homeTechModel;
                                    problemObj = repairObj.problemDescryption;
                                }
                            }

                            foreach (var master in App.DB.Users)
                            {
                                if (master.userID == request.masterID)
                                {
                                    masterName = master.fio;
                                }
                            }

                            if (request.completionDate != null)
                            {
                                DateTime d = Convert.ToDateTime(request.completionDate);
                                endDate = d.ToString("dd/MM/yy");
                            }
                            else
                            {
                                endDate = null;
                            }

                            RequestsForClients newRequest = new RequestsForClients { Id = request.requestID, StartDate = request.startDate.ToString("dd/MM/yy"), Type = typeObj, Model = modelObj, Problem = problemObj, Status = request.requestStatus, EndDate = endDate, Master = masterName };
                            requests.Add(newRequest);
                        }
                    }
                }
            }
            
            CreatedRequestsDataGrid.ItemsSource = requests;
        }
    }
}
