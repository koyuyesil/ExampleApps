using DesignPatternsCommand.Entities;
using System;

namespace DesignPatternsCommand.Commands
{
    public class FastbootReboot : ICommand
    {
        Android device;
        public FastbootReboot(Android dev)
        {
            device = dev;
        }
        public void Execute()
        {
            device.Reboot();
            Console.WriteLine("reboot: " + device.GetStatus());
        }
        public override string ToString()
        {
            return "Fastboot Reboot";
        }
    }

}