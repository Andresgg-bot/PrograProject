using Proyecto_Progra_MVC.Domain.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Services.IServices
{
    public interface IUserServices
    {
        Task<IEnumerable<User>> getUsersAsync();
        Task<User> getUserById(string id);
        Task<User> updateUserById(User user);
        Task<string> deleteUserById(string id);
    }
}
