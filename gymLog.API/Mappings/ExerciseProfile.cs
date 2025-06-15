using AutoMapper;
using gymLog.API.Model.DTO;
using gymLog.Model;

namespace gymLog.API.Mappings
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile() 
        {
            CreateMap<Exercise, GetExerciseDto>();
            CreateMap<GetExerciseDto, Exercise>();
        }
    }
}
