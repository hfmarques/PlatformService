using System.Net;
using System.Net.Http.Json;
using PlatformService.Dtos;

namespace PlatformServiceTests.Integration;

[Collection("Integration")]
public class PlatformApiTests
{
    private readonly HttpClient _client;

    public PlatformApiTests()
    {
        _client = new IntegrationFixture().Client;
    }
    
    [Fact]
    public async Task Get_WhenIdDoNotExists_ReturnsNotFound()
    {
        using var getPlatformResponse = await _client.GetAsync($"/platform/id/{999}");
        Assert.False(getPlatformResponse.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, getPlatformResponse.StatusCode);
    }
    
    [Fact]
    public async Task Post_WhenPostPlatform_ReturnsCreated()
    {
        using var postPlatformResponse = await _client.PostAsJsonAsync("/platform", 
            new PlatformCreateDto()
            {
                Name = "Dot Net", Publisher = "Microsoft", Cost = 0
            }
        );
        Assert.True(postPlatformResponse.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Created,postPlatformResponse.StatusCode);
    }

    [Fact]
    public async Task Post_WhenPostPlatform_GetCreatedPlatform()
    {
        using var postPlatformResponse = await _client.PostAsJsonAsync("/platform", 
            new PlatformCreateDto()
            {
                Name = "Dot Net", Publisher = "Microsoft", Cost = 0
            }
        );
        Assert.True(postPlatformResponse.IsSuccessStatusCode);
        var postPlatformResult = await postPlatformResponse.Content.ReadFromJsonAsync<PlatformReadDto>();

        using var getPlatformResponse = await _client.GetAsync($"/platform/id/{postPlatformResult.Id}");
        Assert.True(getPlatformResponse.IsSuccessStatusCode);
        var getPlatformResult = await getPlatformResponse.Content.ReadFromJsonAsync<PlatformReadDto>();
        
        Assert.Equal(postPlatformResult.Name, getPlatformResult.Name);
    }   
    
    [Fact]
    public async Task Get_WhenCalled_GetPlatforms()
    {
        using var getPlatformResponse = await _client.GetAsync("/platform");
        Assert.True(getPlatformResponse.IsSuccessStatusCode);
        var getPlatformResult = await getPlatformResponse.Content.ReadFromJsonAsync<List<PlatformReadDto>>();
        
        Assert.NotEmpty(getPlatformResult);
    }   
}