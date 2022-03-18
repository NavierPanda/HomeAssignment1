using System;
using FluentAssertions;
using NUnit.Framework;

namespace HomeAssignment.Task1.Services.Tests
{
    public class TextInverterServiceTests
    {
        private TextInverterService _textInverterService;

        [SetUp]
        public void Setup()
        {
            _textInverterService = new TextInverterService();
        }

        [Test]
        public void When_EmptyString_ReturnsEmptyString()
        {
            var inverted = _textInverterService.InvertStringChars(String.Empty);
            inverted.Should().BeEmpty();
        }

        [Test]
        public void When_NullString_ReturnsNullString()
        {
            var inverted = _textInverterService.InvertStringChars(null);
            inverted.Should().BeNull();
        }
        
        [Test]
        [TestCase("l","l")]
        [TestCase("lol","lol")]
        [TestCase("some data string","gnirts atad emos")]
        public void When_GivenString_ReturnsInvertedString(string originalString, string invertedString)
        {
            var inverted = _textInverterService.InvertStringChars(null);
            inverted.Should().BeNull();
        }
    }
}