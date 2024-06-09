
- Proje şablonlarının listesini görmek için

    > dotnet new list  

komutunu çalıştırıyoruz.  Varlıklar (Entities) kütüphane projemizi oluşturacağız. Bunun için,

    > dotnet new classlib -f net8.0.2 -o .\Store\Entites   

komutunu çalıştırıyoruz ve sınıf kütüphanesi oluşturuyoruz.

Solution projesinde oluşturulan projeyi eklemek için ve eklenmiş mi olduğunu öğrenebilmek için aşağıdaki komutları çalıştırırız.

> dotnet sln .\Store.sln list

> dotnet sln  .\Store.sln add .\Entities

Entities/Models/Products.cs

```csharp
namespace Entities.Models;

public class Product
{
    public int ProductId { get; set; }
    public String? ProductName { get; set; } = String.Empty;
    public decimal Price { get; set; }
}
```

varlık dosyasını oluşturuyoruz.  StoreApp deki Models klasorü ve Product.cs dosyasını siliyoruz.

Entities projesi ile StoreApp projesi birbirlerinden bağımsız projeler olduğundan birbirlerini görememektedirler. Bunun için **reference**  işlemini yapmamız gerekiyor.

> dotnet add .\StoreApp reference .\Entities\

Bu komut ile StoreApp projesine Entities projesini reference olarak eklemiş oluyoruz.  

Projemize bu reference işlemininin eklenip eklenmediğini görmek için  .\StoreApp\StoreApp.csproj   dosyasinin içeriğinde aşağıdaki kod yapisinin oluştuğunu görmeliyiz.

```csharp
  <ItemGroup>
    <ProjectReference Include="..\Entities\Entities.csproj" />
  </ItemGroup>
```

StoreApp\Views\_ViewImports.cshtml içerisindeki bilgileri aşağıdaki gibi düzenlemeliyiz.

```csharp
@using StoreApp
@using Entities.Models
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers 
```

Yeni bir Proje oluşturacağız.

```csharp
dotnet new classlib -f net8.0.2 -o .\Store\Repositories
```

projeyi ilişkilendirmek için

```csharp
dotnet sln .\Store add .\Store\Repositories
```

oluşturulan projeyi ekliyoruz.

> dotnet add .\StoreApp reference .\Repositories\

ile projeye reference ekliyoruz.

```csharp
  <ItemGroup>
    <ProjectReference Include="..\Entities\Entities.csproj" />
    <ProjectReference Include="..\Repositories\Repositories.csproj" />
  </ItemGroup>
```

Repositories projesine aşağıdaki paketleri ekliyoruz.

```csharp
dotnet add .\Store\Repositories\ package Microsoft.EntityFrameworkCore  --version 8.0.2

dotnet add .\Store\Repositories\ package Microsoft.EntityFrameworkCore.Sqlite  --version 8.0.2

//paketlerin listesini görmek içn
dotnet list .\Store\Repositories package 
```

yukarıda kurulan iki paketi StoreApp içinden kaldırıyoruz. Bunun için

```csharp
dotnet remove .\Store\StoreApp\ package Microsoft.EntityFrameworkCore  --version 8.0.2

dotnet remove .\Store\StoreApp\ package Microsoft.EntityFrameworkCore.Sqlite  --version 8.0.2

//paketlerin listesini görmek içn
dotnet list .\Store\StoreApp package 
```

StoreApp içindeki RepositoryDbContex dosaysını Repositories projesine taşıyoruz.

```csharp
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class RepositoryDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
            .HasData(
                new Product() { ProductId = 1, ProductName = "Computer", Price = 17_000 },
                new Product() { ProductId = 2, ProductName = "Keyboard", Price = 1_000 },
                new Product() { ProductId = 3, ProductName = "Mouse", Price = 500 },
                new Product() { ProductId = 4, ProductName = "Monitor", Price = 7_000 },
                new Product() { ProductId = 5, ProductName = "Deck", Price = 1_500 }
            );
        }
    }
}
```

