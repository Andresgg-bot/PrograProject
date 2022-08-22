using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Application.Handlers
{
    public enum PermissionTypes
    {
        [Description(PermissionTypesNames.VIEWROLES)]
        VIEWROLES,

        [Description(PermissionTypesNames.WRITEROLES)]
        WRITEROLES,

        [Description(PermissionTypesNames.MANAGEROLES)]
        MANAGEROLES
    }
}
