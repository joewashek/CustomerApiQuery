using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiQuery.Intacct
{
    public class ClientConfig
    {
        public string SenderId { get; set; } = "";
        public string SenderPassword { get; set; } = "";
        public string CompanyId { get; set; } = "";
        public string UserId { get; set; } = "";
        public string UserPassword { get; set; } = "";

    }
}
