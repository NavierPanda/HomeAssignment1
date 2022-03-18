using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace HomeAssignment.Task4.Services
{
    internal class BaseGraphQlRepository
    {
        private readonly string _baseUrl;
        private readonly IGraphQLWebsocketJsonSerializer _jsonSerializer;

        protected BaseGraphQlRepository(string baseUrl)
        {
            _baseUrl = baseUrl;
            _jsonSerializer = new NewtonsoftJsonSerializer();
        }

        protected async Task<TResponse> GetResultsFromQuery<TResponse>(GraphQLRequest requestedQuery)
        {
            using var graphQlClient = new GraphQLHttpClient(_baseUrl, _jsonSerializer);
            var graphQlResponse = await graphQlClient.SendQueryAsync<TResponse>(requestedQuery);
            return graphQlResponse.Data;
        }
    }
}