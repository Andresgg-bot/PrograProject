using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Domain.Models.ViewModels
{
    public class CalculateViewModel
    {
        public int basal { get; set; }

        public int calorias { get; set; }

        public int bajar { get; set; }

        public int subir { get; set; }

        public string IdInfo { get; set; }
    }
}
