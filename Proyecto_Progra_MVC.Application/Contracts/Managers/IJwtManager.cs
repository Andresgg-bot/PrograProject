using Proyecto_Progra_MVC.Domain.Models.PlainModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Application.Contracts.Managers
{
    public interface IJwtManager
    {
        JwtToken Authenticate(JwtUser input);

        bool IsTokenValid(string token, out SecurityToken authorizedToken);
    }
}
