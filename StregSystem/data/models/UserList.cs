using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    class UserList
    {
        public List<User> users = new List<User>(); 
        public void OnNewUser(object source, UserArgs e)
        {
            users.Add(e.user);
        }
    }
}
