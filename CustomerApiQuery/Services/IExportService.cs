using CustomerApiQuery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiQuery.Services
{
    internal interface IExportService
    {
        Task ExportCustomersAsync(IEnumerable<Customer> customers, string exportPath);
    }
}
