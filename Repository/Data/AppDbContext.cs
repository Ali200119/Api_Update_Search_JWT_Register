using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Repository.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            //modelBuilder.ApplyConfiguration(new CountryConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            modelBuilder.Entity<Employee>().HasQueryFilter(e => !e.SoftDelete);
            modelBuilder.Entity<Country>().HasQueryFilter(c => !c.SoftDelete);
            modelBuilder.Entity<City>().HasQueryFilter(c => !c.SoftDelete);


            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FullName = "Roya Meherremova",
                    Address = "Sumqayit",
                    Age = 27
                },

                new Employee
                {
                    Id = 2,
                    FullName = "Anar Aliyev",
                    Address = "Xetai",
                    Age = 28
                },

                new Employee
                {
                    Id = 3,
                    FullName = "Mubariz Agayev",
                    Address = "Nesimi",
                    Age = 18
                }
            );

            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Azerbaijan"

                },

                new Country
                {
                    Id = 2,
                    Name = "Turkey"
                },

                new Country
                {
                    Id = 3,
                    Name = "USA"
                }
            );

            modelBuilder.Entity<City>().HasData(
                new City
                {
                    Id = 1,
                    Name = "Baku",
                    CountryId = 1
                },

                new City
                {
                    Id = 2,
                    Name = "Kurdemir",
                    CountryId = 1
                },

                new City
                {
                    Id = 3,
                    Name = "Ankara",
                    CountryId = 2
                },

                new City
                {
                    Id = 4,
                    Name = "Istanbul",
                    CountryId = 2
                },

                new City
                {
                    Id = 5,
                    Name = "New-York",
                    CountryId = 3
                },

                new City
                {
                    Id = 6,
                    Name = "Washington",
                    CountryId = 3
                }
            );
        }

    }
}
