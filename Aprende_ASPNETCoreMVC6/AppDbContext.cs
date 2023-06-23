using Microsoft.EntityFrameworkCore;

namespace Aprende_ASPNETCoreMVC6
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
