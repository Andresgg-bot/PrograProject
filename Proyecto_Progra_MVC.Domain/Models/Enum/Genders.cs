using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Domain.Models.Enum
{
    public enum Genders
    {
        [Display(Name = "Male")]
        MALE = 1,
        [Display(Name = "Female")]
        FEMALE = 2
    }
}
