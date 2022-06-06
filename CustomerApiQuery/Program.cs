// See https://aka.ms/new-console-template for more information

using CustomerApiQuery.Models;
using CustomerApiQuery.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CustomerApiQuery
{
    class Program
    {
        private const string EXPORT_APP_KEY = "ExportPathWithFileName";
        static async Task Main(string[] args)
        {
            Console.WriteLine("***Customer API Query***");

            bool validInput = false;
            int customerCount = 0;

            while (!validInput)
            {
                Console.WriteLine("Enter the number of customers to returned:");
                string? enteredCustomerCount = Console.ReadLine();

                if (int.TryParse(enteredCustomerCount, out customerCount))
                    validInput = true;
                else
                    Console.WriteLine("Invalid Input. Please enter a valid integer.");
            }

            Console.WriteLine($"You entered {customerCount}.");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<ICustomerService, IntacctSdkCustomerService>();
                    services.AddTransient<IExportService, CsvExportService>();
                })
                .Build();

            var customerService = host.Services.GetRequiredService<ICustomerService>();
            var exportService = host.Services.GetRequiredService<IExportService>();
            var config = host.Services.GetRequiredService<IConfiguration>();

            var exportPath = config?.GetValue<string>(EXPORT_APP_KEY) ?? "";
                
            var customers = await customerService.GetCustomersAsync(customerCount);

            await exportService.ExportCustomersAsync(customers, exportPath);

            
        }

    }
}