using System.Net.Http.Json;
using PlatformService.Models;

namespace PlatformServiceTests.Integration;

[Collection("Integration")]
public class PostPlatform
{
    private readonly HttpClient _client;

    public PostPlatform()
    {
        _client = new IntegrationFixture().Client;
    }

    [Fact]
    public async Task Post_WhenPostPlatform_GetCreatedPlatform()
    {
        using var postPlatformResponse = await _client.PostAsJsonAsync("/platform", 
            new Platform
            {
                Id = 0, Name = "Dot Net", Publisher = "Microsoft", Cost = "Free"
            }
        );
        Assert.True(postPlatformResponse.IsSuccessStatusCode);
        var postPlatformResult = await postPlatformResponse.Content.ReadFromJsonAsync<Platform>();

        using var getPlatformResponse = await _client.GetAsync($"/platform/id/{postPlatformResult.Id}");
        Assert.True(getPlatformResponse.IsSuccessStatusCode);
        var getPlatformResult = await getPlatformResponse.Content.ReadFromJsonAsync<Platform>();
        
        Assert.Equal(postPlatformResult.Name, getPlatformResult.Name);
    }   
}