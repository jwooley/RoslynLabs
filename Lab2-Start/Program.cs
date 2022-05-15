using System;
using System.Linq;
using System.IO;

namespace Lab1Start
{

    public static class Class1
    {
        public static void ShouldUseVar(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentException(null, nameof(input));

                Person person = Jim;
                Console.Write($"{person.Name} is {person.Age} years old");
                Console.WriteLine($"His parent is {person.Parent.Name}");
                Console.WriteLine($"Blog name {person.Blog}");
            }
            catch (Exception)
            {
            }
        }

        public static void TryUsingDisposableWithoutDisposing()
        {
            using var toDispose = new ToDispose();
            toDispose.Write("test");
        }
        public static Person Jim =>
                 new()
                 {
                     Age = 42,
                     Name = "Jim",
                     Parent = null
                 };
    }
    public class Person
    {
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

        readonly int VotingAge = 18;
        public Uri Blog { get; } = new Uri("thinqlinq");

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
        public static string SayHello()
        {
            var hello = "Hello";
            for (int i = 0; i < 20; i++)
            {
                hello += "Hello";
            }
            return hello;
        }
    }
    public class ToDispose : IDisposable
    {
        private readonly StringWriter sw = new();
        public ToDispose()
        {

        }
        public void Write(string value)
        {
            sw.Write(value);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                sw.Dispose();
            }
        }
        public void Dispose()
        {
            // Dispose me
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
