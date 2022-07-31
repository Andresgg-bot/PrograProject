namespace Proyecto_Progra_MVC.Application.Contracts.Managers
{
    public interface IRecaptchaValidator
    {
        bool Validate(string token);
    }
}
