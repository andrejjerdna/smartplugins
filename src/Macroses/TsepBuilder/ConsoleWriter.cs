namespace TsepBuilder
{
    internal class ConsoleWriter
    {
        public static void WriteData(string startMessage, IEnumerable<string> data, ConsoleColor consoleColor = ConsoleColor.White)
        {
            var currentColor = Console.ForegroundColor;

            Console.WriteLine(string.Empty);
            Console.WriteLine(startMessage);
            Console.WriteLine(string.Empty);

            Console.ForegroundColor = consoleColor;

            foreach(var item in data)
                Console.WriteLine(item);

            Console.ForegroundColor = currentColor;
        }
    }
}
