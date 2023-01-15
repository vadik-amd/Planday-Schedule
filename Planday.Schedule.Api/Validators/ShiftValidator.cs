using FluentValidation;
using Planday.Schedule.Api.Models;

namespace Planday.Schedule.Api.Validators
{
    public class ShiftValidator : AbstractValidator<ShiftModel>
    {
        public ShiftValidator()
        {
            RuleFor(s => s)
                .Must(s => s.End > s.Start)
                .WithMessage("The start time must not be greater than the end time.");
            RuleFor(s => s)
                .Must(s => s.Start.Date == s.End.Date)
                .WithMessage("Start and end time should be in the same day.");
        }
    }
}

