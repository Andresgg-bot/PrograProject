using Proyecto_Progra_MVC.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Models.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            InputModel = new LoginInputModel();
        }

        public LoginInputModel InputModel { get; set; }

        public string SiteKey { get; set; }

        public string Recaptcha { get; set; }
    }
}
