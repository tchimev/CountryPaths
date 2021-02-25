using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryPaths.Shared.Models
{
    internal class CountryPathsContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryPath> CountryPaths { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Temp\\CountryPathsDb.db");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
