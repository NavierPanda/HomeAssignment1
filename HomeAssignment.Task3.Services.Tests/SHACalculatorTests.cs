using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace HomeAssignment.Task3.Services.Tests
{
    public class SHACalculatorTests
    {
        private SHACalculator _shaCalculator;

        [SetUp]
        public void Setup()
        {
            _shaCalculator = new SHACalculator();
        }

        [Test]
        public void When_BadStream_Throws()
        {
            var fileStream = File.Open("./someFile.txt", FileMode.Open);
            fileStream.Close();

            var exception = Assert.Throws<ArgumentException>(
                () => _shaCalculator.Calc(fileStream)
            );
            exception.Should().NotBeNull();
            exception.Message.Should().Contain(SHACalculator.UnreadableStream);
        }
        
        [Test]
        public void When_LocalFile_ReturnsExpectedHash()
        {
            using var fileStream = File.Open("./someFile.txt", FileMode.Open);
            var actualHashString = _shaCalculator.Calc(fileStream);
            var expectedHashString = "35-6A-19-2B-79-13-B0-4C-54-57-4D-18-C2-8D-46-E6-39-54-28-AB";
            actualHashString.Should().Be(expectedHashString);
        }

        [Test]
        [Ignore("Works fine, but 2 minutes")]
        public void When_RealUrl_ReturnsExpectedHash()
        {
            string fileUrl = "https://speed.hetzner.de/100MB.bin";
            string hashString = "2C-2C-EC-CB-5E-C5-57-4F-79-1D-45-B6-3C-94-0C-FF-20-55-0F-9A";
            var req = System.Net.WebRequest.Create(fileUrl);
            using var stream = req.GetResponse().GetResponseStream();
            var actualHashString = _shaCalculator.Calc(stream);
            actualHashString.Should().Be(hashString);
        }
    }
}