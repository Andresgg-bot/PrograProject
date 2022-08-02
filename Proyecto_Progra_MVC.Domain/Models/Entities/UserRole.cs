using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Domain.Models.Entities
{
    public class UserRole
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool Selected { get; set; }
    }
}
