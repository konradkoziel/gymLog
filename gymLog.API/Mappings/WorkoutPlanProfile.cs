using AutoMapper;
using gymLog.API.Model.DTO.WorkoutDay;
using gymLog.API.Model.DTO.WorkoutPlan;
using gymLog.Model;

namespace gymLog.API.Mappings
{
    public class WorkoutPlanProfile : Profile
    {
        public WorkoutPlanProfile()
        {
            //GET
            CreateMap<WorkoutPlan, GetWorkoutPlanDto>();
            CreateMap<GetWorkoutPlanDto, WorkoutPlan>();

            //CREATE
            CreateMap<CreateWorkoutPlanDto, WorkoutPlan>();
            CreateMap<CreateWorkoutDayDto, WorkoutDay>();
            CreateMap<CreateExerciseDto, Exercise>();
        }
    }
}
