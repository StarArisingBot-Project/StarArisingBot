namespace StarArisingBot
{
    internal static class Program
    {
        public static void Main()
        {
            new Startup().RunAsync().GetAwaiter().GetResult();
        }
    }
}
