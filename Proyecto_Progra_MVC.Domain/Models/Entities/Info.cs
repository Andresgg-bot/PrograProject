using Proyecto_Progra_MVC.Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Domain.Models.Entities
{
    public class Info
    {
        [Key]
        public int IdInfo { get; set; }

        public float Weight { get; set; }

        public int Height { get; set; }

        public int Age { get; set; }

        [Display(Name = "Physical Activity")]
        public PhysicalActivity PhysicalActivity { get; set; }

        [Display(Name = "Gender")]
        public Genders Gender { get; set; }

        public DateTime InfoDate { get; set; }

        public User User { get; set; }

    }
}
