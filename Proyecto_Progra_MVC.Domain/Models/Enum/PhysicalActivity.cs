using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Domain.Models.Enum
{
    public enum PhysicalActivity
    {
        [Display(Name = "Sedentary")]
        SEDENTARY = 1,
        [Display(Name = "Light")]
        LIGHT = 2,
        [Display(Name = "Moderate")]
        MODERATE = 3,
        [Display(Name = "Intense")]
        INTENSE = 4,
        [Display(Name = "Very Intense")]
        VERYINTENSE = 5
    }
}
