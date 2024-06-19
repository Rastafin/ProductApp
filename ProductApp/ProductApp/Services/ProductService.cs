using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ProductApp.Data;
using ProductApp.Models;
using ProductApp.Services.Interfaces;

namespace ProductApp.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Find(id)!;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public void UpdateProduct(Product product)
        {
            var trackedEntity = _context.Products.Local.FirstOrDefault(e => e.Id == product.Id);
            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }

            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
