using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDWorkFlow.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Summary { get; set; }

    }
}
