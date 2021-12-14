using System;
using System.Collections.Generic;
using StregSystem.data.Controller;
using StregSystem.data.models;
using StregSystem.data.views;

namespace StregSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            IStregsystem stregsystem = new Stregsystem();
            IStregsystemCLI ui = new StregsystemCLI(stregsystem);
            Controller sc = new Controller(stregsystem, ui);
            ui.Start();
        }
    }
}
