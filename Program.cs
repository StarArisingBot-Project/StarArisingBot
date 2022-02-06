namespace SAB.System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StartBot bot = new StartBot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
