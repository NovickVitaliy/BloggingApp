using System.Net;
using System.Text.Json;
using BloggingApp.Web.Models.Main;
using BloggingApp.Web.ServicesContracts;

namespace BloggingApp.Web.Services;

public class CountriesService : ICountriesService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public CountriesService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Country?> GetByName(string? name)
    {
        if (name == null)
        {
            return null;
        }

        using var client = _httpClientFactory.CreateClient();

        HttpRequestMessage message = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://restcountries.com/v3.1/name/{name}?fullText=true")
        };
        var response = await client.SendAsync(message);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            string json = await response.Content.ReadAsStringAsync();

            JsonDocument jsonDocument = JsonDocument.Parse(json);

            JsonElement rootElement = jsonDocument.RootElement;
            var firstElement = rootElement.EnumerateArray().First();
            string commonName = firstElement.GetProperty("name").GetProperty("common").ToString();
            string flagUrl = firstElement.GetProperty("flags").GetProperty("png").ToString();
            return new Country()
            {
                Name = commonName,
                FlagUrl = flagUrl
            };
        }

        return null;
    }
}