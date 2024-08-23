using Microsoft.EntityFrameworkCore;

namespace WebApp.BDD
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {

        }
    }
}
