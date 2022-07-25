using System;

namespace StarArisingBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Startup().RunAsync().GetAwaiter().GetResult();
        }
    }
}
