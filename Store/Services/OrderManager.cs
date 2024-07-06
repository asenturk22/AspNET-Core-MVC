using Repositories.Contracts; 
using Services.Contracts;
using Entities.Models;
using System.Linq;

namespace Services
{
    public class OrderManager : IOrderService
    {
        private readonly IRepositoryManager _manager; 

        public OrderManager(IRepositoryManager manager)
        {
            _manager = manager; 
        }
        public IQueryable<Order> Orders => _manager.Order.Orders; 
        public int NumberOfInProcess => _manager.Order.NumberOfInProcess; 
        public Order? GetOneOrder(int id)
        {
            return _manager.Order.GetOneOrder(id);
        }
        public void Complete(int id)
        {
            _manager.Order.Complete(id); 
            _manager.Save();
        }
        public void SaveOrder(Order order)
        {
            _manager.Order.SaveOrder(order);
        }
    }
}