using System.Text.Json.Serialization;
using TaskFlow.Models;

public class AppUser
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    [JsonIgnore]
    public string Password { get; set; } = string.Empty;

    public List<Project> Projects { get; set; } = new();

    public List<Teamproject> Teamprojects { get; set; } = new();
}