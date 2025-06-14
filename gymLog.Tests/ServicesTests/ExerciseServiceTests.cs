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
    
    public static IEnumerable<object[]> InvalidExercises() {
        WorkoutDay workoutDay = new WorkoutDay {
            Id = Guid.NewGuid(),
            Description = "Test",
            Day = DayOfWeek.Friday,
            Exercises = []
        };
            
        return new List<object[]> {
            new object[] {
                new Exercise {
                    Id = Guid.NewGuid(), Category = BodyPartCategories.FullBody, Description = "Test description",
                    Name = "", WorkoutDay = workoutDay, WorkoutDayId = workoutDay.Id
                },
                "Exercise has no name"
            },
            new object[] {
                new Exercise {
                    Id = Guid.NewGuid(), Category = BodyPartCategories.FullBody, Description = "Test description",
                    Name = "Test", WorkoutDay = null
                },
                "Exercise has no workout day"
            },
            new object[] {
                new Exercise {
                    Id = Guid.NewGuid(), Category = null, Description = "Test description", Name = "Test",
                    WorkoutDay = workoutDay, WorkoutDayId = workoutDay.Id
                },
                "Exercise has no category"
            },
        };
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
        Result<IEnumerable<Exercise>> exercisesResult = await _exerciseService.GetAllAsync();
        
        // Assert
        Assert.True(exercisesResult.IsSuccess);
        Assert.NotNull(exercisesResult.Data);
        Assert.Equal(10, exercisesResult.Data.Count());
    }
    
    [Fact]
    public async Task GetAllExercises_ShouldReturnAllExercises_WhenListIsEmpty() {
        // Act
        Result<IEnumerable<Exercise>> exercisesResult = await _exerciseService.GetAllAsync();
        
        // Assert
        Assert.NotNull(exercisesResult.Data);
        Assert.Empty(exercisesResult.Data);
    }
    
    #endregion

    #region GetExerciseByIdTests

    [Fact]
    public async Task GetExerciseById_ShouldReturnExercise_WhenExerciseExists() {
        // Arrange
        WorkoutDay workoutDay = new WorkoutDay() {
            Id = Guid.NewGuid(),
            Day = DayOfWeek.Friday,
            Description = "Test",
            Exercises = []
        };
        
        Exercise exercise = new Exercise {
            Id = Guid.NewGuid(),
            Category = BodyPartCategories.FullBody,
            Description = "Test description",
            Name = "Full Body Exercise",
            WorkoutDayId = workoutDay.Id,
            WorkoutDay = workoutDay
        };
        
        _context.Exercises.Add(exercise);
        await _context.SaveChangesAsync();
        
        // Act
        Result<Exercise> exerciseResult = await _exerciseService.GetByIdAsync(exercise.Id);
        
        // Assert
        Assert.True(exerciseResult.IsSuccess);
        Assert.NotNull(exerciseResult.Data);
        Assert.Equal(exercise.Id, exerciseResult.Data.Id);
    }
    
    [Fact]
    public async Task GetExerciseById_ShouldReturnExercise_WhenExerciseNotExists() {
        // Arrange
        WorkoutDay workoutDay = new WorkoutDay() {
            Id = Guid.NewGuid(),
            Day = DayOfWeek.Friday,
            Description = "Test",
            Exercises = []
        };
        
        Exercise exercise = new Exercise {
            Id = Guid.NewGuid(),
            Category = BodyPartCategories.FullBody,
            Description = "Test description",
            Name = "Full Body Exercise",
            WorkoutDayId = workoutDay.Id,
            WorkoutDay = workoutDay
        };
        
        _context.Exercises.Add(exercise);
        await _context.SaveChangesAsync();
        
        // Act
        Result<Exercise> exerciseResult = await _exerciseService.GetByIdAsync(Guid.NewGuid());
        
        // Assert
        Assert.False(exerciseResult.IsSuccess);
        Assert.Null(exerciseResult.Data);
    }

    #endregion
    
    #region UpdateExerciseTests

    [Fact]
    public async Task UpdateExercise_ShouldUpdateExercise_WhenExerciseExists() {
        // Arrange
        WorkoutDay workoutDay = new WorkoutDay() {
            Id = Guid.NewGuid(),
            Day = DayOfWeek.Friday,
            Description = "Test",
            Exercises = []
        };
        
        Exercise exerciseBeforeUpdate = new Exercise {
            Id = Guid.NewGuid(),
            Category = BodyPartCategories.FullBody,
            Description = "Test description",
            Name = "Full Body Exercise",
            WorkoutDayId = workoutDay.Id,
            WorkoutDay = workoutDay
        };
        
        _context.Exercises.Add(exerciseBeforeUpdate);
        await _context.SaveChangesAsync();
        
        // Act
        exerciseBeforeUpdate.Description = "Test description after update";
        exerciseBeforeUpdate.Category = BodyPartCategories.Arms;
        Result<Exercise> updateResult = await _exerciseService.UpdateAsync(exerciseBeforeUpdate);
        
        // Assert
        Assert.True(updateResult.IsSuccess);
        Assert.NotNull(updateResult.Data);
        Assert.Equal(exerciseBeforeUpdate.Id, updateResult.Data.Id);
        Assert.Equal(BodyPartCategories.Arms, updateResult.Data.Category);
        Assert.Equal("Test description after update", updateResult.Data.Description);
    }

    [Fact]
    public async Task UpdateExercsie_ShouldNotUpdateExercise_WhenExerciseNotExist() {
        // Arrange
        WorkoutDay workoutDay = new WorkoutDay() {
            Id = Guid.NewGuid(),
            Day = DayOfWeek.Friday,
            Description = "Test",
            Exercises = []
        };
        
        Exercise exerciseBeforeUpdate = new Exercise {
            Id = Guid.NewGuid(),
            Category = BodyPartCategories.FullBody,
            Description = "Test description",
            Name = "Full Body Exercise",
            WorkoutDayId = workoutDay.Id,
            WorkoutDay = workoutDay
        };
        
        // Act
        exerciseBeforeUpdate.Description = "Test description after update";
        exerciseBeforeUpdate.Category = BodyPartCategories.Arms;
        Result<Exercise> updateResult = await _exerciseService.UpdateAsync(exerciseBeforeUpdate);
        
        // Assert
        Assert.False(updateResult.IsSuccess);
        Assert.Null(updateResult.Data);
    }

    [Theory]
    [MemberData(nameof(InvalidExercises))]
    public async Task UpdateExercise_ShouldNotUpdateExercise_WhenExerciseIsInvalid(Exercise invalidExercise,
        string errorMessage) {
        // Arrange
        WorkoutDay workoutDay = new WorkoutDay() {
            Id = Guid.NewGuid(),
            Day = DayOfWeek.Friday,
            Description = "Test",
            Exercises = []
        };
        
        Exercise validExercise = new Exercise {
            Id = invalidExercise.Id,
            Category = invalidExercise.Category ?? BodyPartCategories.FullBody,
            Description = invalidExercise.Description,
            Name = invalidExercise.Name ?? "Exercise Name",
            WorkoutDayId = workoutDay.Id,
            WorkoutDay = invalidExercise.WorkoutDay ?? workoutDay,
        };
        _context.Exercises.Add(validExercise);
        await _context.SaveChangesAsync();
        
        // Act
        Result<Exercise> exerciseResult = await _exerciseService.UpdateAsync(invalidExercise);
        
        // Assert
        Assert.False(exerciseResult.IsSuccess);
        Assert.Null(exerciseResult.Data);
        Assert.Equal(exerciseResult.Message, errorMessage);
    }
    
    #endregion

    #region DeleteExerciseTests

    [Fact]
    public async Task DeleteExercise_ShouldDeleteExercise_WhenExerciseExists() {
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
        _context.Exercises.Add(validExercise);
        await _context.SaveChangesAsync();
        
        // Act
        Result<bool> deleteExerciseResult = await _exerciseService.DeleteAsync(validExercise.Id);
        
        // Assert
        Assert.True(deleteExerciseResult.IsSuccess);
        Assert.Empty(_context.Exercises.ToList());
    }

    [Fact]
    public async Task DeleteExercise_ShouldNotDeleteExercise_WhenExerciseDoesNotExist() {
        // Act
        Result<bool> deleteExerciseResult = await _exerciseService.DeleteAsync(Guid.NewGuid());
        
        // Assert
        Assert.False(deleteExerciseResult.IsSuccess);
    }

    #endregion
}