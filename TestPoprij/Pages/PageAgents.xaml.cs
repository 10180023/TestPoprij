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
using TestPoprij.Model;

namespace TestPoprij.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAgents.xaml
    /// </summary>
    public partial class PageAgents : Page
    {
        

        public PageAgents()
        {
            InitializeComponent();

            var currentAgents = Poprijenok2Entities.GetContext().Agent.ToList();
            var types = Poprijenok2Entities.GetContext().AgentType.ToList();

            List<String> sort = new List<string>();
            sort.Add("Сортировка");
            sort.Add("По наименованию");
            sort.Add("По скидке");
            sort.Add("По приоритетности");

            types.Insert(0, new AgentType
            {
                Title = "Все типы"
            });

            cbFilter.DisplayMemberPath = "Title";
            cbFilter.SelectedValuePath = "ID";
            cbFilter.SelectedIndex = 0;
            cbFilter.ItemsSource = types;

            cbSort.SelectedIndex = 0;
            cbSort.ItemsSource = sort;

            lvAgents.ItemsSource = currentAgents;

            tbFinder.Text = "Поиск по названию";
        }

        private void UpdateAgents()
        {
            var currentAgents = Poprijenok2Entities.GetContext().Agent.ToList();

            if (cbFilter.SelectedIndex > 0)
            {
                currentAgents = currentAgents.Where(a => a.AgentTypeID == int.Parse(cbFilter.SelectedValue.ToString())).ToList();
            }

            if (cbSort.SelectedIndex > 0)
            {
                switch (cbSort.SelectedIndex)
                {
                    case 1:
                        if (rbAsc.IsChecked == true)
                        {
                            currentAgents = currentAgents.OrderBy(a => a.Title).ToList();
                        }
                        else
                            currentAgents = currentAgents.OrderByDescending(a => a.Title).ToList();
                        break;
                    case 2:
                        break;
                    case 3:
                        if (rbAsc.IsChecked == true)
                        {
                            currentAgents = currentAgents.OrderBy(a => a.Priority).ToList();
                        }
                        else
                            currentAgents = currentAgents.OrderByDescending(a => a.Priority).ToList();
                        break;
                }
            }

            if (tbFinder.Text == "Поиск по названию")
            {
                tbFinder.Text = "";
            }

            currentAgents = currentAgents.Where(a => a.Title.ToLower().Contains(tbFinder.Text.ToLower())).ToList();
            lvAgents.ItemsSource = currentAgents.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageAddEdit(null));
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageAddEdit((sender as Button).DataContext as Agent));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Poprijenok2Entities.GetContext().ChangeTracker.Entries().ToList();
                lvAgents.ItemsSource = Poprijenok2Entities.GetContext().Agent.ToList();
            }
        }

        private void tbFinder_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void tbFinder_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbFinder.Text == "Поиск по названию")
            {
                tbFinder.Text = "";
            }
        }

        private void rbAsc_Click(object sender, RoutedEventArgs e)
        {
            UpdateAgents();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateAgents();
        }
    }
}
