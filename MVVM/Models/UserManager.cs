using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Models
{
	public class UserManager
	{
		public static ObservableCollection<UserDto> _DatabaseUsers = new ObservableCollection<UserDto>() { new UserDto() { Email = "quam@aol.couk", Name = "Erich Fry" }, new UserDto { Email = "in@yahoo.com", Name = "Virginia Salas" }, new UserDto { Email = "luctus@google.com", Name = "Alvin Velasquez" }, new UserDto { Email = "etiam.bibendum@hotmail.edu", Name = "Zia Hutchinson" }, new UserDto { Email = "augue.porttitor@protonmail.org", Name = "Justin Burch" } };

		public static ObservableCollection<UserDto> GetUsers()
		{
			return _DatabaseUsers;
		}

		public static void AddUser(UserDto user)
		{
			_DatabaseUsers.Add(user);
		}
	}
}
