using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatternsCommand
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu m = new Menu();
            SwitchOnOff cmd_switch = new SwitchOnOff(new Switch());
            Television tv = new Television();
            PreviousChannel cmd_prev_ch = new PreviousChannel(tv);
            NextChannel cmd_next_ch = new NextChannel(tv);
            m.AddCommand(cmd_switch);
            m.AddCommand(cmd_prev_ch);
            m.AddCommand(cmd_next_ch);
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
                Thread.Sleep(5000);
                //Task.Delay(5000);//await için
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}


public class Switch
{
    private int sw;
    public Switch()
    {
        sw = 0;
    }
    public void SwitchOn()
    {
        sw = 1;
    }
    public void SwitchOff()
    {
        sw = 0;
    }
    public int SwitchStatus() { return sw; }
}


public class Television
{
    private int channel;
    public Television()
    {
        channel = 1;
    }
    public void NextChannel()
    {
        channel++;
    }
    public void PreviousChannel()
    {
        channel--;
    }
    public int CurrentChannel() { return channel; }
}

public interface ICommand
{
    void Execute();
}



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

public class PreviousChannel : ICommand
{
    Television tv;
    public PreviousChannel(Television t)
    {
        tv = t;
    }
    public void Execute()
    {
        tv.PreviousChannel();
        Console.WriteLine("Current Channel:" + tv.CurrentChannel());
    }
    public override string ToString()
    {
        return "Previous Channel";
    }
}


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