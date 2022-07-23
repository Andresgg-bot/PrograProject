namespace Proyecto_Progra_MVC.Contracts
{
    public interface IRecaptchaValidator
    {
        bool Validate(string token);
    }
}
