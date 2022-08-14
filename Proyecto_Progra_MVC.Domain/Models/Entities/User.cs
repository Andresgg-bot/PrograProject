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
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public Calories Calories { get; set; }

    }
}
