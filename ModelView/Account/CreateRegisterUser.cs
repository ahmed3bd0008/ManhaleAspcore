using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManhaleAspNetCore.ModelView.Account
{
    public class CreateRegisterUser
    {
        [Required]
        public String Name { get; set; }
        [EmailAddress]
        public String Email { get; set; }
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="password not match")]
        public String ConfirmPassword { get; set; }
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
      //  [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]

        public String Phone { get; set; }
    }
}
