using EMS.Commands;
using EMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EMS.ViewModel
{
	public class AddUserViewModel
	{

		public ICommand AddUserCommand { get; set; }

		public string? Name { get; set; }
		public string? Email { get; set; }

		public AddUserViewModel()
		{
			AddUserCommand = new RelayCommand(AddUser, CanAddUser);


		}

		private bool CanAddUser(object obj)
		{
			return true;
		}

		private void AddUser(object obj)
		{

            UserManager.AddUser(new Models.UserDto() { Name = Name, Email = Email });

		}
	}
}
