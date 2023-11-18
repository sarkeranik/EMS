using EMS.Core.Commands;
using EMS.Models;
using EMS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace EMS.ViewModel
{
    public class AddUserViewModel
    {
        private readonly IGoRestClientService _goRestClientService;

        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Status { get; set; }

        public List<string> Genders { get; set; } = new List<string>() { "Male", "Female", "Other" };
        public List<string> Statuses { get; set; } = new List<string>() { "Active", "Inactive" };

        public ICommand AddUserCommand { get; set; }

        public AddUserViewModel(IGoRestClientService goRestClientService)
        {
            _goRestClientService = goRestClientService;
            AddUserCommand = new RelayCommand(AddUser, CanAddUser);
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
            var res = _goRestClientService.AddUserAsync(user).Result;

        }
    }
}
