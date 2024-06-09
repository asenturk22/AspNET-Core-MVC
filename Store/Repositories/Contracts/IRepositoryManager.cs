namespace Repositories.Contracts 
{
    public interface IRepositorManager 
    {
        IProductRepository Product {get; }
        ICategoryRepository Category {get; }

        void Save(); 
    }
}