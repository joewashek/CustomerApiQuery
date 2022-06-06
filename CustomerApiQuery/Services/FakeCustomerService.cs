using CustomerApiQuery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiQuery.Services
{
    public class FakeCustomerService : ICustomerService
    {
        public FakeCustomerService()
        {

        }
        public async Task<IEnumerable<Customer>> GetCustomersAsync(int? count)
        {
            return await Task
                .FromResult(
                    new List<Customer>()
                    {
                        new Customer{Name = "Cust1",Id = 1},
                        new Customer{Name = "Cust2",Id = 2}
                    }
                );
        }

    }
}
