// <copyright file="ActionFileServiceTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Common.Tests
{
    using Domain.Models.Programs;
    using global::Tests.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Tests the <see cref="ActionFileService"/>.
    /// </summary>
    /// <seealso cref="Tests.Base.TestBase" />
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

                var file = pathProvider.MapPath("~/TestAction.txts");

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
                    },
                };

                // Act
                target.Write(programModels, file);

                // Assert
                Assert.IsTrue(File.Exists(file));

                var allText = File.ReadAllText(file);
                Assert.IsFalse(string.IsNullOrWhiteSpace(allText));
            }
        }
    }
}