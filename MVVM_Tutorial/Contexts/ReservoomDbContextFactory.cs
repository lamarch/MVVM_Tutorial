namespace MVVM_Tutorial.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ReservoomDbContextFactory
    {
        private readonly string connectionString;

        public ReservoomDbContextFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ReservoomDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(connectionString).Options;

            return new ReservoomDbContext(options);
        }
    }
}
