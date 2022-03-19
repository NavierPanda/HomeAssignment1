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
        public void When_EmptyString_InvertStringChars_ReturnsEmptyString()
        {
            var inverted = _textInverterService.InvertStringChars(String.Empty);
            inverted.Should().BeEmpty();
        }

        [Test]
        public void When_NullString_InvertStringChars_ReturnsNullString()
        {
            var inverted = _textInverterService.InvertStringChars(null);
            inverted.Should().BeNull();
        }
        
        [Test]
        [TestCase("l","l")]
        [TestCase("lol","lol")]
        [TestCase("some data string","gnirts atad emos")]
        public void When_GivenString_InvertStringChars_ReturnsInvertedString(string originalString, string invertedString)
        {
            var inverted = _textInverterService.InvertStringChars(originalString);
            inverted.Should().Be(invertedString);
        }
        
        [Test]
        public void When_EmptyString_ReverseWordOrder_ReturnsEmptyString()
        {
            var inverted = _textInverterService.ReverseWordOrder(String.Empty);
            inverted.Should().BeEmpty();
        }

        [Test]
        public void When_NullString_ReverseWordOrder_ReturnsNullString()
        {
            var inverted = _textInverterService.ReverseWordOrder(null);
            inverted.Should().BeNull();
        }
        
        [Test]
        [TestCase("l","l")]
        [TestCase("some data string","string data some")]
        [TestCase("some  data    string","string    data  some")]
        [TestCase(" sit amet, consectetur adipiscing elit, sed.", "sed. elit, adipiscing consectetur amet, sit ")]
        public void When_GivenString_ReverseWordOrder_ReturnsReverseWordOrder(string originalString, string invertedString)
        {
            var inverted = _textInverterService.ReverseWordOrder(originalString);
            inverted.Should().Be(invertedString);
        }
    }
}