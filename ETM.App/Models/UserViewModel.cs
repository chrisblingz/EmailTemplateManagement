
namespace ETM.App.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Passcode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
