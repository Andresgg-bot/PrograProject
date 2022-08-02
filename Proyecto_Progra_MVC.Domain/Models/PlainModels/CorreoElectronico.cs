using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Domain.Models.PlainModels
{
    public class CorreoElectronico
    {
        public string Destinatario { get; set; }

        public string Asunto { get; set; }

        public string Cuerpo { get; set; }
    }
}
