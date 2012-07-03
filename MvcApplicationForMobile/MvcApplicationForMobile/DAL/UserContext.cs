using System;
using System.Collections.Generic;
using System.Data.Entity;
using MvcApplicationForMobile.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MvcApplicationForMobile.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}