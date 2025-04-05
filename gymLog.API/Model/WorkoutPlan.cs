namespace gymLog.Model
{
    public class WorkoutPlan
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<DayOfWeek> WorkoutSelectedDays { get; set; }
        public ICollection<WorkoutDay> WorkoutDays { get; set; }
        public User User { get; set; }
    }
}
