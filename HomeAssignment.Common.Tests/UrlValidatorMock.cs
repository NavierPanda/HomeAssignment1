using HomeAssignment.Contracts;
using Moq;

namespace HomeAssignment.Services.Tests
{
    public sealed class UrlValidatorMock : Mock<IUrlValidator>
    {
        public UrlValidatorMock()
        {
            Setup(o => o.IsValidFileUrl(It.IsAny<string>()))
                .Returns(true);
        }

        public void MakeFileUrlInvalid(string fileUrl)
        {
            Setup(o => o.IsValidFileUrl(fileUrl))
                .Returns(false);
        }
    }
}