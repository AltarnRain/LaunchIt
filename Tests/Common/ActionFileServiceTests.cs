// <copyright file="ActionFileServiceTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Common
{
    using Domain.Models.Programs;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.IO;
    using Tests.Base;

    /// <summary>
    /// Tests the <see cref="ActionFileService"/>.
    /// </summary>
    /// <seealso cref="TestBase" />
    [TestClass]
    public class ActionFileServiceTests : TestBase
    {
        /// <summary>
        /// Writes the test.
        /// </summary>
        [TestMethod]
        public void WriteTest()
        {
            using (var scope = this.StartTestScope())
            {
                // Arrange
                var target = scope.ActionFileService;
                var pathProvider = scope.PathProvider;

                var file = pathProvider.MapPath("~/TestAction.txt");

                var programModels = new List<ProgramModel>
                {
                    new ProgramModel
                    {
                        ProgramType = ProgramType.Service,
                        Name = "AName",
                        ServiceInfo = new ServiceInfo
                        {
                            DisplayName = "A name",
                        },
                        Running = true,
                    },
                    new ProgramModel
                    {
                        ProgramType = ProgramType.Service,
                        Name = "AnotherName",
                        ServiceInfo = new ServiceInfo
                        {
                            DisplayName = "Another name",
                        },
                        Running = false,
                    },
                    new ProgramModel
                    {
                        ProgramType = ProgramType.Executable,
                        Name = "An Executable",
                        Running = true,
                    },
                };

                // Act
                target.Write(programModels, file);

                // Assert
                Assert.IsTrue(File.Exists(file));

                var allLines = File.ReadAllLines(file);
                Assert.IsTrue(allLines.Length > 0);

                var serviceSection = allLines[3];
                Assert.AreEqual(Domain.Constants.Sections.Services, serviceSection);

                var service = allLines[4];
                Assert.AreEqual("A name", service);

                var executableSection = allLines[6];
                Assert.AreEqual(Domain.Constants.Sections.Executables, executableSection);

                var executable = allLines[7];
                Assert.AreEqual("An Executable", executable);
            }
        }
    }
}