
using keviofocus_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace keviofocus_backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Cycle> Cycles { get; set; }
        public DbSet<FocusTask> Tasks { get; set; }
        public DbSet<StudyMaterial> StudyMaterials { get; set; }
    }
}
