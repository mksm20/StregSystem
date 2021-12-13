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
        }
        private Stregsystem _stregsystem;
        private StregsystemCLI _ui;
        public void CommandParser()
        {

        }

    }
}
