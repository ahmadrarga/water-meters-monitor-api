using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WaterMetersMonitor.Application.Errors;
using WaterMetersMonitor.Application.Exceptions;
using WaterMetersMonitor.Domain.Entities;
using WaterMetersMonitor.Infrastructure.DataContexts;

namespace WaterMetersMonitor.Application.Repositories
{
    public class EntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly SqlDataContext _context;
        protected DbSet<TEntity> DbSet => _context.Set<TEntity>();

        public EntityRepository(SqlDataContext context)
        {
            _context = context;
        }
        public IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] properties)
        {
            return predicate != null
                ? GetQueryWithIncludes(properties).Where(predicate)
                : GetQueryWithIncludes(properties);
        }

        private IQueryable<TEntity> GetQueryWithIncludes(params Expression<Func<TEntity, object>>[] properties)
        {
            var query = DbSet as IQueryable<TEntity>;
            if (properties != null)
            {
                query = properties.Aggregate(query, (current, property) => current.Include(property));
            }

            return query;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.BadRequest, RepositoryErrorCodes.EntityIsNull.ToString());
            }

            if (entity.Created == default)
            {
                entity.Created = DateTime.UtcNow;
            }

            entity.Updated = DateTime.UtcNow;

            DbSet.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new ApiException(System.Net.HttpStatusCode.Conflict, RepositoryErrorCodes.ConstraintsNotMatched.ToString(), e.Message);
            }

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.BadRequest, RepositoryErrorCodes.EntityIsNull.ToString());
            }

            entity.Updated = DateTime.UtcNow;

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new ApiException(System.Net.HttpStatusCode.Conflict, RepositoryErrorCodes.ConstraintsNotMatched.ToString(), e.Message);
            }

            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.NotFound, RepositoryErrorCodes.EntityNotFound.ToString());
            }

            DbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
