using CsvHelper;
using CustomerApiQuery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiQuery.Services
{
    public class CsvExportService : IExportService
    {
        public Task ExportCustomersAsync(IEnumerable<Customer> customers, string exportPath)
        {
            using var sw = new StreamWriter(exportPath);
            using var cw = new CsvWriter(sw,System.Globalization.CultureInfo.InvariantCulture);
            return cw.WriteRecordsAsync(customers);
        }
    }
}
