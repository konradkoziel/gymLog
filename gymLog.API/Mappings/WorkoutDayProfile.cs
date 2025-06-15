using AutoMapper;
using gymLog.API.Model.DTO.WorkoutDay;
using gymLog.Model;

namespace gymLog.API.Mappings
{
    public class WorkoutDayProfile : Profile
    {
        public WorkoutDayProfile()
        {
            CreateMap<WorkoutDay, GetExerciseDto>();
            CreateMap<GetExerciseDto, WorkoutDay>();
        }
    }
}
