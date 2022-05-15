// <copyright file="ToDispose.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Lab1Start
{
    using System;
    using System.IO;

    /// <summary>
    /// Disposable sample.
    /// </summary>
    public class ToDispose : IDisposable
    {
        private readonly StringWriter sw = new ();

        /// <summary>
        /// Write to the log.
        /// </summary>
        /// <param name="value">Value to write.</param>
        public void Write(string value)
        {
            sw.Write(value);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            // Dispose me
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">If disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                sw.Dispose();
            }
        }
    }
}
