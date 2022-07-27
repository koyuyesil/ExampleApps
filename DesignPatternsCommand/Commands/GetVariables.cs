using DesignPatternsCommand.Entities;
using System;

namespace DesignPatternsCommand.Commands
{
    public class GetVariables : ICommand
    {
        Android device;
        public GetVariables(Android dev)
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