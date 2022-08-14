using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Infraestructure.Repository.UnitOfWork
{
    public interface IUnitOfWork<out TContext>
        where TContext : DbContext
    {
        TContext Context { get; }

        void CrearTransaccion();

        void Commit();

        void Rollback();

        void Guardar();

        IRepository<TEntity> Repository<TEntity>()
            where TEntity : class;

    }
}
