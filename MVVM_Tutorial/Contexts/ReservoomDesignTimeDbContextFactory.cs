namespace MVVM_Tutorial.Contexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

internal class ReservoomDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReservoomDbContext>
{
    public ReservoomDbContext CreateDbContext(string[] args)
    {
        DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data source=reservoom.db").Options;

        return new ReservoomDbContext(options);
    }
}
