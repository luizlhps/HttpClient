using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

namespace TodoApi.TodoApi;

public class Root
{
    public int userId { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public bool completed { get; set; }
}

public class Res
{
    public int id { get; set; }

}





public class TodoService(
    IHttpClientFactory httpClientFactory,
    ILogger<TodoService> logger)
{
    public async Task<Root[]> GetUserTodosAsync(int userId)
    {
        using HttpClient client = httpClientFactory.CreateClient();
        // Create the client

        try
        {
            // Make HTTP GET request
            // Parse JSON response deserialize into Todo types
            var todos = await client.GetFromJsonAsync<Root[]>(
                $"https://jsonplaceholder.typicode.com/todos?userId={userId}",
                new JsonSerializerOptions(JsonSerializerDefaults.Web));

            return todos ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError("Error getting something fun to say: {Error}", ex);
        }

        return [];
    }
    public async Task<Res> CreateItemAsync(Root todoItem)
    {
        using HttpClient client = httpClientFactory.CreateClient();

        using var httpResponseMessage =
            await client.PostAsJsonAsync("https://jsonplaceholder.typicode.com/posts", new JsonSerializerOptions(JsonSerializerDefaults.Web));

        httpResponseMessage.EnsureSuccessStatusCode();

        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Res>(
        new JsonSerializerOptions(JsonSerializerDefaults.Web));

        return result ?? new Res { id = 0 };
    }
}