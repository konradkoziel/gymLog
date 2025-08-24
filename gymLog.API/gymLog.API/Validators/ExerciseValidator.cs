using FluentValidation;
using gymLog.API.Model.DTO.ExerciseDto;

namespace gymLog.API.Validators
{
    public class ExerciseValidator : AbstractValidator<CreateExerciseDto>
    {
        public ExerciseValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Exercise name is required.")
                .MaximumLength(100).WithMessage("Exercise name must not exceed 100 characters.")
                .MinimumLength(2).WithMessage("Exercise name must be at least 2 characters long.");
            RuleFor(x => x.Reps)
                .Must(x => x > 0).WithMessage("Reps must be greater than zero.");
        }

    }
}
