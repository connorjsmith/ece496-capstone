﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PillDispenserWeb.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace PillDispenserWeb.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions)  {
        }
    }
}
