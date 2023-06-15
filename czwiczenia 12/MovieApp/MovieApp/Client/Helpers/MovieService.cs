using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static MovieApp.Client.Helpers.MovieService;

namespace MovieApp.Client.Helpers
{
    public class MovieService : IMovieService
    {
        public interface IMovieService
        {
            Task<MovieResponse<object>> Delete(string url);
            Task<MovieResponse<T>> Get<T>(string url);
            Task<MovieResponse<object>> Post<T>(string url, T data);
            Task<MovieResponse<Response>> Post<T, Response>(string url, T data);
            Task<MovieResponse<object>> Put<T>(string url, T data);
        }   

        private readonly HttpClient httpClient;

        private JsonSerializerOptions defaultJsonSerializerOptions =>
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public MovieService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<MovieResponse<T>> Get<T>(string url)
        {
            var responseHTTP = await httpClient.GetAsync(url);

            if (responseHTTP.IsSuccessStatusCode)
            {
                var response = await Deserialize<T>(responseHTTP, defaultJsonSerializerOptions);
                return new MovieResponse<T>(response, true, responseHTTP);
            }
            else
            {
                return new MovieResponse<T>(default, false, responseHTTP);
            }
        }

        public async Task<MovieResponse<object>> Post<T>(string url, T data)
        {
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, stringContent);
            return new MovieResponse<object>(null, response.IsSuccessStatusCode, response);
        }

        public async Task<MovieResponse<object>> Put<T>(string url, T data)
        {
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(url, stringContent);
            return new MovieResponse<object>(null, response.IsSuccessStatusCode, response);
        }

        public async Task<MovieResponse<Response>> Post<T, Response>(string url, T data)
        {
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                var responseDeserialized = await Deserialize<Response>(response, defaultJsonSerializerOptions);
                return new MovieResponse<Response>(responseDeserialized, true, response);
            }
            else
            {
                return new MovieResponse<Response>(default, false, response);
            }
        }

        public async Task<MovieResponse<object>> Delete(string url)
        {
            var responseHTTP = await httpClient.DeleteAsync(url);
            return new MovieResponse<object>(null, responseHTTP.IsSuccessStatusCode, responseHTTP);
        }

        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, options);
        }
    }
}