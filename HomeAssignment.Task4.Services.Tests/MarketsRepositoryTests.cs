using HomeAssignment.Task4.Contracts;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace HomeAssignment.Task4.Services.Tests
{
    public class MarketsRepositoryTests
    {
        private MarketsRepository _marketsRepository;

        [SetUp]
        public void Setup()
        {
            var someOptions = Options.Create(new BlocktapWebApiOptions
            {
                BaseUrl =  "https://api.blocktap.io/graphql"
            });

            _marketsRepository = new MarketsRepository(someOptions);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}