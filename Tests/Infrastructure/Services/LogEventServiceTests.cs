// <copyright file="LogEventServiceTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services.Tests
{
    using Logic.Contracts.Loggers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /// <summary>
    /// Tests <see cref="LogEventService"/>.
    /// </summary>
    [TestClass]
    public class LogEventServiceTests
    {
        /// <summary>
        /// Tests if the LogEventService passes the message to its subscribers.
        /// </summary>
        [TestMethod]
        public void LogTest()
        {
            // Arrange
            var receivedMessage1 = string.Empty;
            var receivedMessage2 = string.Empty;
            var receivedMessage3 = "Won't change";

            var target = new LogEventService();
            var testLogger1 = new TestLogger((m) => receivedMessage1 = m[14..]);
            var testLogger2 = new TestLogger((m) => receivedMessage2 = m[14..]);
            var testLogger3 = new TestLogger((m) => receivedMessage3 = m[14..]);

            target.Subscribe(testLogger1);
            target.Subscribe(testLogger2);

            // Act
            target.Log("Message"); // passes 'Message' to its subscribers.

            // Assert
            Assert.AreEqual("Message", receivedMessage1);
            Assert.AreEqual("Message", receivedMessage2);
            Assert.AreEqual("Won't change", receivedMessage3);
        }

        /// <summary>
        /// Same as the test above. 'Skipped' just adds 'Skipped: ' to the message.
        /// </summary>
        [TestMethod]
        public void LogSkippedTest()
        {
            // Arrange
            var receivedMessage1 = string.Empty;
            var receivedMessage2 = string.Empty;
            var receivedMessage3 = "Won't change";

            var target = new LogEventService();
            var testLogger1 = new TestLogger((m) => receivedMessage1 = m[14..]);
            var testLogger2 = new TestLogger((m) => receivedMessage2 = m[14..]);
            var testLogger3 = new TestLogger((m) => receivedMessage3 = m[14..]);

            target.Subscribe(testLogger1);
            target.Subscribe(testLogger2);

            // Act
            target.LogSkipped("Message"); // passes 'Message' to its subscribers.

            // Assert
            Assert.AreEqual("Skipped: Message", receivedMessage1);
            Assert.AreEqual("Skipped: Message", receivedMessage2);
            Assert.AreEqual("Won't change", receivedMessage3);
        }

        /// <summary>
        /// Logs the stopped test.
        /// </summary>
        [TestMethod]
        public void LogStoppedTest()
        {
            // Arrange
            var receivedMessage1 = string.Empty;
            var receivedMessage2 = string.Empty;
            var receivedMessage3 = "Won't change";

            var target = new LogEventService();
            var testLogger1 = new TestLogger((m) => receivedMessage1 = m[14..]);
            var testLogger2 = new TestLogger((m) => receivedMessage2 = m[14..]);
            var testLogger3 = new TestLogger((m) => receivedMessage3 = m[14..]);

            target.Subscribe(testLogger1);
            target.Subscribe(testLogger2);

            // Act
            target.LogStopped("Message"); // passes 'Message' to its subscribers.

            // Assert
            Assert.AreEqual("Stopped: Message", receivedMessage1);
            Assert.AreEqual("Stopped: Message", receivedMessage2);
            Assert.AreEqual("Won't change", receivedMessage3);
        }

        /// <summary>
        /// Logs the stopped test.
        /// </summary>
        [TestMethod]
        public void TestUnsubscribeReturnAction()
        {
            // Arrange
            var receivedMessage1 = string.Empty;
            var receivedMessage2 = string.Empty;

            var target = new LogEventService();
            var testLogger1 = new TestLogger((m) => receivedMessage1 = m[14..]);
            var testLogger2 = new TestLogger((m) => receivedMessage2 = m[14..]);

            var unsubLogger1 = target.Subscribe(testLogger1);
            target.Subscribe(testLogger2);

            // Act
            target.Log("Message"); // passes 'Message' to its subscribers.
            unsubLogger1(); // unsub
            target.Log("Message 2"); // passes 'Message' to its subscribers which no longer included testLogger1

            // Assert
            Assert.AreEqual("Message", receivedMessage1); // First message.
            Assert.AreEqual("Message 2", receivedMessage2); // Second message.
        }

        private class TestLogger : ILog
        {
            private readonly Action<string> action;

            public TestLogger(Action<string> action)
            {
                this.action = action;
            }

            /// <summary>
            /// Logs the specified message.
            /// </summary>
            /// <param name="message">The message.</param>
            public void Log(string message)
            {
                this.action(message);
            }
        }
    }
}