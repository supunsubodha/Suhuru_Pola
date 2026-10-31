public class User
{
<<<<<<< HEAD
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Role { get; set; }

    public User(int userId, string userName, string role)
    {
        UserId = userId;
        UserName = userName;
        Role = role;
    }
=======
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string NIC { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
>>>>>>> d8d409568f88564d647c8098b139d07db5186974
}