using System.ComponentModel.DataAnnotations;

namespace Holdings.Api.Resources.Account
{
    class AuthenticateRes
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Location { get; set; }
    }
}