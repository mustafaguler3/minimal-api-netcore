using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using StudentEnrollment.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollment.Data
{
    public class VtContext : IdentityDbContext<User>
    {
        public VtContext(DbContextOptions<VtContext> options):base(options)
        {
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<BaseEntity>().HasNoKey();
            builder.Entity<Student>().HasKey(i=>i.Id);
            builder.Entity<Course>().HasKey(i=>i.Id);
            builder.Entity<Enrollment>().HasNoKey();


            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }

    public class VtContextFactory : IDesignTimeDbContextFactory<VtContext>
    {
        public VtContext CreateDbContext(string[] args)
        {
            //GetEnvironment
            //string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            //Build config
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            //get connection string
            var optionsBuilder = new DbContextOptionsBuilder<VtContext>();
            var connectionString = config.GetConnectionString("SqlCon");
            optionsBuilder.UseSqlServer(connectionString);

            return new VtContext(optionsBuilder.Options);   
        }
    }
}
