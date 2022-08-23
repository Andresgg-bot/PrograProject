using Proyecto_Progra_MVC.Domain.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Services.IServices
{
    public interface ISupplementsServices
    {
        Task<IEnumerable<Supplements>> getSupplementsAsync();
        Task<Supplements> getSupplementById(int id);
        Task<Supplements> updateSupplementById(Supplements supplement);
        Task<Supplements> addSupplementAsync(Supplements supplement);
        Task<string> deleteSupplementById(int id);
    }
}
