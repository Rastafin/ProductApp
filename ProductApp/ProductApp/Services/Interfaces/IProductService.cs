﻿using ProductApp.Models;

namespace ProductApp.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
