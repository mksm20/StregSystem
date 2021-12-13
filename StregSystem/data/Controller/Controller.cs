using StregSystem.data.models;
using StregSystem.data.views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.Controller
{
    public class Controller
    {
        public Controller(Stregsystem stregSystem, StregsystemCLI ui)
        {
            _stregsystem = stregSystem;
            _ui = ui;
            _ui.CommandParse += OnCommandParse;
            initiateController();
        }
        private Dictionary<string, Delegate> adminCommands = new Dictionary<string, Delegate>();
        private Stregsystem _stregsystem;
        private StregsystemCLI _ui;
        private void initiateController()
        {
            adminCommands[":quit"] = new Action(_ui.Close);
            adminCommands[":activate"] = new Action<int>(_stregsystem.ActivateProduct);
            adminCommands[":deactivate"] = new Action<int>(_stregsystem.DeactivateProduct);
            adminCommands[":crediton"] = new Action<int>(_stregsystem.CreditOnOff);
            adminCommands[":creditoff"] = new Action<int>(_stregsystem.CreditOnOff);
            adminCommands[":addcredits"] = new Func<string, double,Transaction>(_stregsystem.AddCreditsToAccount);
        }
        public void OnCommandParse(object source, CommandArgs e)
        {
            CommandParser(e.Command);
        }
        public void CommandParser(string command)
        {
            string[] commandArr = command.Split(" ");
            if (commandArr[0].StartsWith(":"))
            {
                switch (commandArr[0])
                {
                    case ":quit":
                        _ui.Close();
                        break;
                    case ":activate":
                        _stregsystem.ActivateProduct(int.Parse(commandArr[1]));
                        break;
                    case ":deactivate":
                        _stregsystem.DeactivateProduct(int.Parse(commandArr[1]));
                        break;
                    case ":crediton":
                    case ":credioff":
                        _stregsystem.CreditOnOff(int.Parse(commandArr[1]));
                        break;
                    case ":addcredits":
                        _stregsystem.AddCreditsToAccount(commandArr[1], double.Parse(commandArr[2]));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                
                try
                {
                    User user = _stregsystem.GetUserByUsername(commandArr[0]);
                    for (int i = 1; i < commandArr.Length; i++)
                    {
                        if (commandArr.Length == 1)
                        {
                            _ui.DisplayUserInfo(user);
                        }
                        else
                        {
                            Product temp = _stregsystem.GetProductByID(int.Parse(commandArr[i]));
                            _stregsystem.BuyProduct(user, temp);
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    throw;
                }
            }
        }

    }
}
