using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationService.Repository
{
    public class UserRepository
    {
        public List<User> Users;

        public UserRepository()
        {
            // To do : Move this to a users table in SQL
            Users = new List<User>();
            Users.Add(new User() { Username = "User1", Password = "Pass1" });
            Users.Add(new User() { Username = "Admin", Password = "Pass2" });
        }

        public User GetUser(string username)
        {
            try
            {
                return Users.First(user => user.Username.Equals(username));
            }
            catch
            {
                return null;
            }
        }
    }
}