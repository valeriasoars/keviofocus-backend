using Microsoft.EntityFrameworkCore;

namespace keviofocus_backend.Data
{
    public class KevioDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public KevioDbContext(DbContextOptions<KevioDbContext> options): base(options) { }

    }
}
