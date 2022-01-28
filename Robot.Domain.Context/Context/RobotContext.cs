using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Robot.DAL.Entities;
using System.IO;


namespace Robot.DAL
{
    public class RobotContext : DbContext
    {
        DbSet<Decision> Decisions { get; set; }

        DbSet<Query> Queries { get; set; }

        DbSet<ScannerField> ScannerFields { get; }

        DbSet<Template> Templates { get; }

        public RobotContext() : base()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {

            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json");

                var config = builder.Build();

                string connString = config.GetConnectionString("Robot");

                optionsBuilder.UseSqlServer(config.GetConnectionString("Robot"), options => options.EnableRetryOnFailure());

                
            }

        }
    }
}
