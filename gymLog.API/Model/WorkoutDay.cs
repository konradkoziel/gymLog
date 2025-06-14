namespace gymLog.Model
{
    public class WorkoutDay
    {
        public Guid Id { get; set; }
        public DayOfWeek Day { get; set; }
        public string Description { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}
