using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterMetersMonitor.Domain.Entities;

namespace WaterMetersMonitor.Infrastructure.DataContexts
{
    public class SqlDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public SqlDataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DomainDB"));
        }

        public DbSet<Group> Groups { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<MainWaterMeter> MainWaterMeters { get; set; }

        public DbSet<MainWaterMeterValue> MainWaterMeterValues { get; set; }

        public DbSet<WaterMeter> WaterMeters { get; set; }

        public DbSet<WaterMeterValue> WaterMeterValues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}
