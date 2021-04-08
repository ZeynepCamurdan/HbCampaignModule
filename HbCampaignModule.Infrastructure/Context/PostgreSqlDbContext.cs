

using HbCampaignModule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HbCampaignModule.Infrastructure.Context
{
    public class PostgreSqlDbContext : DbContext
    {

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }

        public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : base(options)
        {

        }


    }
}