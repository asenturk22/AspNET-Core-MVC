![alt text](images/HTTP_000.png)

Protocol : HTTP
Domain Name : www.btkakademi.gov.tr
Path : portal/course/algoritma-programlama-ve-veri-yapilarina


# HTTP Nasıl Çalışır?

![alt text](images/HTTP_001.png)

- Request
    - POST
    - Content Length : 5
    - Content : Hello
- Response
    - Status Code : 201
    - Content Type : text
    - Content

- Stateless (Durumsuzluk)
Sunucu daha önce istek atılıp atılmadığını hatırlamak durumunda değildir. İstemciyle ilgili bilgileri sunucu tutmaz. 

## Request (İstek)  Yapısı

![alt text](images/HTTP_002.png)

![alt text](images/HTTP_003.png)

![alt text](images/HTTP_004.png)

## Response (Cevabın) Yapısı

![alt text](images/HTTP_005.png)

![alt text](images/HTTP_006.png)
![alt text](images/HTTP_007.png)


## Pipeline 

![alt text](images/HTTP_008.png)

## Modern .NET Stack

![alt text](images/HTTP_009.png)

## İçerik

![alt text](images/HTTP_010.png)


## Komutlar 
- > dotnet
- > dotnet --version
- > dotnet new --list                   // dotnet template listesini gösterir. 
- > dotnet mvc -f net6.0  -o Basics     // Basics isminde MVC template li proje oluşturur. <-f net6.0> yazmassak defaul surumle proje oluşturu. 
- > dotnet build                        // Proje derlenmiş olur. 
- > dotnet run                          // Projeyi çalıştırır. 

Proje çalıştırıldıktan sonra localhost:5023  verilen adresten projenin çalıştığı görülecektir. 


## MVC Proje Şablonu

Asp.NET Core' da mvc tasarım modelini kullanarak web uygulamaları geliştirmemizi sağlayan bir çerçevedir. 

![alt text](images/HTTP_011.png)

**Model** : Nesnemizdir, Product, Employee vb. Model bağımsızdır ancak view ve controller, modele bağlı olarak çalışır. 
**Controller** : Denetleme ile ilgili işlemlerin çözüldüğü alan
**view** : Görüntüleme ile ilgili işlemlerin çözüldüğü yer. 

### Coding by Conventon
### Coding Over Configuration

 ![alt text](images/HTTP_012.png)
 ![alt text](images/HTTP_013.png)

![alt text](images/HTTP_014.png)

## Endpoint Yapısını Anlamak

![alt text](images/HTTP_015.png)
![alt text](images/HTTP_016.png)

burda routing ile belirtilen kısım 
- http://localhost:5023
- http://localhost:5023/
- http://localhost:5023/Home
- http://localhost:5023/Home/Index  

hepside aynı sayfaya yönlendirilecektir aksi belirtilmediği sürece. 

## HTML Rendering Yapısını Anlamak

- Yeni Controller Eklemek 
- > Controller/EmployeeController.cs  oluşturuluyor. 

## Model Direktifi (@model)

```csharp
//Index1.cshtml
@model string

<div class="display-3">Index-1</div>
```

Burda kontrollere bu modelin string ile çalışacağını söylemiş olduk. 


Biz dinamik olarak Index1 içerisine veri göndermek istersek yapıyı aşağıdaki gibi yapmalıyız. 

Örnek1 : string bir veriyi sayfaya dinamik olarak gönderelim. 

```csharp
//Models/Employee.cs
namespace   Basics.Models
{
    public class Employee
    {
        public int Id {get; set;}
        public String FirstName {get; set;} = String.Empty;
        public String LastName {get; set;} = String.Empty;
        public String FullName => $"{FirstName} {LastName.ToUpper()}";

        public int Age {get; set;} 
    }
}
```


```csharp
//EmployeeController.cs
using Microsoft.AspNetCore.Mvc;
using Basics.Models;

namespace Basics.Controllers 
{
    public class EmployeeController : Controller
    {
        public IActionResult Index() 
        {
            return View();
        }

        public IActionResult Index1() 
        {
            string message = $"Hello World. {DateTime.Now.ToString()}";
            //1. parametre hangi sayfaya veri göndereceğiz
            //2. parametre gönderilecek mesaj Bu mesajı @Model tutacaktır. 
            return View("Index1", message);
        }

       public ViewResult Index2() 
        {
            var names = new String[] {
                "Ahmet", 
                "Abdulla", 
                "Sahra" 
            };
            return View(names);
        }

        public IActionResult Index3()
        {
            var list = new List<Employee>() {
                new Employee(){Id=1, FirstName="Ahmet", LastName="Demir", Age=20},
                new Employee(){Id=2, FirstName="Mehmet", LastName="Can", Age=20},
                new Employee(){Id=3, FirstName="Hamza", LastName="Dağ", Age=20},

            };
            return View("Index3", list);
        }
    }
 }
```

```csharp
//Index1.cshtml
@model string

<div class="display-3">Index-1</div>
<div class="lead">@Model</div>   //Modeldeki veriyi getir. 
```


