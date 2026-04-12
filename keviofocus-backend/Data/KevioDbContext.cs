using keviofocus_backend.Models;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace keviofocus_backend.Data
{
    public class KevioDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public KevioDbContext(DbContextOptions<KevioDbContext> options): base(options) { }


        public DbSet<SessionModel> Sessions { get; set; }
        public DbSet<TaskItemModel> Tasks { get; set; }
        public DbSet<SessionRunModel> SessionRuns { get; set; }
        public DbSet<TaskCompletionModel> TaskCompletions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // 1. Relacionamento Session -> Tasks (1:N)
            // Se a Sessão for deletada, as tarefas configuradas nela também são.
            modelBuilder.Entity<TaskItemModel>()
                .HasOne(t => t.Session)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            // 2. Relacionamento Session -> SessionRun (1:N)
            // Se a Sessão (template) sumir, o histórico de execuções dela também some.
            modelBuilder.Entity<SessionRunModel>()
                .HasOne(sr => sr.Session)
                .WithMany(s => s.Runs)
                .HasForeignKey(sr => sr.SessionId)
                .OnDelete(DeleteBehavior.Cascade);


            // 3. Relacionamento TaskCompletion (Tabela de Junção)
            // Se a Task for deletada, remove o registro de conclusão.
            modelBuilder.Entity<TaskCompletionModel>()
                .HasOne(tc => tc.Task)
                .WithMany(t => t.Completions)
                .HasForeignKey(tc => tc.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            // Se a Execução (Run) for deletada, remove os registros de quais tarefas foram feitas nela.
            modelBuilder.Entity<TaskCompletionModel>()
                .HasOne(tc => tc.SessionRun)
                .WithMany(sr => sr.TaskCompletions)
                .HasForeignKey(tc => tc.SessionRunId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
