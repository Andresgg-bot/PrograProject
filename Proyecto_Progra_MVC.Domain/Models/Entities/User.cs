using Microsoft.AspNetCore.Identity;
using Proyecto_Progra_MVC.Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Domain.Models.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string Lastname { get; set; }

        public int Age { get; set; }

        [Display(Name = "Physical Activity")]
        public PhysicalActivity PhysicalActivity { get; set; }

        [Display(Name = "Gender")]
        public Genders Gender { get; set; }

        public List<Progress> Progress { get; set; }
    }
}
