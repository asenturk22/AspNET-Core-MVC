using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        //Injection
        private readonly RepositoryDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        private readonly ICategoryRepository _categoryRepository;
        public RepositoryManager(
            RepositoryDbContext context,
            IProductRepository productRepository, 
            ICategoryRepository categoryRepository,
            IOrderRepository orderRepository
        )
        {
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
        }

        public IProductRepository Product => _productRepository;
        public ICategoryRepository Category => _categoryRepository;
        public IOrderRepository Order => _orderRepository;
        public void Save()
        {
           _context.SaveChanges();
        }
    }
}