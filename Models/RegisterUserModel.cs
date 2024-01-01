using System.ComponentModel.DataAnnotations;

namespace FullProjectUsingIdentity.Models
{
    public class RegisterUserModel
    {

        [Required(ErrorMessage ="Please Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please Enter Email")]
        [EmailAddress(ErrorMessage ="Please Enter Valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please Enter Password")]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }

        [Required(ErrorMessage ="Please Confirm Your Password")]
        [Compare("Password",ErrorMessage ="Password Doesn't Match")]

        public string ConfirmPassword { get; set;}

        public Roles Roles { get; set; }
    }

    public enum Roles
    { 
        Admin,
        Teacher
    }
}
