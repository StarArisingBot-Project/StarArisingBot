using System;

namespace SAB.System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Startup bot = new Startup();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
