using gymLog.API.Model;
using Microsoft.EntityFrameworkCore;

namespace gymLog.API.Entity;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> Users => Set<User>();
    public DbSet<WorkoutPlan> WorkoutPlans => Set<WorkoutPlan>();
    public DbSet<WorkoutDay> WorkoutDays => Set<WorkoutDay>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<ExerciseProgress> ExerciseProgress => Set<ExerciseProgress>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Plans)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkoutPlan>()
            .HasMany(p => p.Days)
            .WithOne(d => d.WorkoutPlan)
            .HasForeignKey(d => d.WorkoutPlan)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkoutDay>()
            .HasMany(d => d.Exercises)
            .WithOne(e => e.WorkoutDay)
            .HasForeignKey(e => e.WorkoutDayId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Exercise>()
            .HasMany(e => e.ProgressHistory)
            .WithOne(p => p.Exercise)
            .HasForeignKey(p => p.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}