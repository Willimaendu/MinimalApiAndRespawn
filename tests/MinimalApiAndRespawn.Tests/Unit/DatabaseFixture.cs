using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MinimalApiAndRespawn.Core.Persistence;

namespace MinimalApiAndRespawn.Tests.Unit;

public class DatabaseFixture : IDisposable
{
    protected IDbContextFactory<AppDbContext> DbContextFactory { get; }

    protected DatabaseFixture() 
    {
        DbContextFactory = new TestDbContextFactory();
        using AppDbContext dbContext = DbContextFactory.CreateDbContext();
        dbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void SetupDatabase(Action<AppDbContext> setupAction)
    {
        using AppDbContext dbContext = DbContextFactory.CreateDbContext();
        setupAction(dbContext);
        dbContext.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing) 
        {
            ((IDisposable)DbContextFactory).Dispose();
        }
    }

    private class TestDbContextFactory : IDbContextFactory<AppDbContext>, IDisposable
    {
        private readonly SqliteConnection _connection;

        public TestDbContextFactory()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
        }

        public AppDbContext CreateDbContext()
        {
            DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            return new AppDbContext(options);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}