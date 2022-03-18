using System;
using System.IO;
using FluentAssertions;
using HomeAssignment.Contracts;
using HomeAssignment.Task3.Contracts;
using Moq;
using NUnit.Framework;

namespace HomeAssignment.Task3.Services.Tests
{
    public class SHACalcServiceTests
    {
        private SHACalcService _shaCalcService;
        private Mock<IUrlValidator> _urlValidatorMock;
        private Mock<IStreamExtractor> _streamExtractorMock;
        private Mock<ISHACalculator> _shaCalculatorMock;


        [SetUp]
        public void Setup()
        {
            _urlValidatorMock = new Mock<IUrlValidator>();
            _urlValidatorMock.Setup(o => o.IsValidFileUrl(It.IsAny<string>()))
                .Returns(true);

            _streamExtractorMock = new Mock<IStreamExtractor>();
            _shaCalculatorMock = new Mock<ISHACalculator>();

            _shaCalcService = new SHACalcService(
                _urlValidatorMock.Object,
                _streamExtractorMock.Object,
                _shaCalculatorMock.Object
            );
        }

        [Test]
        public void When_InvalidUrlData_Throws()
        {
            var invalidUrl = "someInvalidUrl";
            _urlValidatorMock.Setup(o => o.IsValidFileUrl(invalidUrl))
                .Returns(false);

            var exception = Assert.Throws<ArgumentException>(
                () => _shaCalcService.Calc(invalidUrl)
            );
            exception.Should().NotBeNull();
            exception.Message.Should().Contain(SHACalcService.InvalidFileUrlPassed);
        }

        [Test]
        public void When_CalculationThrows_StreamDisposes()
        {
            var validUrl = "someUrl";
            var myStream = new MyStream();

            _streamExtractorMock.Setup(o => o.GetFileStream(It.IsAny<string>()))
                .Returns(myStream);
            _shaCalculatorMock.Setup(o => o.Calc(It.IsAny<Stream>()))
                .Throws<ArgumentException>();

            var exception = Assert.Throws<ArgumentException>(
                () => _shaCalcService.Calc(validUrl)
            );
            exception.Should().NotBeNull();
            myStream.DisposeWasCalled.Should().BeTrue();
        }

        private class MyStream : Stream
        {
            public bool DisposeWasCalled { get; private set; }

            public override void Flush()
            {
                throw new NotImplementedException();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotImplementedException();
            }

            public override void SetLength(long value)
            {
                throw new NotImplementedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }
            

            protected override void Dispose(bool disposing)
            {
                DisposeWasCalled = true;
            }

            public override bool CanRead { get; }
            public override bool CanSeek { get; }
            public override bool CanWrite { get; }
            public override long Length { get; }
            public override long Position { get; set; }
        }
    }
}