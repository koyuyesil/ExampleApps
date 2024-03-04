using DesignPatternsCommand.Commands;
using DesignPatternsCommand.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DesignPatternsCommand
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Menu m = new Menu();
            SwitchOnOff cmd_switch = new SwitchOnOff(new Switch());

            Television tv = new Television();
            PreviousChannel cmd_prev_ch = new PreviousChannel(tv);
            NextChannel cmd_next_ch = new NextChannel(tv);

            Android dev = new Android();
            FastbootGetVariables getVariables = new FastbootGetVariables(dev);
            FastbootReboot reboot = new FastbootReboot(dev);

            m.AddCommand(cmd_switch);
            m.AddCommand(cmd_prev_ch);
            m.AddCommand(cmd_next_ch);
            m.AddCommand(getVariables);
            m.AddCommand(reboot);
            m.CreateMenu();
        }
    }
}

public class Menu
{
    private List<ICommand> commands;

    public Menu()
    {
        commands = new List<ICommand>();
    }

    public void AddCommand(ICommand c)
    {
        commands.Add(c);
    }

    public void CreateMenu()
    {
        int s = 0;
        while (s != -1)
        {
            Console.Clear();
            for (int i = 0; i < commands.Count(); i++)
            {
                Console.WriteLine(i + ":" + commands[i]);
            }
            Console.WriteLine("-1: For exit.");

            try
            {
                s = int.Parse(Console.ReadLine());
                commands[s].Execute();
                Thread.Sleep(2000);
                //Task.Delay(2000);//await için
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(2000);
            }
        }
    }
}