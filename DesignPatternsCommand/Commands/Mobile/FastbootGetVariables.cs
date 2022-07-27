using DesignPatternsCommand.Entities;
using System;

namespace DesignPatternsCommand.Commands
{
    public class FastbootGetVariables : ICommand
    {
        Android device;
        public FastbootGetVariables(Android dev)
        {
            device = dev;
        }
        public void Execute()
        {
            device.GetVariables();
            Console.WriteLine("Batarya durumu: "+ device.GetStatus());
        }
        public override string ToString()
        {
            return "Get Variables";
        }
    }

}