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
        public  async Task<Product> GetProductByIDAsync(int id)
        {
            return await _context.Products.FindAsync(id);

        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();

        }
    }
}
