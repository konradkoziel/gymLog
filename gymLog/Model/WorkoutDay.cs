namespace gymLog.Model
{
    public class WorkoutDay
    {
        public string Id { get; set; }
        public DayOfWeek Day { get; set; }
        public string Description { get; set; }

        public ICollection<Exercise> Exercises { get; set; }

    }
}
