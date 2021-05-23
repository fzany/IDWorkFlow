using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDWorkFlow.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsWorker { get; set; }

        [PersonalData]
        public string Firstname { get; set; }
        [PersonalData]
        public string Lastname { get; set; }

    }
}
