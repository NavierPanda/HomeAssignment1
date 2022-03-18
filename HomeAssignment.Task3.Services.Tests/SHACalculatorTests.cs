using System;
using FluentAssertions;
using HomeAssignment.Services.Tests;
using NUnit.Framework;

namespace HomeAssignment.Task3.Services.Tests
{
    public class SHACalculatorTests
    {
        private SHACalculator _shaCalculator;
        private UrlValidatorMock _validatorMock;

        [SetUp]
        public void Setup()
        {
            _validatorMock = new UrlValidatorMock();
            _shaCalculator = new SHACalculator(_validatorMock.Object);
        }

        [Test]
        public void When_InvalidData_Throws()
        {
            var invalidUrl = "someInvalidUrl";
            _validatorMock.MakeFileUrlInvalid(invalidUrl);

            var exception = Assert.Throws<ArgumentException>(
                () => _shaCalculator.Calc(invalidUrl)
            );
            exception.Should().NotBeNull();
            exception.Message.Should().Be(SHACalculator.InvalidFileUrlPassed);
        }
        
        [Test]
        public void When_LocalFile_ReturnsExpectedHash()
        {
            var actualHashString = _shaCalculator.Calc("./someFile.txt");
            var expectedHashString = "35-6A-19-2B-79-13-B0-4C-54-57-4D-18-C2-8D-46-E6-39-54-28-AB";
            actualHashString.Should().Be(expectedHashString);
        }

        [Test]
        [TestCase("https://speed.hetzner.de/100MB.bin", "2C-2C-EC-CB-5E-C5-57-4F-79-1D-45-B6-3C-94-0C-FF-20-55-0F-9A")]
        public void When_RealUrl_ReturnsExpectedHash(string fileUrl, string hashString)
        {
            var actualHashString = _shaCalculator.Calc(fileUrl);
            actualHashString.Should().Be(hashString);
        }
    }
}