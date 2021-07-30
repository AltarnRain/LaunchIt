﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Extensions;
// <copyright file="ExtensionClassTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Extensions.Tests
{
    using Domain.Models.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests extension methods.
    /// </summary>
    [TestClass]
    public class ExtensionClassTests
    {
        /// <summary>
        /// Determines whether [is switch test].
        /// </summary>
        [TestMethod]
        public void IsSwitchTest()
        {
            // Act
            var result1 = "-reset".IsSwitchCommand();
            var result2 = "/reset".IsSwitchCommand();
            var result3 = "reset".IsSwitchCommand();

            // Assert
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsFalse(result3);
        }

        /// <summary>
        /// Starts the monitoring test.
        /// </summary>
        [TestMethod]
        public void StartMonitoringAllFalseTest()
        {
            // Act
            var model = GetLaunchModelWithoutMonitoring();

            // Act
            var result = model.StartMonitoring();

            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Starts the monitoring test.
        /// </summary>
        [TestMethod]
        public void StartMonitoringMonitorRestartsTrueTest()
        {
            // Act
            var model = GetLaunchModelWithoutMonitoring();
            model.MonitoringConfiguration.MonitorRestarts = true;

            // Act
            var result = model.StartMonitoring();

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Starts the monitoring test.
        /// </summary>
        [TestMethod]
        public void StartMonitoringShutdownServiceAfterRestartTrueTest()
        {
            // Act
            var model = GetLaunchModelWithoutMonitoring();
            model.ServiceShutdownConfiguration.ShutdownAfterRestart = true;

            // Act
            var result = model.StartMonitoring();

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Starts the monitoring test.
        /// </summary>
        [TestMethod]
        public void StartMonitoringShutdownExecutableAfterRestartTrueTest()
        {
            // Act
            var model = GetLaunchModelWithoutMonitoring();
            model.ExecutableShutdownConfiguration.ShutdownAfterRestart = true;

            // Act
            var result = model.StartMonitoring();

            // Assert
            Assert.IsTrue(result);
        }

        private static LaunchModel GetLaunchModelWithoutMonitoring()
        {
            return new LaunchModel
            {
                MonitoringConfiguration = new MonitoringConfiguration
                {
                    MonitorRestarts = false,
                },
                ExecutableShutdownConfiguration = new ShutdownConfigurationModel
                {
                    ShutdownAfterRestart = false,
                },
                ServiceShutdownConfiguration = new ShutdownConfigurationModel
                {
                    ShutdownAfterRestart = false,
                },
            };
        }
    }
}