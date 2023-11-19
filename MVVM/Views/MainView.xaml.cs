
using EMS.Models;
using EMS.Services;
using EMS.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Window = System.Windows.Window;

namespace EMS.Views
{
    public partial class MainView : Window
    {
        private readonly IExportToExcelService _exportToExcelService;
        private readonly IGoRestClientService _goRestClientService;
        private MainViewModel _mainViewModel { get; set; }

        public MainView(IGoRestClientService goRestClientService, IExportToExcelService exportToExcelService)
        {
            InitializeComponent();

            _goRestClientService = goRestClientService;
            _exportToExcelService = exportToExcelService;
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            _mainViewModel = new MainViewModel(_goRestClientService, this, _exportToExcelService);
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

            if (user != null)
            {
                _mainViewModel.ShowEditUserWindowCommand.Execute(user);
            }
        }

        private void DDNumbersOfPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            _mainViewModel.NumbersOfPagesSelectionChangedommand.Execute(cmb.SelectedValue);
        }
    }
}
