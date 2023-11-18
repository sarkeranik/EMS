
using EMS.Models;
using EMS.Services;
using EMS.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
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


namespace EMS.Views
{
    public partial class MainView : Window
    {
        private readonly IGoRestClientService _goRestClientService;
        private MainViewModel _mainViewModel { get; set; }

        public MainView(IGoRestClientService goRestClientService)
        {
            InitializeComponent();

            _goRestClientService = goRestClientService;
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            _mainViewModel = new MainViewModel(_goRestClientService, this);
            this.DataContext = _mainViewModel;
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsersDataGrid.Items.Filter = FilterMethod;
        }
        private bool FilterMethod(object obj)
        {
            var user = (Models.UserDto)obj;

            return user.Name.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase)
                || user.Email.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase)
                || user.Gender.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase)
                || Convert.ToString(user.Id).Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase)
                || user.Status.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }

        private void UsersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;
            var user = row.DataContext as Models.UserDto;

            //MessageBox.Show($"{user.Name} row clicked");

            if (user != null)
            {
                _mainViewModel.ShowEditUserWindowCommand.Execute(user);
            }
        }
    }
}
