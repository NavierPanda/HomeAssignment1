using System;
using System.Net;
using FluentAssertions;
using NUnit.Framework;

namespace HomeAssignment.Task3.Services.Tests
{
    public class StreamExtractorTests
    {
        private StreamExtractor _shaCalculator;

        [SetUp]
        public void Setup()
        {
            _shaCalculator = new StreamExtractor();
        }

        [Test]
        public void When_InvalidUrl_Throws()
        {
            var fileStream = "./someFile.txt";
            var exception = Assert.Throws<UriFormatException>(
                () => _shaCalculator.GetFileStream(fileStream)
            );
            exception.Should().NotBeNull();
        }
        
        [Test]
        public void When_ValidUrl_NoStream_Throws()
        {
            var webUrl = "https://speed.hetzner.de/100MB.bin111111";
            var exception = Assert.Throws<WebException>(
                () => _shaCalculator.GetFileStream(webUrl)
            );
            exception.Should().NotBeNull();
        }

        [Test]
        public void When_ValidUrlWithStream_ReturnsStream()
        {
            string fileUrl = "https://speed.hetzner.de/100MB.bin";
            using var stream = _shaCalculator.GetFileStream(fileUrl);
            stream.Should().NotBeNull();
            stream.CanRead.Should().BeTrue();
        }
    }
}