using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Core.Contracts;
using WebShopApp.Data;
using WebShopApp.Infrastrucutre.Data.Domain;

namespace WebShopApp.Core.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext _context;
        public StatisticsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public int CountClients()
        {
            return _context.Users.Count();

        }
        public int CountProducts()
        {
            return _context.Products.Count();
        }
        public int CountOrders()
        {
            return _context.Orders.Count();
        }
        public decimal SumOrders()
        {
           var suma = _context.Orders.Sum(x=> x.TotalPrice);

            return suma;
        }




    }
}
