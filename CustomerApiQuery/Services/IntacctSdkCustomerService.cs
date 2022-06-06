using CustomerApiQuery.Models;
using Intacct.SDK;
using Intacct.SDK.Functions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CustomerApiQuery.Services
{
    public class IntacctSdkCustomerService : ICustomerService
    {
        private readonly OnlineClient _client;
        public IntacctSdkCustomerService()
        {
            ClientConfig clientConfig = new ClientConfig()
            {
                ProfileFile = Path.Combine(Directory.GetCurrentDirectory(), "credentials.ini")
            };
            _client = new OnlineClient(clientConfig);
        }
        public async Task<IEnumerable<Customer>> GetCustomersAsync(int? count)
        {
            var customers = new List<Customer>();

            ReadByQuery query = new ReadByQuery()
            {
                ObjectName = "CUSTOMER",
                PageSize = count ?? 10,  
                Fields =
                {
                    "CUSTOMERID",
                    "NAME"
                }
            };

            var response = await _client.Execute(query);

            var result = response.Results[0].Data;
            foreach(var item in result)
            {
                customers.Add(new Customer
                {
                    Id = int.Parse(item.Element("CUSTOMERID")?.Value ?? "0"),
                    Name = item.Element("NAME")?.Value ?? "",
                });
            }
            return customers;
        }
    }
}
