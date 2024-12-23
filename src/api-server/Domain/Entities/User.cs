namespace Domain.Entities;

public class User : IBaseEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public Guid? RefreshToken { get; set; }
    public bool IsAdmin { get; set; } = false;
    
    public void GenerateRefreshToken()
    {
        RefreshToken = Guid.NewGuid();
    }
}