using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Application.Handlers
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(PermissionTypes permissionTypes)
        {
            PermissionType = permissionTypes;
        }

        public PermissionTypes PermissionType { get; private set; }
    }
}
