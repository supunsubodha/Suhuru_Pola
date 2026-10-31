public class User
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string NIC { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}