using gymLog.API.Services;
using gymLog.Entity;
using Microsoft.EntityFrameworkCore;

namespace gymLog.Tests.ServicesTests;

public class WorkoutPlanServiceTests {
    WorkoutPlanService _workoutPlanService;

    public WorkoutPlanServiceTests() {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        AppDbContext dbContext = new AppDbContext(options);
        _workoutPlanService = new WorkoutPlanService(dbContext);
        
        //
    }
}