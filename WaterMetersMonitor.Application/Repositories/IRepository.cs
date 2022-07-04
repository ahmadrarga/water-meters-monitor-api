using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WaterMetersMonitor.Domain.Entities;

namespace WaterMetersMonitor.Application.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(long id);

        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] properties);
    }
}
