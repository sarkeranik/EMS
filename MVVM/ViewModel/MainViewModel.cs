using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EMS.Core.Commands;
using EMS.Models;
using EMS.Services;
using EMS.Views;

namespace EMS.ViewModel
{
    public class MainViewModel
    {
        private readonly IExportToExcelService _exportToExcelService;
        private readonly IGoRestClientService _goRestClientService;
        private readonly MainView _window;

        private List<UserDto> Users { get; set; }

        public ICommand ShowAddUserWindowCommand { get; set; }
        public ICommand ShowEditUserWindowCommand { get; set; }
        public ICommand ExportToCsvCommand { get; set; }

        public MainViewModel(IGoRestClientService goRestClientService, Window window, IExportToExcelService exportToExcelService)
        {
            _goRestClientService = goRestClientService;
            _window = (MainView)window;
            _exportToExcelService = exportToExcelService;
            _window.UsersDataGrid.ItemsSource = _goRestClientService.GetAllUsersAsync(null, null, null).Result;

            ShowAddUserWindowCommand = new RelayCommand(ShowAddUserWindow, CanShowWindowOfAddUser);
            ShowEditUserWindowCommand = new RelayCommand(ShowEditUserWindow, CanShowWindowOfEditUser);
            ExportToCsvCommand = new RelayCommand(ExportToCsv, CanExportToCsv);
        }

        private bool CanShowWindowOfAddUser(object obj)
        {
            return true;
        }
        private void ShowAddUserWindow(object obj)
        {
            var mainWindow = obj as Window;

            UpsertUserView addUserWin = new UpsertUserView(_goRestClientService);
            addUserWin.Owner = mainWindow;
            addUserWin.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addUserWin.Title = "Add an User";
            addUserWin.LblUpsertAction.Content = "Add";
            addUserWin.LblDeleteAction.Visibility = Visibility.Hidden;
            addUserWin.Show();
            addUserWin.Closed += AddUserViewClosed;
        }

        private bool CanShowWindowOfEditUser(object obj)
        {
            return true;
        }
        private void ShowEditUserWindow(object obj)
        {
            var userDto = obj as UserDto;

            if (userDto != null)
            {
                UpsertUserView addUserWin = new UpsertUserView(_goRestClientService, userDto);
                addUserWin.Owner = _window;
                addUserWin.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addUserWin.Title = "Update an User";
                addUserWin.LblUpsertAction.Content = "Update";
                addUserWin.LblDeleteAction.Visibility = Visibility.Visible;
                addUserWin.Show();
                addUserWin.Closed += AddUserViewClosed;
            }
        }
        
        private bool CanExportToCsv(object obj)
        {
            return true;
        }
        private void ExportToCsv(object obj)
        {
            if(_window.UsersDataGrid.Items.Count > 0)
            {
                _exportToExcelService.ExportToExcel(_window.UsersDataGrid);
            }
        }

        private void AddUserViewClosed(object? sender, EventArgs e)
        {
            _window.UsersDataGrid.ItemsSource = _goRestClientService.GetAllUsersAsync(null, null, null).Result;
        }
    }
}
