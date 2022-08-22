﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Domain.Models.Entities
{
    public class Progress
    {
        [Key]
        public int Id { get; set; }

        public float Weight { get; set; }

        public int Height { get; set; }

        [Display(Name = "Body Mass Index")]
        public float BMI { get; set; }

    }
}
