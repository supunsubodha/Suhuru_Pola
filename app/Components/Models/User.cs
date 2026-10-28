public class User
{   
    public int Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string NIC { get; set; }
    public bool IsVerified { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}