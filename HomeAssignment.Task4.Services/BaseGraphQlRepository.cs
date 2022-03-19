using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace HomeAssignment.Task4.Services
{
    /// <summary>
    /// GraphQl based data sources
    /// </summary>
    internal class BaseGraphQlRepository
    {
        private readonly string _baseUrl;
        private readonly IGraphQLWebsocketJsonSerializer _jsonSerializer;

        protected BaseGraphQlRepository(string baseUrl)
        {
            _baseUrl = baseUrl;
            _jsonSerializer = new NewtonsoftJsonSerializer();
        }

        /// <summary>
        /// Evaluate single graphQl query
        /// </summary>
        /// <param name="requestedQuery">request query</param>
        /// <typeparam name="TResponse">Response type</typeparam>
        protected async Task<TResponse> GetResultsFromQuery<TResponse>(GraphQLRequest requestedQuery)
        {
            using var graphQlClient = new GraphQLHttpClient(_baseUrl, _jsonSerializer);
            var graphQlResponse = await graphQlClient.SendQueryAsync<TResponse>(requestedQuery);
            return graphQlResponse.Data;
        }
    }
}