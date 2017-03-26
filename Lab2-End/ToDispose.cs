// <copyright file="ToDispose.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Lab1Start
{
    using System;

    public class ToDispose : IDisposable
    {
        public const string SOMECONSTANT = "123";

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

#pragma warning disable CC0091 // Use static method
        private void Dispose(bool disposing)
#pragma warning restore CC0091 // Use static method
        {
            if (disposing)
            {
                // Get rid of resources
            }
        }
    }
}
