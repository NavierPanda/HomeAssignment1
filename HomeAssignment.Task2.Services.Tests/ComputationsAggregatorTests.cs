using System;
using System.Threading.Tasks;
using FluentAssertions;
using HomeAssignment.Task2.Contracts;
using Moq;
using NUnit.Framework;

namespace HomeAssignment.Task2.Services.Tests
{
    public class ComputationsAggregatorTests
    {
        private ComputationsAggregator _calculationAggregator;
        private Mock<ILongRunningCalculator> _longRunningCalculator;

        [SetUp]
        public void Setup()
        {
            _longRunningCalculator = new Mock<ILongRunningCalculator>();
            _calculationAggregator = new ComputationsAggregator(_longRunningCalculator.Object);
        }

        [Test]
        public void When_TwoIterationsThrowsExceptions_ThrowsAggregateException()
        {
            var firstException = new ArgumentException();
            _longRunningCalculator.Setup(o => o.LongRunning(10))
                .Throws(() => firstException);
                
            var secondException = new InvalidOperationException();
            _longRunningCalculator.Setup(o => o.LongRunning(20))
                .Throws(() => secondException);

            var firstExceptionOnly = Assert.ThrowsAsync<ArgumentException>(
                async () => await _calculationAggregator.BuildAggregatedRecord()
            );
            firstExceptionOnly.Should().NotBeNull().And.BeEquivalentTo(firstException);
        }

        [Test]
        [TestCase(100, "00:00:00.20")]
        [TestCase(1000, "00:00:02")]
        [TestCase(10000, "00:00:20")]
        public async Task When_EverIterationLasts10Seconds_LastsShorter(int taskDelay, TimeSpan noMoreTime)
        {
            _longRunningCalculator.Setup(o => o.LongRunning(It.IsAny<int>()))
                .Returns(() => Task.Delay(taskDelay).ContinueWith(task => true));
            var realTime = await _calculationAggregator.BuildAggregatedRecord();
            realTime.Should().BeLessOrEqualTo(noMoreTime);
        }
    }
}