using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DiabloCms.MsSql
{
    public class CmsDbContextFactory : IDesignTimeDbContextFactory<CmsDbContext>
    {
        private const string MsSqlConnectionString = @"Data Source=localhost;Initial Catalog=DemoBase;Integrated Security=True";

        private const string PostgresqlConnectionString = @"Host=localhost;Port=5432;Database=DemoBase;Username=sa;Password=sa;";

        public CmsDbContext CreateDbContext(string[] args)
            => CreateCmsDbContext();

        public static CmsDbContext CreateCmsDbContext()
        {
            // MsSql
            //var options = new DbContextOptionsBuilder<CmsDbContext>()
            //    .UseSqlServer(MsSqlConnectionString);
            
            // Progress
            var options = new DbContextOptionsBuilder<CmsDbContext>()
                .UseNpgsql(PostgresqlConnectionString);

            return new CmsDbContext(options.Options);
        }
    }
}