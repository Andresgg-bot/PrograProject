using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Domain.Models.ViewModels
{
    public class CalculateViewModel
    {
        [Display(Name = "Basal metabolism:")]
        public int basal { get; set; }

        [Display(Name = "Calories needed to maintain weight:")]
        public int calorias { get; set; }

        [Display(Name = "Calories to lose weight: ")]
        public int bajar { get; set; }

        [Display(Name = "Calories to gain weight:")]
        public int subir { get; set; }

        public string IdInfo { get; set; }
    }
}
