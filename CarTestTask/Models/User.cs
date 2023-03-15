using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarTestTask.Models
{
    public class User
    {
        [Required]
        [DefaultValue("Admin")]
        public string UserName { get; set; }

        [Required]
        [DefaultValue("12345")]
        public string Password { get; set; }
    }
}
