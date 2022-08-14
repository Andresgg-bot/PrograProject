using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Infraestructure.Repository
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> Listar(Expression<Func<TEntity, bool>> filtro = null, Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> ordemiento = null, string propiedadesIncluidas = "");

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? Filter = null, string? includeProperties = null);

        TEntity Obtener(object id);

        TEntity Obtener(Expression<Func<TEntity, bool>> filtro);

        void Insertar(TEntity entidad);
        
        void Actualizar(TEntity entidad);

        void Borrar(object id);

        void Borrar(TEntity borrar);
    }
}
