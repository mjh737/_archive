namespace Terminal
{
    using System.Data;

    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("sdb> ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    continue;
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;
                try
                {
                    var result = EvaluateExpression(input);
                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static string EvaluateExpression(string input)
        {
            string firstWord = input.Split(' ')[0];
            string theRest = input.Substring(firstWord.Length).Trim();

            switch (firstWord.ToUpper())
            {
                case Statements.SELECT:
                    return SelectStatement(theRest);
                case Statements.INSERT:
                    return InsertStatement(theRest);
                default:
                    throw new InvalidOperationException("Unknown command");
            }
        }

        private static string SelectStatement(string theRest)
        {
            Console.WriteLine("A SELECT statement");
            return "SELECT executed";
        }

        private static string InsertStatement(string theRest)
        {
            Console.WriteLine("An INSERT statement");
            return "INSERT executed";
        }
    }
}
