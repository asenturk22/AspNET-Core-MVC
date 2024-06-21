using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Services.Contracts;

namespace StoreApp.Pages
{
    public class CartModel : PageModel
    {
        private readonly IServiceManager _manager; 
        public Cart Cart { get; set; } //IoC
        public CartModel(IServiceManager manager, Cart cart)
        {
            _manager = manager;
            Cart = cart;
        }
        public string ReturnUrl { get; set; } = "/";    
        public void OnGet(string returnUrl)
        {
            //?? ifadesi,   eğer referans almışsa returnUrl bilgsini ata almamışsa "\" ana dizine git.
            ReturnUrl = returnUrl ?? "/";  
        }

        public IActionResult OnPost(int productId, string returnUrl)
        {
            Product? product = _manager.ProductService.GetOneProduct(productId, false); 
            
            if (product is not null)
            {
                Cart.AddItem(product, 1); 
            }
            return Page();   //returnUrl
        }

        public IActionResult OnPostRemove(int id, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl => cl.Product.ProductId.Equals(id)).Product);
            return Page();
        }
    }
}