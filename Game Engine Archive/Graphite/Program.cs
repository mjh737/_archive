
namespace Graphite
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Executive game = new Executive("Graphite Terrain Engine"))
            {
                game.Run();
            }
        }
    }
}

