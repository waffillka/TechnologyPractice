using Contracts;
using Entities;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);

        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(RequestParameters parameters, bool trackChanges) =>
            !trackChanges ? RepositoryContext.Set<T>()
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .AsNoTracking()
            : RepositoryContext.Set<T>()
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

        //public IQueryable<T> FindCount(RequestParameters parameters, bool trackChanges) =>
        //    !trackChanges ? RepositoryContext.Set<T>()
        //        .Skip((parameters.PageNumber - 1) * parameters.PageSize)
        //        .Take(parameters.PageSize).AsNoTracking() 
        //    : RepositoryContext.Set<T>()
        //        .Skip((parameters.PageNumber - 1) * parameters.PageSize)
        //        .Take(parameters.PageSize);

        public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, RequestParameters parameters, bool trackChanges) =>
            !trackChanges ? RepositoryContext.Set<T>()
                .Where(expression)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .AsNoTracking()
            : RepositoryContext.Set<T>()
                .Where(expression)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
    }
}
