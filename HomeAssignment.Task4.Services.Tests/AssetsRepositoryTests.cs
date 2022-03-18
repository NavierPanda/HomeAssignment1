using System;
using System.Threading.Tasks;
using FluentAssertions;
using GraphQL.Client.Http;
using HomeAssignment.Task4.Contracts;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace HomeAssignment.Task4.Services.Tests
{
    public class AssetsRepositoryTests
    {
        private AssetsRepository _assetsRepository;

        [SetUp]
        public void Setup()
        {
            var someOptions = Options.Create(new BlocktapWebApiOptions
            {
                BaseUrl = "https://api.blocktap.io/graphql"
            });

            _assetsRepository = new AssetsRepository(someOptions);
        }

        [Test]
        public void When_WrongUrl_GetAllAvailableAssets_Throws()
        {
            var someOptions = Options.Create(new BlocktapWebApiOptions
            {
                BaseUrl = "https://api.blocktap.io/graphql1111"
            });

            _assetsRepository = new AssetsRepository(someOptions);
            
            var exception = Assert.ThrowsAsync<GraphQLHttpRequestException>( async () => await _assetsRepository.GetAllAvailableAssets());
            
        }

        [Test]
        public async Task When_BaseUrlIsRight_GetAllAvailableAssets()
        {
            var allAvailableAssets = await _assetsRepository.GetAllAvailableAssets();
            allAvailableAssets.Should().NotBeNull();
            allAvailableAssets.Assets.Should().NotBeNull()
                .And.NotBeEmpty()
                .And.NotContainNulls();
        }
    }
}