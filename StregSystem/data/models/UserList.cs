using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    public class UserList
    {
        public UserList()
        {
            getUsers();
        }
        public List<User> users = new List<User>();
        private string _path = "../../../files/users.Json";
        private void getUsers()
        {
            using (StreamReader r = new StreamReader(_path))
            {
                string json = r.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }
        }
    }
}
