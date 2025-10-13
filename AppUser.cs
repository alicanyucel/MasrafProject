public sealed class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }
    public bool IsDeleted { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}