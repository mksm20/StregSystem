using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    class User : IComparable<User>
    {
        private int _ID;
        private List<string> _firstName;
        private string _lastName;
        private string _userName;
        private string _email;
        private double _balance;

        public int ID { get; private set; }
        public List<string> FirstName { get; private set; }
        public string UserName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public double Balance { get; private set; }
        private void setID()
        {
            //Open CSV with current members, read last member number + Add one
            //To that, save as ID
        } 
        private void setUserName(string UserNameInp)
        {
            if(Regex.IsMatch(UserNameInp, @"^[A-Za-z0-9_]$"))
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
            if(Regex.IsMatch(EmailSet, @"^[A-Za-z0-9._%+-]+@[^-.][A-Za-z0-9]{1}[A-Za-z0-9.-]+[^-.][A-Za-z0-9]{1}\.[A-Za-z0-9]{2,4}$ "))
            {
                _email = EmailSet;
                Email = _email;
            }
            else
            {
                throw new ArgumentException("Email does not match requirements");
            }
        }

        public void setName(List<String> Name)
        {
            bool HasAName = false;
            for (int i = 0; i < Name.Count - 1; i++)
            {
                if (Name[i] != "" && Regex.IsMatch(Name[i], @"^[a-zA-Z]+$"))
                {
                    _firstName.Add(Name[^1]);
                    HasAName = true;
                }
            }
            
            if(Name[^1] != "" && Regex.IsMatch(Name[^1], @"^[a-zA-Z]+$"))
            {
                _lastName = Name[^1];
                LastName = _lastName;
            }
            else
            {
                HasAName = false;
            }
            
            if (HasAName)
            {
                FirstName = _firstName;
            }
            else
            {
                throw new ArgumentException("Not a correct name input");
            }
        }

        public void setIncBalance(double IncTransAct)
        {

                if (IncTransAct > 0)
                {
                    _balance += IncTransAct;
                    Balance = _balance;
                }
                else
                {
                    throw new ArgumentException("Incoming transaction cannot be less than 0 :");
                }
        }
        public void setOutBalance(double OutTransAct)
        {
            {


                if (OutTransAct > 0 && OutTransAct <= Balance)
                {
                    _balance -= OutTransAct;
                    Balance = _balance;
                }
                else
                {
                    throw new ArgumentException("Outgoing transaction cannot be negative or 0 :");
                }

            }

        }
        public int CompareTo(User that)
        {
            if (this.ID < that.ID) return -1;
            if (this.ID == that.ID) return 0;
            return 1;
        }

    }
}
