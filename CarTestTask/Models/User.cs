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

        //#region DB substitute
        //private static System.Collections.Generic.List<User> Users;

        //public static System.Collections.Generic.List<User> GetUsers()
        //{
        //    if (Users == null)
        //    {
        //        Users = new System.Collections.Generic.List<User>()
        //        {
        //            new User() { UserName = "Admin", Password = "12345"},
        //            new User() { UserName = "User", Password = "123456"},
        //        };
        //    }
        //    return Users;
        //}
        //#endregion
    }
}
