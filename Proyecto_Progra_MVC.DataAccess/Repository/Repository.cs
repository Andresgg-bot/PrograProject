using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.DataAccess.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        readonly DbContext _dbContext;
        readonly DbSet<TEntity> _dbSet;
        bool isDisposed;

        public Repository(DbContext dbContext)
        {
            isDisposed = false;
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        IQueryable<TEntity> Query
        {
            get { return _dbSet; }
        }

        public IEnumerable<TEntity> List(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, string IncludedProperties = "")
        {
            IQueryable<TEntity> query = Query;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var propiedad in IncludedProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(propiedad);
            }

            if (order != null)
            {
                return order(query).ToList();
            }

            return query.ToList();
        }

        public TEntity Get(object id)
        {
            return _dbSet.Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return Query.FirstOrDefault(filter);
        }

        public void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entidad");
            }

            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entidad");
            }

            _dbSet.Update(entity);
        }

        public void Delete(object id)
        {
            TEntity entidad = Get(id);
            if (entidad == null)
            {
                throw new KeyNotFoundException($"{id}");
            }
            Delete(entidad);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entidad");
            }

            _dbSet.Remove(entity);
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                }
            }

            isDisposed = true;
        } 

    }
}
