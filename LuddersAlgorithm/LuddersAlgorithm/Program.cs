using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuddersAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            // repeat ad infinitum
            do {
                DoIt();
                Console.ReadLine();
            }
            while (true);
        }

        private static void DoIt()
        {
            // Create the people
            Person sophia = new Person("sophia");
            Person india = new Person("india");
            Person john = new Person("john");
            Person chris = new Person("chris");
            Person simon = new Person("simon");
            Person olwen = new Person("olwen");
            Person kate = new Person("kate");
            Person jenni = new Person("jenni");
            Person matt = new Person("matt");

            // Create the pool from which Sophia's recipient is chosen
            List<Person> sophiaPool = new List<Person>();
            sophiaPool.Add(john);
            sophiaPool.Add(chris);
            sophiaPool.Add(simon);
            sophiaPool.Add(olwen);
            sophiaPool.Add(kate);
            sophiaPool.Add(jenni);

            // Pick a random recipient from the pool
            int randomNumber = new Random().Next(0, 6);
            sophia.BuysFor = sophiaPool[randomNumber];
            // Person A is going to be the person that Sophia buys for
            Person A = sophia.BuysFor;

            // Create the pool from which Sophia's recipient is chosen
            List<Person> indiaPool = new List<Person>();
            indiaPool.Add(john);
            indiaPool.Add(chris);
            indiaPool.Add(simon);
            indiaPool.Add(olwen);
            indiaPool.Add(kate);
            indiaPool.Add(matt);

            // but remove the person Sophia is buying for
            indiaPool.Remove(sophia.BuysFor);

            // Pick a random recipient from the pool
            randomNumber = new Random().Next(0, 5);
            india.BuysFor = indiaPool[randomNumber];

            // Person B is going to be the person that India buys for
            Person B = india.BuysFor;

            // Create a pool containing everyone except the kids
            List<Person> remainingPool = new List<Person>();
            remainingPool.Add(john);
            remainingPool.Add(chris);
            remainingPool.Add(simon);
            remainingPool.Add(olwen);
            remainingPool.Add(kate);
            remainingPool.Add(jenni);
            remainingPool.Add(matt);

            // But remove the two we have already assigned for A and B
            remainingPool.Remove(A);
            remainingPool.Remove(B);

            // Allocate C and remove them from the pool
            randomNumber = new Random().Next(0, 5);
            Person C = remainingPool[randomNumber];
            remainingPool.RemoveAt(randomNumber);

            // Allocate D and remove them from the pool
            randomNumber = new Random().Next(0, 4);
            Person D = remainingPool[randomNumber];
            remainingPool.RemoveAt(randomNumber);

            // Allocate E and remove them from the pool
            randomNumber = new Random().Next(0, 3);
            Person E = remainingPool[randomNumber];
            remainingPool.RemoveAt(randomNumber);

            // Allocate F and remove them from the pool
            randomNumber = new Random().Next(0, 2);
            Person F = remainingPool[randomNumber];
            remainingPool.RemoveAt(randomNumber);

            // Allocate G
            Person G = remainingPool[0];

            // Apply the fixed connections
            A.BuysFor = C;
            B.BuysFor = D;
            C.BuysFor = E;
            D.BuysFor = F;
            E.BuysFor = G;
            F.BuysFor = sophia;
            G.BuysFor = india;

            // Display results and hope for the best
            Console.WriteLine("sophia (matt) buys for " + sophia.BuysFor.Name);
            Console.WriteLine("india (jenni) buys for " + india.BuysFor.Name);
            Console.WriteLine("simon buys for " + simon.BuysFor.Name);
            Console.WriteLine("olwen buys for " + olwen.BuysFor.Name);
            Console.WriteLine("john buys for " + john.BuysFor.Name);
            Console.WriteLine("chris buys for " + chris.BuysFor.Name);
            Console.WriteLine("kate buys for " + kate.BuysFor.Name);
            Console.WriteLine("matt buys for " + matt.BuysFor.Name);
            Console.WriteLine("jenni buys for " + jenni.BuysFor.Name);

            // Be slightly miffed the Simon cracked it
        }
    }
}
