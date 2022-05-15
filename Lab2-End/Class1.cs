// <copyright file="Class1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Lab1Start
{
    using System;

    /// <summary>
    /// Class1.
    /// </summary>
    public class Class1
    {
        /// <summary>
        /// Gets the person instance.
        /// </summary>
        public static Person Jim =>
                 new ()
                 {
                     Age = 42,
                     Name = "Jim",
                     Parent = null,
                 };

        /// <summary>
        /// Test method.
        /// </summary>
        public static void TryUsingDisposableWithoutDisposing()
        {
            using var toDispose = new ToDispose();
            toDispose.Write("test");
        }

        /// <summary>
        /// Should do something.
        /// </summary>
        /// <param name="input">input value.</param>
        public static void ShouldUseVar(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException(null, nameof(input));
            }

            var person = Jim;
            Console.Write($"{person.Name} is {person.Age} years old");
            Console.WriteLine($"His parent is {person.Parent.Name}");
            Console.WriteLine($"Blog name {person.Blog}");
        }
    }
}
