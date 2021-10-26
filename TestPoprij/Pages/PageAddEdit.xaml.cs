using System;
using System.Collections.Generic;
using System.IO;
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
using TestPoprij.Model;

namespace TestPoprij.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddEdit.xaml
    /// </summary>
    public partial class PageAddEdit : Page
    {
        private Agent _currentAgent = new Agent();

        public PageAddEdit(Agent selectedAgent)
        {
            InitializeComponent();
            if (selectedAgent != null)
            {
                _currentAgent = selectedAgent;
            }

            DataContext = _currentAgent;
            cbTypes.ItemsSource = Poprijenok2Entities.GetContext().AgentType.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentAgent.Title))
                errors.AppendLine("Введите название");
            if (_currentAgent.AgentType == null)
                errors.AppendLine("Выберите тип");
            if (string.IsNullOrWhiteSpace(_currentAgent.Address))
                errors.AppendLine("Введите адрес");
            if (string.IsNullOrWhiteSpace(_currentAgent.INN))
                errors.AppendLine("Введите ИНН");
            if (string.IsNullOrWhiteSpace(_currentAgent.KPP))
                errors.AppendLine("Введите КПП");
            if (string.IsNullOrWhiteSpace(_currentAgent.DirectorName))
                errors.AppendLine("Введите имя директора");
            if (string.IsNullOrWhiteSpace(_currentAgent.Phone))
                errors.AppendLine("Введите номер телефона");
            if (string.IsNullOrWhiteSpace(_currentAgent.Email))
                errors.AppendLine("Введите Email");
            if (string.IsNullOrWhiteSpace(_currentAgent.Logo))
            {
                errors.AppendLine("Выберите логотип");
                _currentAgent.Logo = null;
            }    
            if (_currentAgent.Priority == 0)
                errors.AppendLine("Введите приоритет");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_currentAgent.ID == 0)
            {
                Poprijenok2Entities.GetContext().Agent.Add(_currentAgent);
            }

            try
            {
                Poprijenok2Entities.GetContext().SaveChanges();
                MessageBox.Show("Информация добавлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить агента?", "Внимание", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Poprijenok2Entities.GetContext().Agent.Remove(_currentAgent);
                    Poprijenok2Entities.GetContext().SaveChanges();
                    MessageBox.Show("Запись удалена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    Manager.MainFrame.GoBack();
                }
                catch (Exception)
                {

                    MessageBox.Show("Невозможно удалить не существующего агента", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            

        }
    }
}
