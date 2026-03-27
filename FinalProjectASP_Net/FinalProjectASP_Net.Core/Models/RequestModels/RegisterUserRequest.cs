namespace FinalProjectASP_Net.Core.Models.RequestModels;

public class RegisterUserRequest
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}