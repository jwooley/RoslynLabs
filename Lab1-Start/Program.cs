﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;

namespace Lab1_Start
{

    public class Class1
    {
        public void ShouldUseVar(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentException("input");

                Person person = GetJim();
                Console.Write($"{person.Name} is {person.Age} years old");
                Console.WriteLine("His parent is [1]", person.Parent.Name, person.Parent.Parent.Age);
                Console.WriteLine($"His blog is {person.Blog}");
            }
            catch (Exception ex)
            {
            }
        }

        public static void TryUsingDisposableWithoutDisposing()
        {
            var toDispose = new ToDispose();
            toDispose.Write("test");
        }
        public Person GetJim()
        {
            Person person = new Person { };
            person.Age = 42;
            person.Name = "Jim";
            person.Parent = null;
            return person;
        }
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
        public bool CanVote(int minAge)
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
        public string SayHello()
        {
            var unused = "I can be removed";
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
        private StringWriter sw = new StringWriter();
        public ToDispose()
        {

        }
        public void Write(string value)
        {
            sw.Write(value);
        }
        public void Dispose()
        {
            // Dispose me
            sw.Dispose();
        }
    }
}
