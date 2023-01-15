using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Planday.Schedule.Infrastructure.Configuration.Models;
using Planday.Schedule.Infrastructure.Providers.Interfaces;


namespace Planday.Schedule.Infrastructure.Providers
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<HttpConfig> _options;

    
        public ConnectionProvider(HttpClient httpClient, IOptions<HttpConfig> options)
        {
            _httpClient = httpClient;
            _options = options;
            _httpClient.DefaultRequestHeaders.Add("Authorization", _options.Value.AuthenticationToken);

        }

        public async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken)
        {
            try
            {
                var config = _options.Value;
                var builder = new UriBuilder(Uri.UriSchemeHttp,
                    config.RootUrl,
                    config.Port,
                    path);

                using var response = await _httpClient.GetAsync(builder.Uri, cancellationToken);
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }    
}

