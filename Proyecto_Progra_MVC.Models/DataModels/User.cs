using Microsoft.AspNetCore.Identity;
using Proyecto_Progra_MVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Models.DataModels
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string Lastname { get; set; }

        public int Age { get; set; }

        [Display(Name = "Physical Activity")]
        public string PhysicalActivity { get; set; }

        [Display(Name = "Gender")]
        public Generos Genero { get; set; }
    }
}
