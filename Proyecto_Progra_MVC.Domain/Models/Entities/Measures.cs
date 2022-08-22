using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Domain.Models.Entities
{
    public class Measures
    {
        [Key]
        public int IdMeasure { get; set; }

        public float LeftArm { get; set; }

        public float RightArm { get; set; }

        public float LeftLeg { get; set; }

        public float RightLeg { get; set; }

        public float Waist { get; set; }

        public float Chest { get; set; }

        public DateTime MeasureDate { get; set; }

        public User User { get; set; }

    }
}
