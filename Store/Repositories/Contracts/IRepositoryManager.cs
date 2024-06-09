namespace Repositories.Contracts 
{
    public interface IRepositorManager 
    {
        IProductRepository Product {get; }

        void Save(); 
    }
}