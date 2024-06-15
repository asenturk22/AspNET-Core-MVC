using System.Reflection;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class RepositoryDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options)
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*
            modelBuilder.ApplyConfiguration(new ProductConfig()); 
            modelBuilder.ApplyConfiguration(new CategoryConfig());

            Bu sekilde kullanilabilecegi gibi asagidaki gibi de kullanabiliriz. 
            */

            //Calisan Assembly' deki ifadeleri (Tip configurasyonlarini) otomatik almasi sagla. 
            //Yeni tip tanimlamada ilgili ifade bu kisimda cozulecek. 

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
        }
    }
}