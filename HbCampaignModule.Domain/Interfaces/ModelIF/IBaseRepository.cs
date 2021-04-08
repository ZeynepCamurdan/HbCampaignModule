using HbCampaignModule.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HbCampaignModule.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class, new()
    {
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        IQueryable<T> AllIncludingAsQueryable(params Expression<Func<T, object>>[] includeProperties);
        T GetSingle(Expression<Func<T, bool>> predicate);
        void UpdateWithCommit(T entity);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Commit();
    }
}
