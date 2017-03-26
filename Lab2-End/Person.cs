// <copyright file="Person.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Lab1Start
{
    using System;
    using System.Linq;

    public class Person : IDisposable
    {
        private readonly Uri blog = new Uri(@"http://www.thinqlinq.com");
        private readonly int votingAge = 18;
        private readonly ToDispose shouldBeDisposed = new ToDispose();

        public int Age { get; set; }

        public string Name { get; set; }

        public Person Parent { get; set; }

        public static string SayHello()
        {
            var hello = "Hello";
            var builder = new System.Text.StringBuilder();
            builder.Append(hello);
            for (var i = 0; i < 20; i++)
            {
                builder.Append("Hello");
            }

            hello = builder.ToString();

            return hello;
        }

        /// <summary>
        /// Determines if the person is old enough to vote
        /// </summary>
        /// <returns>Determination if they are of voting age</returns>
        public bool CanVote() => Age > votingAge ? true : false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool IsFibber()
        {
            // Should use switch
            switch (Age)
            {
                case 2:
                    return true;
                case 3:
                    return true;
                case 5:
                    return true;
                case 8:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsPrime()
        {
            int[] primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };
            return primes.Any(p => p == Age);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                shouldBeDisposed.Dispose();
            }
        }
    }
}
