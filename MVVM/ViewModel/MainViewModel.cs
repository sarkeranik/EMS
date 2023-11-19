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

        private List<UserDto> Users { get; set; } = new List<UserDto>();
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ShowAddUserWindowCommand { get; set; }
        public ICommand ShowEditUserWindowCommand { get; set; }
        public ICommand ExportToCsvCommand { get; set; }
        public ICommand NumbersOfPagesSelectionChangedCommand { get; set; }
        public ICommand SearchTextChangedCommand { get; set; }

        public MainViewModel(IGoRestClientService goRestClientService, Window window, IExportToExcelService exportToExcelService)
        {
            _goRestClientService = goRestClientService;
            _window = (MainView)window;
            _exportToExcelService = exportToExcelService;
            _window.UsersDataGrid.ItemsSource = _goRestClientService.GetAllUsersAsync(null, null, null).Result;

            ShowAddUserWindowCommand = new RelayCommand(ShowAddUserWindow, CanShowWindowOfAddUser);
            ShowEditUserWindowCommand = new RelayCommand(ShowEditUserWindow, CanShowWindowOfEditUser);
            ExportToCsvCommand = new RelayCommand(ExportToCsv, CanExportToCsv);
            NumbersOfPagesSelectionChangedCommand = new RelayCommand(NumbersOfPagesSelectionChanged, (s) => true);
            SearchTextChangedCommand = new RelayCommand(SearchTextChanged, (s) => true);

            NextCommand = new RelayCommand(NextPage, (s) => true);
            PreviousCommand = new RelayCommand(PreviousPage, (s) => true);
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
            addUserWin.Title = "Add Employee";
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
                addUserWin.Title = "Update Employee";
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
            if (_window.UsersDataGrid.Items.Count > 0)
            {
                _exportToExcelService.ExportToExcel(_window.UsersDataGrid);
            }
        }
        private void SearchTextChanged(object obj)
        {
            var recordsToShow = _goRestClientService.GetAllUsersAsync(_window.FilterTextBox.Text, CurrentPage.ToString(), _selectedRecord.ToString()).Result;
            UpdateCollection(recordsToShow);
            UpdateEnableState();
        }


        private void AddUserViewClosed(object? sender, EventArgs e)
        {
            var recordsToShow = _goRestClientService.GetAllUsersAsync(null, CurrentPage.ToString(), _selectedRecord.ToString()).Result;

            UpdateCollection(recordsToShow);
        }

        private void UpdateCollection(IEnumerable<UserDto> users)
        {
            _window.UsersDataGrid.ItemsSource = users;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Pagination

        public List<int> DDNumbersOfPages { get; set; } = new List<int>() { 5, 10, 20, 30 };

        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }

        private int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
                UpdateEnableState();
            }
        }

        private int _selectedRecord = 10;
        public int SelectedRecord
        {
            get { return _selectedRecord; }
            set
            {
                _selectedRecord = value;
                OnPropertyChanged(nameof(CurrentPage));
                UpdateRecordCount();
            }
        }

        private int _numberOfPages = 20;
        public int NumberOfPages
        {
            get { return _numberOfPages; }
            set
            {
                _numberOfPages = value;
                OnPropertyChanged(nameof(NumberOfPages));
                UpdateEnableState();
            }
        }

        private bool _isPreviousEnabled;
        public bool IsPreviousEnabled
        {
            get { return _isPreviousEnabled; }
            set
            {
                _isPreviousEnabled = value;
                OnPropertyChanged(nameof(IsPreviousEnabled));
            }
        }

        private bool _isNextEnabled;
        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set
            {
                _isNextEnabled = value;
                OnPropertyChanged(nameof(IsNextEnabled));
            }
        }

        private void NumbersOfPagesSelectionChanged(object obj)
        {
            var recordsToShow = _goRestClientService.GetAllUsersAsync(null, CurrentPage.ToString(), _selectedRecord.ToString()).Result;
            UpdateCollection(recordsToShow);
            UpdateEnableState();
        }
        private void PreviousPage(object obj)
        {
            CurrentPage--;

            var recordsToShow = _goRestClientService.GetAllUsersAsync(null, CurrentPage.ToString(), _selectedRecord.ToString()).Result;
            UpdateCollection(recordsToShow);
            UpdateEnableState();
        }
        private void NextPage(object obj)
        {
            CurrentPage++;

            var recordsToShow = _goRestClientService.GetAllUsersAsync(null, CurrentPage.ToString(), _selectedRecord.ToString()).Result;
            UpdateCollection(recordsToShow);
            UpdateEnableState();
        }
        private void UpdateEnableState()
        {
            _window.BtnPreviousPage.Visibility = CurrentPage > 1 ? Visibility.Visible : Visibility.Hidden;
            _window.BtnNextPage.Visibility = CurrentPage < NumberOfPages ? Visibility.Visible : Visibility.Hidden;
        }
        private void UpdateRecordCount()
        {
            //NumberOfPages = (int)Math.Ceiling((double)Users.Count / SelectedRecord);
            //NumberOfPages = NumberOfPages == 0 ? 1 : NumberOfPages;
            //UpdateCollection(Users.Take(SelectedRecord));
            CurrentPage = 1;
        }
        #endregion
    }
}
