// <copyright file="Class1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Lab1Start
{
    using System;

    public class Class1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Because")]
        public Person Jim => new Person
        {
            Age = 42,
            Name = "Jim",
            Parent = null,
        };

        public void ShouldUseVar(string input)
        {
            {
                if (string.IsNullOrEmpty(input))
                {
                    throw new ArgumentException("not supplied", nameof(input));
                }

                var person = Jim;
                Console.Write($"{person.Name} is {person.Age} years old");
                Console.WriteLine($"His parent is {person.Parent.Name}");
            }
        }
    }
}
