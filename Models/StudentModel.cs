using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullProjectUsingIdentity.Models
{
    public class StudentModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Enter Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please Enter Your Email")]
        [EmailAddress(ErrorMessage ="Please Enter Valid Email")]
        public string Email {  get; set; }

        [Required(ErrorMessage ="Please Enter Class")]
        public string Class {  get; set; }

        [Required(ErrorMessage ="Please Choose Your Teacher")]
        public string SelectTeacher { get; set; }

        [ForeignKey("SelectTeacher")]
        public ApplicationUser Teacher { get; set; }    
    }
}
