using EMS.Core.Commands;
using EMS.Models;
using EMS.MVVM.Models;
using EMS.Services;
using EMS.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EMS.ViewModel
{
    public class UpsertUserViewModel
    {
        private readonly IGoRestClientService _goRestClientService;
        private readonly Window _window;

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Status { get; set; }

        public List<string> Genders { get; set; } = new List<string>() { "Male", "Female" };
        public List<string> Statuses { get; set; } = new List<string>() { "Active", "Inactive" };

        public ICommand UpsertUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }

        public UpsertUserViewModel(IGoRestClientService goRestClientService, Window window, UserDto userDto = null)
        {
            _goRestClientService = goRestClientService;
            _window = window;

            if (userDto != null)
            {
                UpsertUserCommand = new RelayCommand(EditUser, CanEditUser);
                DeleteUserCommand = new RelayCommand(DeleteUser, CanDeleteUser);
                this.Id = userDto.Id;
                this.Name = userDto.Name;
                this.Email = userDto.Email;
                this.Gender = userDto.Gender == "male" ? "Male" : "Female";
                this.Status = userDto.Status == "active" ? "Active" : "Inactive";
            }
            else
            {
                UpsertUserCommand = new RelayCommand(AddUser, CanAddUser);
            }
        }

        private bool CanAddUser(object obj)
        {
            return true;
        }
        private void AddUser(object obj)
        {
            var user = new UserForCreationDto()
            {
                Name = Name,
                Email = Email,
                Gender = Gender,
                Status = Status
            };

            try
            {
                var res = _goRestClientService.AddUserAsync(user).Result;

                string messageBoxText = $"{Name} User Added Successfully";
                string caption = "Message";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                MessageBox.Show(messageBoxText, caption, button, icon);
                _window.Close();
            }
            catch (ApiException ex)
            {
                string messageBoxText = ex.Message;
                string caption = "Message";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            catch (Exception ex)
            {
                string messageBoxText = ex?.Message;
                string caption = "Message";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;

                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }

        private bool CanEditUser(object obj)
        {
            return true;
        }
        private void EditUser(object obj)
        {
            var user = new UserForCreationDto()
            {
                Name = Name,
                Email = Email,
                Gender = Gender,
                Status = Status
            };

            try
            {
                var res = _goRestClientService.UpdateUserAsync(Id, user).Result;

                string messageBoxText = $"{Name} User Updated Successfully";
                string caption = "Message";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                MessageBox.Show(messageBoxText, caption, button, icon);
                _window.Close();
            }
            catch (ApiException ex)
            {
                string messageBoxText = ex.Message;
                string caption = "Message";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            catch (Exception ex)
            {
                string messageBoxText = ex?.Message;
                string caption = "Message";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;

                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }

        private bool CanDeleteUser(object obj)
        {
            return true;
        }
        private void DeleteUser(object obj)
        {
            string caption = "Message";

            try
            {
                string confirmMessageBoxText = $"Are you sure you want to delete '{Name}'?";
                string confirmCaption = "Message";

                var result = MessageBox.Show(confirmMessageBoxText, confirmCaption, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Cancel);

                if(result == MessageBoxResult.Yes)
                {
                    var res = _goRestClientService.DeleteUserAsync(Id);

                    string messageBoxText = $"{Name} User Deleted Successfully";

                    MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Information);
                    _window.Close();
                }
            }
            catch (ApiException ex)
            {
                string messageBoxText = ex.Message;

                MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                string messageBoxText = ex?.Message;

                MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
