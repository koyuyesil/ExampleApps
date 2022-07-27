namespace DesignPatternsCommand.Entities
{
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
}