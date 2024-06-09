using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryManager : IRepositorManager
    {
        private readonly RepositoryDbContext _context;
        private readonly IProductRepository _productRepository;

        public RepositoryManager(IProductRepository productRepository, RepositoryDbContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }

        public IProductRepository Product => _productRepository;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}