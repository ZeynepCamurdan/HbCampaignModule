using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HbCampaignModule.Domain.Entities;
using HbCampaignModule.Domain.Interfaces;
using HbCampaignModule.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HbCampaignModule.Infrastructure.Repository.ModelRepos
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        private readonly PostgreSqlDbContext _context;

        public IQueryable<T> Table => throw new NotImplementedException();

        public IQueryable<T> TableNoTracking => throw new NotImplementedException();

        public BaseRepository(PostgreSqlDbContext context)
        {
            _context = context;
        }

        public T GetSingle(Expression<Func<T, bool>> predicate) => _context.Set<T>().FirstOrDefault(predicate);

        public virtual void Add(T entity)
        {
            var dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Added;
            _context.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            var dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            var dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void Commit() => _context.SaveChanges();

        public virtual IQueryable<T> AllIncludingAsQueryable(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public virtual void UpdateWithCommit(T entity)
        {
            Update(entity);
            _context.SaveChanges();
        }
    }
}
