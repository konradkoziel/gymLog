using gymLog.API.Services.interfaces;
using gymLog.Entity;
using gymLog.Model;
using Microsoft.EntityFrameworkCore;

namespace gymLog.API.Services
{
    public class ExerciseService(AppDbContext context) : BasicCrudService<Exercise>(context), IExerciseService
    {

    }
}