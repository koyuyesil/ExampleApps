using DesignPatternsCommand.Entities;
using System;

namespace DesignPatternsCommand.Commands
{
    public class NextChannel : ICommand
    {
        Television tv;
        public NextChannel(Television t)
        {
            tv = t;
        }
        public void Execute()
        {
            tv.NextChannel();
            Console.WriteLine("Current Channel:" + tv.CurrentChannel());
        }
        public override string ToString()
        {
            return "Next Channel";
        }
    }
}