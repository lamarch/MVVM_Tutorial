namespace MVVM_Tutorial.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ReservoomDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReservoomDbContext>
    {
        public ReservoomDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data source=reservoom.db").Options;

            return new ReservoomDbContext(options);
        }
    }
}
