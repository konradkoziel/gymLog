using gymLog.Model;
using Microsoft.EntityFrameworkCore;

namespace gymLog.Entity
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(us =>
            {
                us.HasKey(x => x.Id);
                us.Property(x => x.Name).HasColumnType("varchar(100)");
                us.Property(x => x.Weight).HasColumnType("decimal(18,2)");
                us.Property(x => x.Height).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Exercise>(ex =>
            {
                ex.HasKey(x => x.Id);
                ex.Property(x => x.Name).HasColumnType("varchar(100)");
                ex.Property(x => x.Description).HasColumnType("varchar(500)");
                ex.Property(x => x.Category).HasColumnType("varchar(100)");
                ex.HasOne(x => x.WorkoutDay)
                  .WithMany(wd => wd.Exercises)
                  .HasForeignKey("WorkoutDayId");
            });

            modelBuilder.Entity<WorkoutPlan>(wp =>
            {
                wp.HasKey(x => x.Id);
                wp.Property(x => x.Name).HasColumnType("varchar(100)");
                wp.Property(x => x.Description).HasColumnType("varchar(500)");
                wp.HasMany(x => x.WorkoutDays)
                  .WithOne()
                  .HasForeignKey("WorkoutPlanId");
                wp.HasOne(x => x.User)
                  .WithMany(u => u.WorkoutPlans)
                  .HasForeignKey("UserId");
            });

            modelBuilder.Entity<WorkoutDay>(wd =>
            {
                wd.HasKey(x => x.Id);
                wd.Property(x => x.Description).HasColumnType("varchar(500)");
                wd.HasMany(x => x.Exercises)
                  .WithOne(e => e.WorkoutDay)
                  .HasForeignKey("WorkoutDayId");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<WorkoutDay> WorkoutDays { get; set; }
    
    }
}
