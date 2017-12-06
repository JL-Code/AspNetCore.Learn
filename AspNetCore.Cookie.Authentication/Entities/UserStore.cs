using System.Collections.Generic;
using System.Linq;

namespace AspNetCore.Cookie.Authentication.Entities
{
    public class UserStore
    {
        private static List<User> _users = new List<User>() {
            new User {  Id=1, Name="admin", Password="1", Email="alice@gmail.com", PhoneNumber="18800000001" },
            new User {  Id=2, Name="jiangy", Password="1", Email="bob@gmail.com", PhoneNumber="18800000002" }
        };

        public User FindUser(string userName, string password)
        {
            return _users.FirstOrDefault(_ => _.Name == userName && _.Password == password);
        }
    }
}
