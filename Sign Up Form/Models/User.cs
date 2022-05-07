using System.ComponentModel.DataAnnotations;
namespace Sign_Up_Form.Models
{
    public class User
    {
        //Models validatoins
        [Required (ErrorMessage ="Please Enter First Name")]
        [StringLength (50)]
        public String firstname { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name")]
        [StringLength(50)]
        public String lastname { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [StringLength(50)]
        public String email { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(50)]
        public String New_password { get; set; }
     
       
        public String gender { get; set; }
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public int id { get; set; }
        
        public DateTime date { get; set; }
     
    }
}
