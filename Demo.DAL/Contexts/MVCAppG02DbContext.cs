﻿using Demo.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    public class MVCAppG02DbContext:IdentityDbContext<ApplicationUser>
    {
        public MVCAppG02DbContext(DbContextOptions<MVCAppG02DbContext> options):base(options)
        {

        }
        

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
