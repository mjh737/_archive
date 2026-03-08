using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Xmas2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> buyers = new List<Person>();
            List<Person> bigReceivers = new List<Person>();
            List<Person> littleReceivers = new List<Person>();

            buyers = LoadPeople();
            bigReceivers = LoadPeople();
            littleReceivers = LoadPeople();

            bool isGood = true;

            do
            {
                for (int n = 1; n <= 7; n++)
                {
                    Person buyer = buyers[n - 1];
                    int bigger = n % 7;
                    int littler = (n + 1) % 7;

                    buyer.bigPresent = bigReceivers[bigger].Name;
                    buyer.littlePresent = littleReceivers[littler].Name;

                    if (buyer.Name == "Sophia Hewitt" && (buyer.bigPresent == "Matt Hewitt" || buyer.littlePresent == "Matt Hewitt"))
                        isGood = false;
                    if (buyer.Name == "India Hewitt" && (buyer.bigPresent == "Jenni Hewitt" || buyer.littlePresent == "Jenni Hewitt"))
                        isGood = false;

                    // Console.WriteLine(buyer.Name + "\t" + buyer.bigPresent + "\t" + buyer.littlePresent);
                }
            } while (!isGood);

            if (isGood)
            {
                for (int n = 1; n <= 7; n++)
                {
                    SendEmail(buyers[n - 1]);
                }

                Console.WriteLine("Done!");
            }
            else Console.WriteLine("Failed - Run Again!");
            
            Console.ReadKey();
        }

        private static bool SendEmail(Person person)
        {
            string from = "secret.santa@southwark.anglican.org";
            string to = person.Email;
            string subject = "Secret Santa Shenanigans (" + person.Salutation + ")!";
            string body = "Hi " + person.Salutation + ", \n\nYou should buy a £10 Christmas tree present for " + person.bigPresent + " and a £5 Christmas tree present for " + person.littlePresent + ".\n\nMerry Christmas!\n\nSecret Santa x";

            SmtpClient email = new SmtpClient();

            MailMessage message = new MailMessage(from, to, subject, body);

            email.Host = "mail.southwark.anglican.org";

            try
            {
                email.Send(message);
            }
            catch (SmtpException)
            {
                return false;
            }

            return true;
        }

        private static bool IsRowInvalid(string email1, string email2, string email3)
        {
            return (email1 == email2) || (email1 == email3) || (email2 == email3);
        }

        private static List<Person> LoadPeople()
        {
            List<Person> people = new List<Person>();

            Person p1 = new Person() { Salutation="Dad", Name = "John Hewitt" };
            Person p2 = new Person() { Salutation="Mum", Name = "Chris Hewitt" };
            Person p3 = new Person() { Salutation="Kate", Name = "Kate Hewitt" };
            Person p4 = new Person() { Salutation="Matt", Name = "Matt Hewitt", };
            Person p5 = new Person() { Salutation="Jenni", Name = "Jenni Hewitt" };
            Person p6 = new Person() { Salutation="Sophia", Name = "Sophia Hewitt" };
            Person p7 = new Person() { Salutation="India", Name = "India Hewitt" };

            people.Add(p1);
            people.Add(p2);
            people.Add(p3);
            people.Add(p4);
            people.Add(p5);
            people.Add(p6);
            people.Add(p7);

            List<Person> peopleToReturn = new List<Person>();

            for (int n = 1; n <= 7; n++)
            {
                int r = new Random(DateTime.Now.Millisecond).Next(8 - n) + 1;

                peopleToReturn.Add(people.ElementAt(r - 1));
                people.RemoveAt(r - 1);
            }

            return peopleToReturn;

        }
    }
}
