using Proyecto_Progra_MVC.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Domain.Models.ViewModels
{
    public class RoleViewModel
    {
        public IEnumerable<Role> Roles { get; set; }
    }
}
