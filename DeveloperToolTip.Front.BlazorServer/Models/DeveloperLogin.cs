namespace DeveloperToolTip.Front.BlazorServer.Models
{
    public class DeveloperLogin
    {
        public int Id { get; set; }
        public int DeveloperId { get; set; }
        public string? IpAddress { get; set; }
        public bool IsActive { get; set; }

    }
}
