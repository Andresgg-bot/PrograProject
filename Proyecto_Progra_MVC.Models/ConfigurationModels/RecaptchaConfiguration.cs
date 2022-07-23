using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Models.ConfigurationModels
{
    public class RecaptchaConfiguration
    {
        public string SiteKey { get; set; }

        public string SecretKey { get; set; }
    }
}
