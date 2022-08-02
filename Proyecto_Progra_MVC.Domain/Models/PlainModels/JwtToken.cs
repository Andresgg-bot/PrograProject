using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Domain.Models.PlainModels
{
    public class JwtToken
    {
        [Required]
        public string Token { get; set; }
    }
}
