using System;

namespace SAB.System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Startup().RunAsync().GetAwaiter().GetResult();
        }
    }
}
