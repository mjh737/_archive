using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta
{
    class Program
    {
        static void Main(string[] args)
        {
            DoIt("£10");
            Console.WriteLine("Done 1");
            DoIt("£5");
            Console.WriteLine("Done 2");
            Console.WriteLine("Press any key.");
            Console.ReadLine();
        }

        private static void DoIt(string amount)
        {
            // Create the people
            Person sophia = new Person("Sophia", "lonelyoutpost@outlook.com");
            Person india = new Person("India", "jenni.hewitt@southwark.anglican.org");
            Person john = new Person("John", "johnhewitt295@outlook.com");
            Person chris = new Person("Chris", "christinehewitt295@hotmail.com");
            //Person simon = new Person("Simon", "simon.parton@btopenworld.com");
            //Person olwen = new Person("Olwen", "olwenparton@hotmail.com");
            Person kate = new Person("Kate", "kate.hewitt@ubm.com");
            Person jenni = new Person("Jenni", "jenni.hewitt@southwark.anglican.org");
            Person matt = new Person("Matt", "lonelyoutpost@outlook.com");
            Person nan = new Person("Nan", "christinehewitt295@hotmail.com");

            // Create the pool from which Sophia's recipient is chosen
            List<Person> sophiaPool = new List<Person>();
            sophiaPool.Add(john);
            sophiaPool.Add(chris);
            //sophiaPool.Add(simon);
            //sophiaPool.Add(olwen);
            sophiaPool.Add(kate);
            sophiaPool.Add(jenni);
            sophiaPool.Add(nan);

            // Pick a random recipient from the pool
            int randomNumber = new Random().Next(0, sophiaPool.Count);
            sophia.BuysFor = sophiaPool[randomNumber];
            // Person A is going to be the person that Sophia buys for
            Person A = sophia.BuysFor;

            // Create the pool from which Sophia's recipient is chosen
            List<Person> indiaPool = new List<Person>();
            indiaPool.Add(john);
            indiaPool.Add(chris);
            //indiaPool.Add(simon);
            //indiaPool.Add(olwen);
            indiaPool.Add(kate);
            indiaPool.Add(matt);
            indiaPool.Add(nan);

            // but remove the person Sophia is buying for
            indiaPool.Remove(sophia.BuysFor);

            // Pick a random recipient from the pool
            randomNumber = new Random().Next(0, indiaPool.Count);
            india.BuysFor = indiaPool[randomNumber];

            // Person B is going to be the person that India buys for
            Person B = india.BuysFor;

            // Create a pool containing everyone except the kids
            List<Person> remainingPool = new List<Person>();
            remainingPool.Add(john);
            remainingPool.Add(chris);
            //remainingPool.Add(simon);
            //remainingPool.Add(olwen);
            remainingPool.Add(kate);
            remainingPool.Add(jenni);
            remainingPool.Add(matt);
            remainingPool.Add(nan);

            // But remove the two we have already assigned for A and B
            remainingPool.Remove(A);
            remainingPool.Remove(B);

            // Allocate C and remove them from the pool
            randomNumber = new Random().Next(0, remainingPool.Count);
            Person C = remainingPool[randomNumber];
            remainingPool.RemoveAt(randomNumber);

            // Allocate D and remove them from the pool
            randomNumber = new Random().Next(0, remainingPool.Count);
            Person D = remainingPool[randomNumber];
            remainingPool.RemoveAt(randomNumber);

            //// Allocate E and remove them from the pool
            //randomNumber = new Random().Next(0, remainingPool.Count);
            //Person E = remainingPool[randomNumber];
            //remainingPool.RemoveAt(randomNumber);

            //// Allocate F and remove them from the pool
            //randomNumber = new Random().Next(0, remainingPool.Count);
            //Person F = remainingPool[randomNumber];
            //remainingPool.RemoveAt(randomNumber);

            // Allocate G and remove them from the pool
            randomNumber = new Random().Next(0, remainingPool.Count);
            Person G = remainingPool[randomNumber];
            remainingPool.RemoveAt(randomNumber);

            // Allocate H
            Person H = remainingPool[0];

            // Apply the fixed connections
            A.BuysFor = C;
            B.BuysFor = D;
            C.BuysFor = G;
            D.BuysFor = H;
            //E.BuysFor = G;
            //F.BuysFor = H;
            G.BuysFor = sophia;
            H.BuysFor = india;


            Email.SendEmail("secret.santa@lapland.com", sophia.Email, "Secret Santa (" + amount + ")", "Dear " + sophia.Name + ".  Please buy a " + amount + " present for " + sophia.BuysFor.Name + ".  Thank you.  Santa");
            Email.SendEmail("secret.santa@lapland.com", india.Email, "Secret Santa (" + amount + ")", "Dear " + india.Name + ".  Please buy a " + amount + " present for " + india.BuysFor.Name + ".  Thank you.  Santa");
            //Email.SendEmail("secret.santa@lapland.com", simon.Email, "Secret Santa (" + amount + ")", "Dear " + simon.Name + ".  Please buy a " + amount + " present for " + simon.BuysFor.Name + ".  Thank you.  Santa");
            //Email.SendEmail("secret.santa@lapland.com", olwen.Email, "Secret Santa (" + amount + ")", "Dear " + olwen.Name + ".  Please buy a " + amount + " present for " + olwen.BuysFor.Name + ".  Thank you.  Santa");
            Email.SendEmail("secret.santa@lapland.com", john.Email, "Secret Santa (" + amount + ")", "Dear " + john.Name + ".  Please buy a " + amount + " present for " + john.BuysFor.Name + ".  Thank you.  Santa");
            Email.SendEmail("secret.santa@lapland.com", chris.Email, "Secret Santa (" + amount + ")", "Dear " + chris.Name + ".  Please buy a " + amount + " present for " + chris.BuysFor.Name + ".  Thank you.  Santa");
            Email.SendEmail("secret.santa@lapland.com", kate.Email, "Secret Santa (" + amount + ")", "Dear " + kate.Name + ".  Please buy a " + amount + " present for " + kate.BuysFor.Name + ".  Thank you.  Santa");
            Email.SendEmail("secret.santa@lapland.com", matt.Email, "Secret Santa (" + amount + ")", "Dear " + matt.Name + ".  Please buy a " + amount + " present for " + matt.BuysFor.Name + ".  Thank you.  Santa");
            Email.SendEmail("secret.santa@lapland.com", jenni.Email, "Secret Santa (" + amount + ")", "Dear " + jenni.Name + ".  Please buy a " + amount + " present for " + jenni.BuysFor.Name + ".  Thank you.  Santa");
            Email.SendEmail("secret.santa@lapland.com", nan.Email, "Secret Santa (" + amount + ")", "Dear " + nan.Name + ".  Please buy a " + amount + " present for " + sophia.BuysFor.Name + ".  Thank you.  Santa");

            Email.SendEmail("secret.santa@lapland.com", "ofey@outlook.com", "Secret Santa (" + amount + ")",
            "Sophia (matt) buys for " + sophia.BuysFor.Name + "\n\n" + Environment.NewLine +
            "India (jenni) buys for " + india.BuysFor.Name + Environment.NewLine +
            //"Simon buys for " + simon.BuysFor.Name + Environment.NewLine +
            //"Olwen buys for " + olwen.BuysFor.Name + Environment.NewLine +
            "John buys for " + john.BuysFor.Name + Environment.NewLine +
            "Chris buys for " + chris.BuysFor.Name + Environment.NewLine +
            "Kate buys for " + kate.BuysFor.Name + Environment.NewLine +
            "Matt buys for " + matt.BuysFor.Name + Environment.NewLine +
            "Jenni buys for " + jenni.BuysFor.Name + Environment.NewLine +
            "Nan buys for " + nan.BuysFor.Name + Environment.NewLine);
        }
    }
}
