using System.ComponentModel.DataAnnotations;

namespace Holdings.Api.Resources.Account
{
    public class RegisterRes
    {
        [Required]
        public string Username { get; set; }

        public string Location { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
