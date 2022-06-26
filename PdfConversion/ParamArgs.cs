using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfConversion
{
    public class ParamArgs
    {
        public ParamArgs(string source, string dest, string type)
        {
            SourceName = source;
            DestinationName = dest;
            typeName = type;
        }
        public string SourceName { get; set; }
        public string DestinationName { get; set; }
        public string typeName { get; set; }
    }
}
