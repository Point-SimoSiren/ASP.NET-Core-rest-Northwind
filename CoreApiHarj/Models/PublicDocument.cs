using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiHarj.Models
{
    public class PublicDocument
    {
        public int DocumentationId { get; set; }
        public string AvailableRoute { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }

    }
}

