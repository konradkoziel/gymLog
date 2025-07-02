using gymLog.API.Model;
using Microsoft.EntityFrameworkCore;

namespace gymLog.API.Entity
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(x => x.Id);
                u.Property(x => x.Name).HasColumnType("varchar(100)");
                u.Property(x => x.Weight).HasColumnType("decimal(18,2)");
                u.Property(x => x.Height).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<RefreshToken>(rt =>
            {
                rt.HasKey(x => x.Id);
                rt.Property(x => x.Token).HasColumnType("varchar(88)");
                rt.HasOne(x => x.User)
                  .WithMany(u => u.RefreshTokens)
                  .HasForeignKey(x => x.UserId);
            });

            modelBuilder.Entity<Exercise>(ex =>
            {
                ex.HasKey(x => x.Id);
                ex.Property(x => x.Name).HasColumnType("varchar(100)");
                ex.Property(x => x.LastMaxWeight).HasColumnType("decimal(18,2)");
                ex.HasOne(x => x.WorkoutDay)
                  .WithMany(wd => wd.Exercises)
                  .HasForeignKey(x => x.WorkoutDayId)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WorkoutPlan>(wp =>
            {
                wp.HasKey(x => x.Id);
                wp.Property(x => x.Name).HasColumnType("varchar(100)");
                wp.HasMany(wp => wp.Days)
                  .WithOne(wd => wd.WorkoutPlan)
                  .HasForeignKey(wd => wd.WorkoutPlanId);
                wp.HasOne(wp => wp.User)
                  .WithMany(u => u.WorkoutPlans)
                  .HasForeignKey(wp => wp.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WorkoutDay>(wd =>
            {
                wd.HasKey(x => x.Id);
                wd.HasOne(wd => wd.WorkoutPlan)
                  .WithMany(wp => wp.Days)
                  .HasForeignKey(wd => wd.WorkoutPlanId);
                wd.HasMany(wd => wd.Exercises)
                  .WithOne(ex => ex.WorkoutDay)
                  .HasForeignKey(ex => ex.WorkoutDayId);
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
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
