using CustomerApiQuery.Intacct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApiQuery_Tests
{
    public class IntacctXmlBuilder_Tests
    {
        [Fact]
        public void Can_Create_Root_Element()
        {
            var requestBuilder = new XmlRequestBuilder();

            var xmlDoc = requestBuilder.Build();

            var root = xmlDoc.FirstChild;

            Assert.NotNull(root);
            Assert.Equal("request", root?.Name);
        }

        [Fact]
        public void Can_Create_ReadByQuery()
        {
            var readByParameters = new ReadByQueryParameters
            {
                Object = "CUSTOMER",
                Fields = "CUSTOMERID",
                PageSize = 5
            };

            var requestBuilder = new XmlRequestBuilder();

            var xmlDoc = requestBuilder
                .WithReadByQuery("",readByParameters)
                .Build();

            var ele = xmlDoc.SelectSingleNode("/request/operation/content/function/readByQuery");

            Assert.NotNull(ele);
            Assert.Equal("CUSTOMER", ele?.ChildNodes[0]?.InnerText);
            Assert.Equal("CUSTOMERID", ele?.ChildNodes[1]?.InnerText);
            Assert.Equal("", ele?.ChildNodes[2]?.InnerText);
            Assert.Equal("5", ele?.ChildNodes[3]?.InnerText);
        }
    }
}
