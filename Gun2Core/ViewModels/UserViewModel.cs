using Gun2Core.Models;
using Gun2Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gun2Core.ViewModels
{
    public class UserViewModel : ViewModel
    {

        public UserViewModel()
        {
            List<User> _users = new List<User>();
            Title = "Пользователи";
            for (int i = 0; i < 50; i++)
            {
                User user = new User
                {
                    FirstName = $"Name{i}",
                    MidName = $"MId{i}",
                    LastName = $"Last{i}",
                    Department = $"Dep{i % 10}",
                    IsDeveloper = i % 7 == 0
                };
                _users.Add(user);
            }
            Users = _users;
        }

        public List<User> Users
        {
            get { return _Users; }
            private set { Set(ref _Users, value); }
        }

        private List<User> _Users;

    }
}
