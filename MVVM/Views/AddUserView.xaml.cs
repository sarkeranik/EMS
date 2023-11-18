
using EMS.Services;
using EMS.ViewModel;
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

namespace EMS.Views
{
	
	public partial class AddUserView : Window
	{
        private readonly IGoRestClientService _goRestClientService;

        public AddUserView(IGoRestClientService goRestClientService)
        {
            InitializeComponent();
            _goRestClientService = goRestClientService;

            AddUserViewModel addUserViewModel = new AddUserViewModel(_goRestClientService);
            this.DataContext = addUserViewModel;
        }
    }
}
