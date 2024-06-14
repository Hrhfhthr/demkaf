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

namespace BytService
{
    /// <summary>
    /// Логика взаимодействия для OperaterWindow.xaml
    /// </summary>
    public partial class OperaterWindow : Window
    {
        public class RequestsForOperators
        {

        }
        public void Load()
        {
            var loadList=App.DB.RepairObjects.ToList();
            NewRequestsDataGrid.ItemsSource=loadList;
        }
        public OperaterWindow()
        {
            InitializeComponent();
            this.WindowState=WindowState.Maximized;
            Load();
        }
    }
}
