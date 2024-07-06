using Entities.Models;
using Repositories.Contracts;
using Microsoft.EntityFrameworkCore; 
using System.Linq;

namespace Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryDbContext context) : base(context)
        {

        }

        public IQueryable<Order> Orders => _context.Orders
            .Include(o => o.Lines)
            .ThenInclude( cl => cl.Product)
            .OrderBy(o => o.Shipped)
            .ThenByDescending(o => o.OrderId);  

        //Gitmeyen siparişlerin sayisini verir.     
        public int NumberOfInProcess => _context.Orders.Count(o => o.Shipped.Equals(false));
        public Order? GetOneOrder(int id)
        {
            return FindByCondition(o => o.OrderId.Equals(id), false); 
        }
        public void Complete(int id)
        {
            var order = FindByCondition(o => o.OrderId.Equals(id), true);
            if (order is null) 
                throw new Exception("Order could not found!");
            order.Shipped = true;   
        }
        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderId == 0)
                _context.Orders.Add(order);
            _context.SaveChanges(); 
        }
    }
}