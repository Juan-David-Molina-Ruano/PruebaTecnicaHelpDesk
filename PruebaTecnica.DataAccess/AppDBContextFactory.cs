using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.DataAccess
{
    public class AppDBContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            const string url = "Server=DESKTOP-5EB3FOG;Database=PRUEBACORTA;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(url);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
