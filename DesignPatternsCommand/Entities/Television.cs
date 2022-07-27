using System.Text.RegularExpressions;

namespace DesignPatternsCommand.Entities
{
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
}