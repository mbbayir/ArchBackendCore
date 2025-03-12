using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchBackend.Core.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="UserName is Required")]
        [Display(Name ="UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Password is Reguired")]
        [Display(Name ="Password")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
