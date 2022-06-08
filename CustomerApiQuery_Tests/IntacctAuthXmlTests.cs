using CustomerApiQuery.Intacct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApiQuery_Tests
{
    public class IntacctAuthXmlTests
    {
        [Fact]
        public void Can_Parse_Session_Id()
        {
            var xmlResponse = @"
						<?xml version=""1.0"" encoding=""UTF-8""?>
						<response>
							<control>
								<status>success</status>
								<senderid>intacct-bnandakumar</senderid>
								<controlid>authRequestId</controlid>
								<uniqueid>false</uniqueid>
								<dtdversion>3.0</dtdversion>
							</control>
							<operation>
								<authentication>
									<status>success</status>
									<userid>xml-jw</userid>
									<companyid>Skyline-JWashek</companyid>
									<locationid></locationid>
									<sessiontimestamp>2022-06-07T21:26:27+00:00</sessiontimestamp>
									<sessiontimeout>2022-06-08T09:26:27+00:00</sessiontimeout>
								</authentication>
								<result>
									<status>success</status>
									<function>getAPISession</function>
									<controlid>authFunctionId</controlid>
									<data>
										<api>
											<sessionid>-kx3QH8F0HsSxEBXjl0A20qdexPEJf5MLJuaGmQhEsRAV45dAPs.</sessionid>
											<endpoint>https://api.intacct.com/ia/xml/xmlgw.phtml</endpoint>
											<locationid></locationid>
										</api>
									</data>
								</result>
							</operation>
						</response>
						";

            var sessionId = AuthRequestXML.ParseSessionId(xmlResponse.Trim());

			Assert.Equal("-kx3QH8F0HsSxEBXjl0A20qdexPEJf5MLJuaGmQhEsRAV45dAPs.", sessionId);
        }
    }
}
