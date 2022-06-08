using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiQuery.Intacct
{
    public class ReadByQueryParameters
    {
        public string Object { get; set; } = "";
        public string Fields { get; set; } = "";
        public string Query { get; set; } = "";
        public int PageSize { get; set; }
    }
}
