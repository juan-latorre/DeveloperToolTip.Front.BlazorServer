namespace DeveloperToolTip.Front.BlazorServer.Models
{
    public class DeveloperDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Login { get; set; }
        public string? PasswordHash { get; set; }
        public int RoleId { get; set; }
        public bool? IsActive { get; set; }
    }
}
