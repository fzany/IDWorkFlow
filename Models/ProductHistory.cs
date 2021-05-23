using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDWorkFlow.Models
{
    public class ProductHistory
    {
        public string Id { get; set; }
        public string Action { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public Product Product { get; set; }
    }
}
