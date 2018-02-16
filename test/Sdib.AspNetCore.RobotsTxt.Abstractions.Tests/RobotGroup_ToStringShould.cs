using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sdib.AspNetCore.RobotsTxt.Abstractions;

namespace Sdib.AspNetCore.RobotsTxt.Tests
{
    [TestClass]
    public class RobotGroup_ToStringShould
    {
        [DataTestMethod]
        [DataRow("TwitterBot")]
        [DataRow("*")]
        [DataRow("Google")]
        public void ReturnOnlyUserAgent(string userAgent)
        {
            var group = new RobotGroup(userAgent);

            string output = group.ToString();

            var parts = output.Split(Environment.NewLine);
            Assert.AreEqual($"User-agent: {group.UserAgent}", parts[0]);
            Assert.AreEqual(1, parts.Length);
        }

        [DataTestMethod]
        [DataRow("TwitterBot", "/admin", "/players")]
        [DataRow("*", "/test", "/user")]
        [DataRow("Google", "/", "/private")]
        public void ReturnUserAgent_AndAllow_WhenAny_AndDisallow_WhenAny(string userAgent, string allow, string disallow)
        {
            var group = new RobotGroup(userAgent, new[] { allow }, new[] { disallow });

            string output = group.ToString();

            var parts = output.Split(Environment.NewLine);
            Assert.AreEqual($"User-agent: {group.UserAgent}", parts[0]);
            Assert.AreEqual($"Allow: {allow}", parts[1]);
            Assert.AreEqual($"Disallow: {disallow}", parts[2]);
        }

        [DataTestMethod]
        [DataRow("TwitterBot", "/admin")]
        [DataRow("*", "/test")]
        [DataRow("Google", "/")]
        public void ReturnUserAgent_AndAllow_WhenAny_WihoutDisallow_WhenNone(string userAgent, string allow)
        {
            var group = new RobotGroup(userAgent, new[] { allow }, new string[0]);

            string output = group.ToString();

            string[] parts = output.Split(Environment.NewLine);
            Assert.AreEqual($"User-agent: {group.UserAgent}", parts[0]);
            Assert.AreEqual($"Allow: {allow}", parts[1]);
            Assert.AreEqual(2, parts.Length);
        }
    }
}
