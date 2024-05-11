using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace AccoSystem.DataLayer;

public class MyDbContext:DbContext
{
    public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("server=localhost;database=AccoSystem_DB;user=sa;password=yourStrong(!)Password;TrustServerCertificate=True");
        }
    }
}
