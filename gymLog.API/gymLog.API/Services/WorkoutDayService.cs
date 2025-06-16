using gymLog.API.Entity;
using gymLog.API.Model;
using gymLog.API.Services.interfaces;
using gymLog.Entity;
using gymLog.Model;
using Microsoft.EntityFrameworkCore;

namespace gymLog.API.Services
{
    public class WorkoutDayService(AppDbContext context) : BasicCrudService<WorkoutDay>(context), IWorkoutDayService
    {

    }
}