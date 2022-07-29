using GoGlobal.Test.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoGlobal.Test.Data;

public class TestDbContext : DbContext
{
    public DbSet<Repository> Repositories { get; set; }
    public string DbPath { get; }
    public TestDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "trivialDb");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}