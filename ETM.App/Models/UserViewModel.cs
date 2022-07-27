
using System.Threading.Channels;

namespace ETM.App.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Middlename { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Passcode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Channels Channel { get; set; }
        public UserViewModel()
        {
            this.Fullname = FirstName + " " + Middlename + " " + Lastname;
        }
    }
}
