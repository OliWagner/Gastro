using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GastroPages.Models
{
    public class LoginModel
    {
        [Display(Name = "Email", ResourceType = typeof(ResourcesGastro.Home.Logon))]
        [Required(ErrorMessageResourceName = "ErrorUsername", ErrorMessageResourceType = typeof(ResourcesGastro.Home.Logon))]
        public string Email { get; set; }

        [Display(Name = "Passwort", ResourceType = typeof(ResourcesGastro.Home.Logon))]
        [Required(ErrorMessageResourceName = "ErrorPasswort", ErrorMessageResourceType = typeof(ResourcesGastro.Home.Logon))]
        public string Password { get; set; }
    }
}