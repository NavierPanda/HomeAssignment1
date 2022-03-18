using System;
using FluentAssertions;
using HomeAssignment.Task3.Contracts;
using Moq;
using NUnit.Framework;

namespace HomeAssignment.Task3.Services.Tests
{
    public class SHACalculatorDecoratorTests
    {
        private SHACalcServiceDecorator _shaCalculatorDecorator;
        private Mock<ISHACalcService> _shaCalculator;

        [SetUp]
        public void Setup()
        {
            _shaCalculator = new Mock<ISHACalcService>();
            _shaCalculatorDecorator = new SHACalcServiceDecorator(_shaCalculator.Object);
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