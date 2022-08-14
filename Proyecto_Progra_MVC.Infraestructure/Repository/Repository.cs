using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Infraestructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        public Repository(DbContext dbContext)
        {
            isDisposed = false;

            Context = dbContext;
            dbSet = Context.Set<TEntity>();
        }

        readonly DbContext Context;
        readonly DbSet<TEntity> dbSet;

        bool isDisposed;

        IQueryable<TEntity> Query
        {
            get { return dbSet; }
        }

        public IEnumerable<TEntity> Listar(Expression<Func<TEntity, bool>> filtro = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> ordemiento = null, string propiedadesIncluidas = "")
        {
            IQueryable<TEntity> query = Query;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            foreach (var propiedad in propiedadesIncluidas.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(propiedad);
            }

            if (ordemiento != null)
            {
                return ordemiento(query).ToList();
            }

            return query.ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? Filter = null, string? includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (Filter != null)
            {
                query = query.Where(Filter);
            }

            if (includeProperties != null)
            {

                foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.ToList();
        }

        public TEntity Obtener(object id)
        {
            return dbSet.Find(id);
        }

        public TEntity Obtener(Expression<Func<TEntity, bool>> filtro)
        {
            return Query.FirstOrDefault(filtro);
        }

        public void Insertar(TEntity entidad)
        {
            if (entidad == null)
            {
                throw new ArgumentNullException("entidad");
            }

            dbSet.Add(entidad);
        }

        public void Actualizar(TEntity entidad)
        {
            if (entidad == null)
            {
                throw new ArgumentNullException("entidad");
            }

            dbSet.Update(entidad);
        }

        public void Borrar(object id)
        {
            TEntity entidad = Obtener(id);
            if (entidad == null)
            {
                throw new KeyNotFoundException($"{id}");
            }
            Borrar(entidad);
        }

        public void Borrar(TEntity entidad)
        {
            if (entidad == null)
            {
                throw new ArgumentNullException("entidad");
            }

            dbSet.Remove(entidad);
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                if (Context != null)
                {
                    Context.Dispose();
                }
            }

            isDisposed = true;
        }
    }
}
