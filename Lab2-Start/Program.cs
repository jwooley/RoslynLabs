using System;
using System.Linq;

namespace Lab1Start
{

    public class Class1
    {
        public void ShouldUseVar(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    throw new ArgumentException("input");
                }

                Person person = GetJim();
                Console.Write($"{person.Name} is {person.Age} years old");
                Console.WriteLine("His parent is {0}", person.Parent.Name);
            }
            catch (Exception ex)
            {
            }
        }
        public Person GetJim()
        {
            var person = new Person();
            person.Age = 42;
            person.Name = "Jim";
            person.Parent = null;
            return person;
        }
    }
    public class Person : IDisposable
    {

        private ToDispose ShouldBeDisposed = new ToDispose();

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public Person Parent { get; set; }

        public int Age { get; set; }

        /// <summary>
        /// Determines if the person is old enough to vote 
        /// </summary>
        public bool CanVote()
        {
            // Ternary method body function
            if (Age > VotingAge)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        int VotingAge = 18;
        Uri Blog = new Uri("thinqlinq");

        public bool IsPrime()
        {
            int[] primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };
            return primes.Where(p => p == Age).Any();
        }
        public bool IsFibber()
        {
            // Should use switch
            if (Age == 2)
            {
                return true;
            }
            else if (Age == 3)
            {
                return true;
            }
            else if (Age == 5)
            {
                return true;
            }
            else if (Age == 8)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string SayHello()
        {
            var hello = "Hello";
            for (var i = 0; i < 20; i++)
            {
                hello += "Hello";
            }
            return hello;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                ShouldBeDisposed.Dispose();
            }
        }
    }
    public class ToDispose : IDisposable
    {
        public const string SOMECONSTANT = "123";

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Get rid of resources
            }
        }

    }
}