Entities projesini Repositories içerisine reference etmek için

```csharp
dotnet add .\Repositories\ reference .\Entities\
```

Migration işlemleri yapalım.

MigrationAssebmly tanımlamalıyız DbContex için bunun için;

```csharp
//Program.cs dosyası içerisinde 
builder.Services.AddDbContext<RepositoryDbContext>(
    options =>   
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlconnection"),
    b => b.MigrationsAssembly("StoreApp")
    
    ));

//yukarıdaki kodu güncellyoruz. ardından aşağıdaki kodu çalıştırıyoruz. 

//dotnet ef migrations init  
dotnet ef migrations startPoint  

//tabloların oluşturulması için aşağıdaki komutu çalıştırırız. 
dotnet ef database update 

```

Projemizde bir DbContext vardı ve doğrudan  Controller'a bağlamıştık.

![alt text](../images/repo.png)

Genellikle DbContex'in üzerine bir interface tanımı yapılır. Bu base bir yapı olur temel crud işlemleri için.  Ardından her bir nesne için interface yapıları oluşturulur. IProduct, ICategory gibi. ve bunları yöneten bir IRepositoryManager yazılır.   Gerek controller gerekse servisler bu IRepositoryManager ile enjekte işlemleri yapılır.

Özellikle repoyu soyutlamak test yazmak konusunda gerçekten bir avantaj sağlamaktadır.

Repositories projesinde interface yapılarını kaullanacağımız için bir Contracts adlı klasör ypaısı oluşturup içerisine interface yapılarını oluşturualım.

```csharp
// IRepositoryBase interface yapısı oluşturuluyor. 

using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IRepositoryBase<T> 
    {
        //Sorgulanabilir formatta ve T tipinde ve izleme parametresi olan FindAll() methodu
        IQueryable<T> FindAll(bool trackChanges); 



        T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges); 
    }
}

```

Temel sınıflar new() istemediğimizden **abstract** olarak tanımlıyoruz.  Temel sınıf olan RepostoryBase sınıfı new'lenemeyecek ancak temel alan sınıflar new'lenebilecek.

**Repositories/RepositoriesBase.cs**

```csharp
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
    where T : class, new()   //tip sınıf ve new() olabilir.  Tipi kısıtlıyoruz. 
    {
        //DI-Dependency Injection
        //protected olarak tanımladık çünkü devralınan sınıflarda da bu değişken kullanılsın. 
        protected readonly  RepositoryDbContext _context;

        protected RepositoryBase(RepositoryDbContext context) 
        {
            _context = context;     //IoC
        }
        //  End - DI
        


        public IQueryable<T> FindAll(bool trackChanges)
        {
            //Değişiklikleri izleme parametresi 
            // true ise ->  _context.Set<T>() ile geri döneceğiz (Yani ef core listeyi izleyecek)
            // false ise -> _context.Set<T>.AsNoTracking() izlenmiyor olarak geri donecek. (Yani ef core listeyi izlemeyecek)
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
```

**Repositories/Contracts/IProductRepositories.cs**

```csharp
using Entities.Models;

namespace Repositories.Contracts 
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        IQueryable<Product> GetAllProducts(bool trackChanges);

        Product? GetOneProduct(int id, bool trackChanges);

    }
}
```

**Repositories/ProductRepositories.cs**

```csharp
using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryDbContext context) : base(context)
        {
        }

        public IQueryable<Product> GetAllProducts(bool trackChanges) => FindAll(trackChanges)


        public Product? GetOneProduct(int id, bool trackChanges)
        {
            return FindByCondition(p => p.ProductId.Equals(id), trackChanges);
        }
    }
}
```

**Repositories/Contracts/IRepositoryManager.cs**

