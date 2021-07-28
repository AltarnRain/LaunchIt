// <copyright file="Retry.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using System;

    /// <summary>
    /// Helper class that retries calling a function.
    /// </summary>
    public class Retry
    {
        private readonly Func<bool> action;
        private readonly Action<int> onFail;
        private readonly Action<int> onSucces;
        private readonly int attempts;
        private int retryCount;
        private int retryTimeout = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref="Retry" /> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="onFail">The on fail.</param>
        /// <param name="onSucces">The on succes.</param>
        /// <param name="attempts">The attempts.</param>
        private Retry(Func<bool> action, Action<int> onFail, Action<int> onSucces, int attempts = 3)
        {
            this.action = action;
            this.onFail = onFail;
            this.onSucces = onSucces;
            this.attempts = attempts;
            this.retryCount = 0;
        }

        /// <summary>
        /// Tries this instance.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="onFail">The on fail.</param>
        /// <param name="onSucces">The on succes.</param>
        /// <param name="attempts">The attempts.</param>
        public static void Try(Func<bool> action, Action<int> onFail, Action<int> onSucces, int attempts = 3)
        {
            new Retry(action, onFail, onSucces, attempts).Go();
        }

        /// <summary>
        /// Goes this instance.
        /// </summary>
        private void Go()
        {
            while (this.retryCount <= this.attempts)
            {
                var result = this.action();

                if (result)
                {
                    this.onSucces(this.retryCount);
                    return;
                }

                // Sleep, increase retry time out and try again.
                System.Threading.Thread.Sleep(this.retryTimeout);
                this.retryTimeout *= 2;
                this.retryCount++;
            }

            this.onFail.Invoke(this.retryCount);
        }
    }
}
