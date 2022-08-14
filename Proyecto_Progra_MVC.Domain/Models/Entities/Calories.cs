using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Domain.Models.Entities
{
    public class Calories
    {
        [Key]
        public int Id { get; set; }

        public int BasalMetabolism { get; set; }

        public int MaintainCalories { get; set; }

        public int LoseWeightCalories { get; set; }

        public int GainWeightCalories { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }

    }
}
