using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
    where T : class, new() 
    {
        protected readonly  RepositoryDbContext _context;

        protected RepositoryBase(RepositoryDbContext context) 
        {
            _context = context;
        }
        public IQueryable<T> FindAll(bool trackChanges)
        {
            return trackChanges 
            ? _context.Set<T>() 
            : _context.Set<T>().AsNoTracking();
        }

        public T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges
            ? _context.Set<T>().Where(expression).SingleOrDefault()
            : _context.Set<T>().Where(expression).AsNoTracking().SingleOrDefault(); 
        }
    }
}