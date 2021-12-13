using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;

namespace StregSystem.data.models
{
    public class User : IComparable<User>
    {
        private List<string> _firstName = new List<string>();
        private string _lastName;
        private string _userName;
        private string _email;
        private double _balance;

        public User(int? ID, string firstName, string lastName, string userName, string email, double balance)
        {
            setUserName(userName);
            setName(firstName, lastName);
            setEmail(email);
            setIncBalance(balance);
            if (ID == null)
            {
                setID();
            }
            else
            {
                this.ID = (int)ID;
            }
            OnNewUser();
        }

        public int ID { get; set; }
        public string FirstName { get; private set; }
        public string UserName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public double Balance { get; private set; }
        public List<Transaction> transactions = new List<Transaction>();
        public delegate void NewUserAddedEventHandler(object source, UserArgs args);
        public delegate void BalanceLowEventHandler(object source, UserArgs args);
        public event BalanceLowEventHandler LowBalance;
        public event NewUserAddedEventHandler NewUser;
        protected virtual void OnNewUser()
        {
            if (NewUser != null) NewUser(this, new UserArgs() { user = this });
        }
        protected virtual void OnLowBalance()
        {
            if (LowBalance != null) LowBalance(this, new UserArgs() { user = this });
        }
        private void setID()
        {
            List<User> temp = new List<User>();
            string path = "../../../files/users.Json";
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
                temp = users;
                this.ID = users[^1].ID + 1;
            }
            using (StreamWriter w = new StreamWriter(path))
            {
                string json = "[";
                foreach(User user in temp)
                {
                    json += System.Text.Json.JsonSerializer.Serialize(user);
                    json += ",";
                }
                json += System.Text.Json.JsonSerializer.Serialize(this);
                json += "]";
                w.Write(json);
            }
        } 
        private void setUserName(string UserNameInp)
        {
            if(Regex.IsMatch(UserNameInp, @"^[A-Za-z0-9_]+$"))
            {
                _userName = UserNameInp;
                UserName = _userName;
            }
            else
            {
                throw new ArgumentException("Username Does not match Specifications");
            }
        }
        public void setEmail(string EmailSet)
        {
            if(Regex.IsMatch(EmailSet, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                _email = EmailSet;
                Email = _email;
            }
            else
            {
                throw new ArgumentException("Email does not match requirements");
            }
        }

        public void setName(string firstName, string lastName)
        {
            bool HasAName = false;
           
            
            if (firstName != null && Regex.IsMatch(firstName, @"^[a-zA-Z]+$"))
            {
                _firstName.Add(firstName);
                HasAName = true;
            }
            if(lastName != null && Regex.IsMatch(lastName, @"^[a-zA-Z]+$"))
            {
                _lastName = lastName;
                LastName = _lastName;
            }
            else
            {
                HasAName = false;
            }
            
            if (HasAName)
            {
                FirstName = _firstName[0];
            }
            else
            {
                throw new ArgumentException("Not a correct name input");
            }
        }

        public void setIncBalance(double IncTransAct)
        { 
            _balance += IncTransAct;
            Balance = _balance;
        }
        public void setOutBalance(double OutTransAct)
        {
            {
                if(Balance < 50)
                {
                    OnLowBalance();
                }

                if (OutTransAct > 0 && OutTransAct <= Balance)
                {
                    _balance -= OutTransAct;
                    Balance = _balance;
                }
                else
                {
                    throw new InsufficientCreditsException("Outgoing transaction cannot be negative or 0 :");
                }

            }

        }
        public int CompareTo(User that)
        {
            if (this.ID < that.ID) return -1;
            if (this.ID == that.ID) return 0;
            return 1;
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   EqualityComparer<List<string>>.Default.Equals(_firstName, user._firstName) &&
                   _lastName == user._lastName &&
                   _userName == user._userName &&
                   _email == user._email &&
                   _balance == user._balance &&
                   ID == user.ID &&
                   FirstName == user.FirstName &&
                   UserName == user.UserName &&
                   LastName == user.LastName &&
                   Email == user.Email &&
                   Balance == user.Balance;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(_firstName);
            hash.Add(_lastName);
            hash.Add(_userName);
            hash.Add(_email);
            hash.Add(_balance);
            hash.Add(ID);
            hash.Add(FirstName);
            hash.Add(UserName);
            hash.Add(LastName);
            hash.Add(Email);
            hash.Add(Balance);
            return hash.ToHashCode();
        }
    }
}
