
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
	public partial class Main : Window
	{
        private readonly IGoRestClientService _goRestClientService;

        public Main(IGoRestClientService goRestClientService)
        {
            InitializeComponent();

            _goRestClientService = goRestClientService;
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            MainViewModel mainViewModel = new MainViewModel(_goRestClientService);
            this.DataContext = mainViewModel;
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			UserList.Items.Filter = FilterMethod;
		}

		private bool FilterMethod(object obj)
		{
			var user = (Models.UserDto)obj;

			return user.Name.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
		}
	}
}
