using CustomerApiQuery.Intacct;
using CustomerApiQuery.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CustomerApiQuery.Services
{
    public class RawXmlCustomerService : ICustomerService
    {
        private const string ENDPOINT_URL = "https://api.intacct.com/ia/xml/xmlgw.phtml";
        private const int TIMEOUT = 30;
        static readonly HttpClient client = new HttpClient();
        private readonly ClientConfig _config;
        public RawXmlCustomerService()
        {
            var envs = JsonConvert.SerializeObject(Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine));
            var jsonEnvs = JObject.Parse(envs);
            _config = new ClientConfig
            {
                SenderId = jsonEnvs["INTACCT_SENDER_ID"]?.ToString() ?? "",
                SenderPassword = jsonEnvs["INTACCT_SENDER_PASSWORD"]?.ToString() ?? "",
                CompanyId = jsonEnvs["INTACCT_COMPANY_ID"]?.ToString() ?? "",
                UserId = jsonEnvs["INTACCT_USER_ID"]?.ToString() ?? "",
                UserPassword = jsonEnvs["INTACCT_USER_PASSWORD"]?.ToString() ?? ""
            };
        }
        public async Task<IEnumerable<Customer>> GetCustomersAsync(int? count)
        {
            var customers = new List<Customer>();

            var sessionId = await GetSessionAsync();

            var requestBuilder = new XmlRequestBuilder();

            var readByQueryParams = new ReadByQueryParameters
            {
                Object = "CUSTOMER",
                Fields = "CUSTOMERID,NAME",
                PageSize = count ?? 10
            };

            var query = requestBuilder
                .WithSenderIdControlOnly(_config.SenderId, _config.SenderPassword, "controlId")
                .WithReadByQuery(sessionId, readByQueryParams)
                .Build();

            var response = await client.PostAsync(ENDPOINT_URL, XmlContent(query.InnerXml));

            var xmlResponseString = await response.Content.ReadAsStringAsync();
            var xmlResponseDocument = new XmlDocument();
            xmlResponseDocument.LoadXml(xmlResponseString);

            var data = xmlResponseDocument.SelectNodes("/response/operation/result/data/customer");

            if(data != null)
            {
                foreach(XmlNode customerNode in data)
                {
                    customers.Add(new Customer
                    {
                        Id = int.Parse(customerNode.SelectSingleNode("CUSTOMERID")?.InnerText ?? "0"),
                        Name = customerNode.SelectSingleNode("NAME")?.InnerText ?? ""
                    });
                }
            }

            return customers;
        }

        private async Task<string> GetSessionAsync()
        {

            var authXml = AuthRequestXML.GetXML(_config);

            var response = await client.PostAsync(ENDPOINT_URL,XmlContent(authXml));

            var authXmlResponse = await response.Content.ReadAsStringAsync();
            
            return AuthRequestXML.ParseSessionId(authXmlResponse);
        }

        private StringContent XmlContent(string xmlContent)
            => new StringContent(xmlContent, Encoding.UTF8, "application/xml");
        
    }
}
