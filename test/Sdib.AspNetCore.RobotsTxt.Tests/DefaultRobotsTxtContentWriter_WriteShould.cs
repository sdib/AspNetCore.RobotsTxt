using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sdib.AspNetCore.RobotsTxt.Abstractions;
using System.Threading.Tasks;

namespace Sdib.AspNetCore.RobotsTxt.Tests
{
    [TestClass]
    public class DefaultRobotsTxtContentWriter_WriteShould
    {
        private IRobotTxtContentWriter writer;
        private Mock<IRobotGroupProvider> robotGroupsProvider;
        private List<RobotGroup> robotGroups;

        [TestInitialize]
        public void BeforeEach()
        {
            this.robotGroupsProvider = new Mock<IRobotGroupProvider>();
            this.writer = new DefaultRobotsTxtContentWriter(robotGroupsProvider.Object);
            this.robotGroups = new List<RobotGroup>();
            this.robotGroupsProvider.Setup(o => o.GetGroups())
                .ReturnsAsync(robotGroups.ToArray);
        }

        [TestMethod]
        public async Task ReturnGenerationDate()
        {
            string output = await this.writer.WriteAsync();

            Assert.IsTrue(output.StartsWith($"# Generated at {DateTime.UtcNow.ToLocalTime()}"));
        }

        [TestMethod]
        public async Task ReturnEachGroupOnOneLine()
        {
            var robotGroup = new RobotGroup("TestAgent");
            var otherRobotGroup = new RobotGroup("AnotherTestAgent");
            this.robotGroups.Add(robotGroup);
            this.robotGroups.Add(otherRobotGroup);
            
            string output = await this.writer.WriteAsync();

            string[] parts = output.Split(Environment.NewLine);
            Assert.AreEqual(5, parts.Length);
            Assert.AreEqual(robotGroup.ToString(), parts[1]);
            Assert.AreEqual(otherRobotGroup.ToString(), parts[3]);
        }

        [TestMethod]
        public async Task ReturnOnlyOneLine_WhenNoRobotGroup()
        {
            this.robotGroupsProvider.Setup(o => o.GetGroups())
                .ReturnsAsync((RobotGroup[])null);

            string output = await this.writer.WriteAsync();

            string[] parts = output.Split(Environment.NewLine);
            Assert.AreEqual(1, parts.Length);
        }
    }
}