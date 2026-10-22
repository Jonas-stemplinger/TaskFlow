using Microsoft.EntityFrameworkCore;
using TaskFlow.Models;

namespace TaskFlow.Data
{
    public class TaskFlowDbContext : DbContext
    {
        public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> Users => Set<AppUser>();

        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Dashboard> Dashboards => Set<Dashboard>();

        public DbSet<TaskModelDashboard> DashboardTodos => Set<TaskModelDashboard>();

        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        public DbSet<TodoItem> Todos => Set<TodoItem>();

        public DbSet<Teamproject> Teamprojects => Set<Teamproject>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) //Wenn der Parent gelöscht wird dann gibt es nen Kaskaden Fall quasi der die Kinder auch löscht.
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Teamproject>()
                .HasMany(t => t.Users)
                .WithMany(u => u.Teamprojects);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TodoItem>()
                .HasOne(t => t.TaskItem)
                .WithMany(task => task.Todos)
                .HasForeignKey(t => t.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Dashboard>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Dashboard>()
                .HasIndex(d => d.UserId)
                .IsUnique();

            modelBuilder.Entity<TaskModelDashboard>()
                .HasOne(t => t.Dashboard)
                .WithMany(d => d.Todos)
                .HasForeignKey(t => t.DashboardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskModelDashboard>()
                .HasOne(t => t.Todo)
                .WithMany()
                .HasForeignKey(t => t.TodoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TaskModelDashboard>()
                .HasIndex(t => new { t.DashboardId, t.TodoId })
                .IsUnique();
        }
    }
}
