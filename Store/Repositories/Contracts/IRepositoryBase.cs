using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IRepositoryBase<T> 
    {
        IQueryable<T> FindAll(bool trackChanges); 

        //Geri dönüş değeri T veya Null olabiliir. 
        //1. parametresi expression olan ve bir fonksiyon geri dönüş değeri T veya bool olabilir. 2. parametresi ef core tarafından izlenme yapılıp yapılmayacağı bilgisidir. 
        T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges); 

        void Create(T entity);

        void Remove(T entity);

        void Update(T entity);
    }
}