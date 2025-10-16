using System.Reflection.Metadata;
using System.Text.Json;
using RecipeShopper.Components.Pages;
using RecipeShopper.Models;

public class Auth
{
    public bool LoggedIn { get; private set; } = false;
    public string? Email { get; private set; } = null;

    private readonly IWebHostEnvironment _env;
    private readonly string path;
    private static readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

    public Auth(IWebHostEnvironment env)
    {
        _env = env;
        path = Path.Combine(_env.WebRootPath, "users.json");
    }

    public void LogIn(string email)
    {
        LoggedIn = true;
        Email = email;
    }

    public void LogOut()
    {
        LoggedIn = false;
        Email = null;
    }

    public async Task<int> CheckLogin(string email, string password)
    {
        Dictionary<string, string> users = await GetUsersAsync();

        if (users.ContainsKey(email))
        {
            if (users[email] == password)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            return 2;
        }                
    }

    public async Task<Dictionary<string, string>> GetUsersAsync()
    {
        var json = await File.ReadAllTextAsync(path);

        return JsonSerializer.Deserialize<Dictionary<string, string>>(json, options);
    }

    public async Task AddUserAsync(string email, string password)
    {
        var users = await GetUsersAsync();
        users[email] = password;

        var updatedUsers = JsonSerializer.Serialize(users, options);
        await File.WriteAllTextAsync(path, updatedUsers);
    }
}