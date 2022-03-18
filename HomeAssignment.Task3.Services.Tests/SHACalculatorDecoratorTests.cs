using System;
using FluentAssertions;
using HomeAssignment.Contracts;
using HomeAssignment.Services.Tests;
using Moq;
using NUnit.Framework;

namespace HomeAssignment.Task3.Services.Tests
{
    public class SHACalculatorDecoratorTests
    {
        private SHACalculatorDecorator _shaCalculatorDecorator;
        private UrlValidatorMock _validatorMock;
        private Mock<ISHACalculator> _shaCalculator;

        [SetUp]
        public void Setup()
        {
            _validatorMock = new UrlValidatorMock();
            _shaCalculator = new Mock<ISHACalculator>();
            _shaCalculatorDecorator = new SHACalculatorDecorator(_shaCalculator.Object , _validatorMock.Object);
        }
        
        [Test]
        public void When_InvalidUrl_Throws()
        {
            var invalidUrl = "someInvalidUrl";
            _validatorMock.MakeFileUrlInvalid(invalidUrl);

            var exception = Assert.Throws<ArgumentException>(
                () =>  _shaCalculatorDecorator.Calc(invalidUrl)
            );
            exception.Should().NotBeNull();
            exception.Message.Should().Be(SHACalculator.InvalidFileUrlPassed);
        }
        
        [Test]
        public void When_ShaCalcFails_Throws()
        {
            var someValidUrl = "someValidUrl";
            _shaCalculator.Setup(o => o.Calc(It.IsAny<string>()))
                .Throws(new Exception("SomeThing bad happened"));

            var exception = Assert.Throws<Exception>(
                () =>  _shaCalculatorDecorator.Calc(someValidUrl)
            );
            exception.Should().NotBeNull();
            exception.Message.Should().Be("SomeThing bad happened");
        }
        
        [Test]
        public void When_CalledTwice_SingleInternalCall()
        {
            var someValidUrl = "someValidUrl";
            
            _shaCalculatorDecorator.Calc(someValidUrl);
            _shaCalculatorDecorator.Calc(someValidUrl);
            
            _shaCalculator.Verify(calculator => calculator.Calc(someValidUrl), Times.Once);
            _shaCalculator.VerifyNoOtherCalls();
        }
        
        [Test]
        public void When_CalledTwiceShaCalcFails_ThrowsTwice()
        {
            var someValidUrl = "someValidUrl";
            _shaCalculator.Setup(o => o.Calc(It.IsAny<string>()))
                .Throws(new Exception("SomeThing bad happened"));

            var exception = Assert.Throws<Exception>(
                () =>  _shaCalculatorDecorator.Calc(someValidUrl)
            );
            exception.Should().NotBeNull();
            exception.Message.Should().Be("SomeThing bad happened");
            
            exception = Assert.Throws<Exception>(
                () =>  _shaCalculatorDecorator.Calc(someValidUrl)
            );
            exception.Should().NotBeNull();
            exception.Message.Should().Be("SomeThing bad happened");
        }
    }
}