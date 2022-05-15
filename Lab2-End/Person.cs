// <copyright file="Person.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Lab1Start
{
    using System;
    using System.Linq;

    /// <summary>
    /// Person.
    /// </summary>
    public class Person
    {
        private readonly int votingAge = 18;

        /// <summary>
        /// Gets or sets the Age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets the Blog.
        /// </summary>
        public Uri Blog { get; } = new Uri("https://thinqlinq");

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the person's parent.
        /// </summary>
        public Person Parent { get; set; }

        /// <summary>
        /// Person greeting.
        /// </summary>
        /// <returns>What they say.</returns>
        public static string SayHello()
        {
            var hello = "Hello";
            var builder = new System.Text.StringBuilder();
            builder.Append(hello);
            for (int i = 0; i < 20; i++)
            {
                builder.Append("Hello");
            }

            hello = builder.ToString();
            return hello;
        }

        /// <summary>
        /// Determines if the person is old enough to vote.
        /// </summary>
        /// <returns>True if they are of valid age.</returns>
        public bool CanVote() => Age > votingAge;

        /// <summary>
        /// Bad Fibonacci implementation.
        /// </summary>
        /// <returns>True.</returns>
        public bool IsFibber() =>
            Age switch
            {
                2 => true,
                3 => true,
                5 => true,
                8 => true,
                _ => false,
            };

        /// <summary>
        /// Checks if the number is prime.
        /// </summary>
        /// <returns>True if prime.</returns>
        public bool IsPrime()
        {
            int[] primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };
            return primes.Any(p => p == Age);
        }
    }
}