```csharp
namespace Repositories.Contracts 
{
    public interface IRepositorManager 
    {
        IProductRepository Product {get; }

        void Save(); 
    }
}
```

**Repositories/RepositoryManager.cs**

```csharp
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

```

IoC de önce bir kayıt işlemi yapılması gerekiyor. Kayıt işlemi için StoreApp' de Program.cs dosyasından 

```csharp
builder.Services.AddScoped<IRepositorManager, RepositoryManager>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
```
bilgilerini ekliyoruz.  Burada IoC  IRepositoryManger nesnesini görürse birinci parametrede ikinci parametrede verilen RepositoryManger() çalıştırıacak, Eğer IProductRepository nesnesini görürse de  ProductRepository() çalıştırmasını bekleyeceğiz. 

IoC Resolved (Çözme) işlemi için de StoreApp/Controllers/ProductControllers.cs dosyasından  IRepositoryManager enjekte işlemi yapılacak. 

```csharp
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Contracts;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {
        //Dependency Injection = DI pattern
        private readonly IRepositorManager _manager;

        public ProductController(IRepositorManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index() {
            var model = _manager.Product.GetAllProducts(false);
            return View(model);
        } 

        public IActionResult Get(int id)
        {
            var model = _manager.Product.GetOneProduct(id, false);
            return View(model);
        }
    }
}
```
IoC - dispose da kaynakların iade edilmesi işlemidir. Register aşamasında ilgili nesne üretilirken Scoped olarak üretilmişti. 

IoC (Inversion of Control)
    - Register
    - Resolve
    - Dispose

Yeni bir varlık oluşturup interface ayarlarını yapalım. 

ICategoryRepository & CategoryRepository 

```csharp
//Entities projesinin Models klasör altına Category.cs dosyasi
namespace Entities.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public String? CategoryName { get; set; } = String.Empty;
    }
}
```

```csharp
// Repository/Contracts/ICategoryRepository.cs
using Entities.Models;

namespace Repositories.Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        
    }
}
```

```csharp
// IRepositoryManager.cs

namespace Repositories.Contracts 
{
    public interface IRepositorManager 
    {
        IProductRepository Product {get; }
        ICategoryRepository Category {get; }

        void Save(); 
    }
}
```

```csharp
// CategoryRepository.cs
using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryDbContext context) : base(context)
        {
        }
    }
}
```


```csharp
//IoC  Register
builder.Services.AddScoped<IRepositorManager, RepositoryManager>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
```

```csharp
//RepositoryManager.cs
    private readonly ICategoryRepository _categoryRepository;
    public RepositoryManager(
        RepositoryDbContext context,
        IProductRepository productRepository, 
        ICategoryRepository categoryRepository 
    )
    {
        _context = context;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    //ifadeleri ekleniyor. 
```
```csharp
//CategoryController.cs
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace StoreApp.Controllers
{
    public class CategoryController : Controller 
    {
        private IRepositorManager _manager;

        public CategoryController(IRepositorManager manager)
        {
            _manager = manager;
        }
    }
}
```
```csharp
//RepositoryDbContext.cs aşağıdaki kodu ekliyoruz. 

            modelBuilder.Entity<Category>()
            .HasData(
                new Category() {CategoryId = 1, CategoryName="Book"},
                new Category() {CategoryId = 2, CategoryName="Elektronik"}

            );

// migration islemi icin asagidaki kodu calistiriyoruz. 
dotnet ef migrations add Category
dotnet ef database update
```
oluşturulan Categories tablosunun ve içine eklenen verileri görmek için; 

```csharp
> sqlite3 ProductDb.db 
sqlite3 > .tables    //Oluşturulan tabloların listesini gösterir. 
sqlite3 > .mode box  //kutu görünümüne geç
sqlite3 > select * from categories;      
```
```csharp
```
```csharp
```
```csharp
```
```csharp
```
```csharp
```
```csharp
```
```csharp
```
```csharp
```
