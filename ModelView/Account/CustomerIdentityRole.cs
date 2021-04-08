using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManhaleAspNetCore.ModelView.Account
{
    public class CustomerIdentityRole:IdentityRole
    {
        public int PowerOfRole { get; set; }
    }
}
