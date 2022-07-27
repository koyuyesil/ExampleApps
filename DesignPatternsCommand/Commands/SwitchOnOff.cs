using DesignPatternsCommand.Entities;
using System;

namespace DesignPatternsCommand.Commands
{
    public class SwitchOnOff : ICommand
    {
        Switch lamba;
        public SwitchOnOff(Switch l)
        {
            lamba = l;
        }
        public void Execute()
        {
            if (lamba.SwitchStatus() == 1)
            {
                lamba.SwitchOff();
                Console.WriteLine("Switch is off.");
            }
            else
            {
                lamba.SwitchOn();
                Console.WriteLine("Switch is on.");
            }
        }
        public override string ToString()
        {
            if (lamba.SwitchStatus() == 1) return "Turn off switch.";
            else return "Turn on switch";
        }
    }
}