using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ProducRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProducRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public  async Task<Product> GetProductByIDAsync(int id)
        {
            return await _context.Products
                   .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(  p => p.Id == id );

        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {

            var typeId = 1;

            var products = _context.Products.Where(x => x.ProductTypeId == typeId); 

            return await _context.Products
                .Include(p =>p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();

        }

        public async Task<IReadOnlyList<ProductType>> GetProductTyprsAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}
