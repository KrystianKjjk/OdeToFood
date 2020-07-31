using System;
using Microsoft.EntityFrameworkCore;
using OdeToFood.core;

namespace OdeToFood.Data
{
    public class OdeToFoodDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options)
            : base(options)
        {
        }
    }
}