﻿using Microsoft.EntityFrameworkCore;


namespace ManhaleAspNetCore.Models
{
    public class ManahelContext:DbContext
    {
        public ManahelContext(DbContextOptions options ):base(options)
        {

        }
        public DbSet<Images> Images { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Khalias> khaliases { get; set; }
        public DbSet<Manahel> manahels { get; set; }
        public DbSet<Queue> Queues { get; set; }
    }
}
