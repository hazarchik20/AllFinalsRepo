using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class OrderRepo : ShopReposytory<Order>
    {
        private readonly DataContext _context;
        public OrderRepo(DataContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Order[]> GetByUserIdAsync(int id)
        {
            return await _context.Set<Order>().Include(o => o.Address).Where(o => o.UserId==id).ToArrayAsync();
        }
    }
}