Örnek2 : String dizisi dönelim. 


```csharp
//Index2.cshtml 
@model string[]

<div>
    <h3 class="display">Employe</h3>
</div>
<div class="lead">
    @foreach (string name in Model){
        <p>@name</p> 
    }
    
</div>
```

Örnek 3 : Employee modelinin oluşturulması ve modele göre dinamik veri aktarımı 

```csharp
//index3.cshtml
@model List<Employee>

<div>
    <h3 class="display">Employee </h3>
</div>
<div class="lead">
    @foreach (Employee emp in Model)
    {
        <div class="lead my-2">
            <h6>@emp.FullName</h6>
            <p>@emp.Id</p>
            <p><strong>Age:</strong>@emp.Age</p>
        </div>
    }

</div>
```


## CourseApp Projesinin olusturulmasi

```csharp
> dotnet new mvc --output CourseApp
```


![alt text](images/HTTP_017.png)

- Model Binding : Modelin doldurulması. 


## MVC.TagHelpers Nedir?

Etiket yardimcilari olarak dusunulebilir.  Html ogelerini bir 



## dotnet ef    

Entity Framework Core .NET Command-line Tools

Veri Odaklı Geliştirme (Data driven development)

ASP.NET Core Empty Project Template

```csharp
// Proje sablonu olustur. 
> dotnet new sln -o Store
> dotnet new --list
> dotnet new -o ./Store/StoreApp
> dotnet sln --help
> dotnet sln .\Store\ add .\Store\StoreApp\    //StoreApp projesiinin ilgili sln eklendi. 
> PS C:\Users\snt\Desktop\MVC\Store> dotnet sln .\Store.sln list  // ilglli projenin eklendiğini göreceksiniz. 
```

## SQLite 
- wwww.sqlite.org sitesinden downlod edip kullanabilriz. 

## NuGet 

- NuGet, .NET için resmi paket yöneticisidir.  
- Paket yönetimi, paket oluşturmak, yayınlamak ve var olan paketleri tüketmek üzere araçlar (tools) ve platform karşılık gelir.  
- Paket, derlenmiş kütüphaneler ve tanımlayıcı Metadata ifadelerini kapsar. 
![alt text](images/sqlite.png)

- www.nuget.org
- CLI: 
```csharp
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.2
    dotnet yüklü paketlerin listesini görmek için 
    dotnet list package  CLI dan çalıştırıoruz. 
    dotnet add package Microsoft.EntityFrameworkCore --version 8.0.2
```
![alt text](images/nuget.png)

![alt text](images/Ef1.png)
![alt text](images/Ef2.png)

```csharp
//Desing paketinin yüklenmesi
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.2
```


## .NET Tools 

```csharp
// net tools listelemek için 
// global .net toolları listeler
dotnet tool list -g
dotnet tool install --global dotnet-ef  //ef kurulumu
dotnet ef migrations add init    // migration işlemi gerçekleştiriliyor. 
dotnet ef database update        // database oluşturuluyor. 
sqlite3 .\ProductDb.db           // sqlite3 oturumunu açıyoruz. 
sqlite3> .tables                 // ile olusan tablolari görebiliriz. 

sqlite3 ./ProductsDb.db          // sqlite3 yi ProductsDB üzerine konumlandiryoruz. 
sqlite3> .show                   // sqlite3 ayarlar gözükür
sqlite3> .mode box               // list modundan kutu moduna geçer
sqlite3> select * from Products; // Products tablosunu tablo halinde verleri gösterir. 

```

CLI komutu ile kurulum gerceklestirilir. 

- dotnet ef migrations add init  
komutu çalıştırılır. 


### Inversion of Control (IOC)

![alt text](images/ioc.png)

Middleware'larda servisler kullanılıyor, bu servisler bir yada daha fazla blok içerisinde kullanılabilir servise ait bağımlılığın çözülmesi için IOC kaydının yapılması gerekir. 


## LAYOUT 

![alt text](images/layout.png)

- _ViewStart.cshtml
- _ViewImports.cshtml


## Partial View

Eğer controller bazında kısmı görüntü üzerinde çalışılacaksa Shared klasörünün altına aksi durumda Home yada Product gibi ilgili controller ait klasör altına eklenmesi olaydır. 

## Client-side Library Acquisition


## libman  

.net tool'u dur.  

kurulum için
```csharp
 dotnet tool install -g Microsoft.Web.LibraryManager.Cli
```

komutu çalıştırılır. 

libman init -p cdnjs   komutu çalıştırılr. 

- libman install bootstrap -d wwwroot/lib/boostrap
- libman install font-awesome -d wwwroot/lib/font-awesome 
- libman install jquery -d wwwroot/lib/jquery

 komutu ile kütüphane son güncel versiyonunu verilen konuma kopyalar. 


![alt text](images/repo.png)


# Repository Pattern 
- Entities Project
Entites = Varlıklar = Tablolarımızdır. 
