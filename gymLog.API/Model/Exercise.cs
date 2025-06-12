namespace gymLog.Model
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        
        public Guid WorkoutDayId { get; set; }
        public WorkoutDay WorkoutDay { get; set; }
    }
}
