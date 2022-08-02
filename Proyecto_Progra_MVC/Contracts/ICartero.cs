using Proyecto_Progra_MVC.Domain.Models.PlainModels;

namespace Proyecto_Progra_MVC.Contracts
{
    public interface ICartero
    {
        void Enviar(CorreoElectronico correo);

    }
}
