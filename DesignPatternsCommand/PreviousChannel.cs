using System;

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
