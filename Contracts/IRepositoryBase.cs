using Entities.RequestFeatures;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(RequestParameters parameters, bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, RequestParameters parameters, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
