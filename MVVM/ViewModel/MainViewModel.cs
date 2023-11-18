using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EMS.Core;
using EMS.Models;
using EMS.Services;
using EMS.Views;

namespace EMS.ViewModel
{
	public class MainViewModel
	{
		private readonly IGoRestClientService _goRestClientService;
        public List<UserDto> Users { get; set; }
		public ICommand ShowWindowCommand { get; set; }

        public MainViewModel(IGoRestClientService goRestClientService)
        {
            _goRestClientService = goRestClientService;

            //var gorestClientService = new GoRestClientService(new System.Net.Http.HttpClient());

            Users = _goRestClientService.GetAllUsersAsync(null, null, null).Result;
            ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);
        }

        private bool CanShowWindow(object obj)
		{
			return true;
		}
		private void ShowWindow(object obj)
		{
			var mainWindow = obj as Window;

			AddUser addUserWin = new AddUser();
			addUserWin.Owner = mainWindow;
			addUserWin.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			addUserWin.Show();


		}
	}
}
