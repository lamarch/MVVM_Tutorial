namespace MVVM_Tutorial.Contexts;

using Microsoft.EntityFrameworkCore;

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
