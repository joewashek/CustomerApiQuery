using CustomerApiQuery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiQuery.Services
{
    internal class FakeExportService : IExportService
    {
        public Task ExportCustomersAsync(IEnumerable<Customer> customers,string exportPath)
        {
            foreach (var item in customers)
            {
                Console.WriteLine($"{item.Id},{item.Name}");
            }

            return Task.CompletedTask;
        }
    }
}
