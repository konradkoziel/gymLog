using FakeItEasy;
using gymLog.API.Model.DTO;
using gymLog.API.Services;
using gymLog.API.Services.interfaces;
using gymLog.Entity;
using gymLog.Model;
using Microsoft.EntityFrameworkCore;

namespace gymLog.Tests.ServicesTests;

public class ExerciseServiceTests {
    private readonly AppDbContext _context;
    private readonly IExerciseService _exerciseService;
    
    public ExerciseServiceTests() {
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _context = new AppDbContext(options);
        _exerciseService = new ExerciseService(_context);
    }

    #region CreateExerciseTests

     [Fact]
    public async Task CreateExercise_ShouldCreateExercise_WhenExerciseIsValid() {
        // Arrange
        WorkoutDay workoutDay = new WorkoutDay() {
            Id = Guid.NewGuid(),
            Day = DayOfWeek.Friday,
            Description = "Test",
            Exercises = []
        };
        
        Exercise validExercise = new Exercise {
            Id = Guid.NewGuid(),
            Category = BodyPartCategories.FullBody,
            Description = "Test description",
            Name = "Full Body Exercise",
            WorkoutDayId = workoutDay.Id,
            WorkoutDay = workoutDay
        };
        
        // Act
        Result<Exercise> result = await _exerciseService.CreateAsync(validExercise);
        
        // Assert
        Assert.True(result.IsSuccess);
        Exercise exerciseFromDb = await _context.Exercises.FindAsync(validExercise.Id);
        Assert.NotNull(exerciseFromDb);
        Assert.Equal(exerciseFromDb.Id, validExercise.Id);
    }

    public static IEnumerable<object[]> InvalidExercises() => new List<object[]> {
        new object[] {
            new Exercise {Id = Guid.NewGuid(), Category = BodyPartCategories.FullBody, Description = "Test description",  Name = "", WorkoutDay = A.Fake<WorkoutDay>()},
            "Exercise has no name"
        },
        new object[] {
            new Exercise {Id = Guid.NewGuid(), Category = BodyPartCategories.FullBody, Description = "Test description",  Name = "Test", WorkoutDay = null},
            "Exercise has no workout day"
        },
        new object[] {
            new Exercise {Id = Guid.NewGuid(), Category = null, Description = "Test description",  Name = "Test", WorkoutDay = A.Fake<WorkoutDay>()},
            "Exercise has no category"
        },
    };

    [Theory]
    [MemberData(nameof(InvalidExercises))]
    public async Task CreateExercise_ShouldNotCreateExercise_WhenExerciseIsInvalid(Exercise invalidExercise, string errorMessage) {
        // Act
        Result<Exercise> result = await _exerciseService.CreateAsync(invalidExercise);
        
        // Assert
        Assert.False(result.IsSuccess);
        
        Exercise exerciseFromDb = await _context.Exercises.FindAsync(invalidExercise.Id);
        Assert.Null(exerciseFromDb);
        
        Assert.Equal(errorMessage, result.Message);
    }

    #endregion
    
    #region GetAllExercisesTests
    
    [Fact]
    public async Task GetAllExercises_ShouldReturnAllExercises_WhenListIsNotEmpty() {
        // Arrange
        IEnumerable<Exercise> exampleExercises = Enumerable.Range(0, 10)
            .Select(i => new Exercise {
                Id = Guid.NewGuid(),
                Category = BodyPartCategories.Abs,
                Description = "Test description",
                WorkoutDay = new WorkoutDay {
                    Id = Guid.Empty,
                    Day = DayOfWeek.Friday,
                    Description = "Test",
                    Exercises = []
                },
                WorkoutDayId = Guid.Empty,
                Name = $"Exercise {i}"
            });
        
        _context.Exercises.AddRange(exampleExercises);
        await _context.SaveChangesAsync();
        
        // Act
        IEnumerable<Exercise> exercises = await _exerciseService.GetAllAsync();
        
        // Assert
        Assert.NotNull(exercises);
        Assert.Equal(10, exercises.Count());
    }
    
    [Fact]
    public async Task GetAllExercises_ShouldReturnAllExercises_WhenListIsEmpty() {
        // Act
        IEnumerable<Exercise> exercises = await _exerciseService.GetAllAsync();
        
        // Assert
        Assert.NotNull(exercises);
        Assert.Empty(exercises);
    }
    
    #endregion
}