using StregSystem.data.models;
using StregSystem.data.views;
using System;
using System.Collections.Generic;
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

            Console.WriteLine(command);
        }

    }
}
