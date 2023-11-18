
using EMS.Models;
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

    public partial class UpsertUserView : Window
    {
        private readonly IGoRestClientService _goRestClientService;

        public UpsertUserView(IGoRestClientService goRestClientService, UserDto userDto = null)
        {
            InitializeComponent();
            _goRestClientService = goRestClientService;

            UpsertUserViewModel addUserViewModel = new UpsertUserViewModel(_goRestClientService, this, userDto);
            this.DataContext = addUserViewModel;
        }
    }
}
