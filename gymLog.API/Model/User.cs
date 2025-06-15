namespace gymLog.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<WorkoutPlan> WorkoutPlans { get; set; }

    }
}
