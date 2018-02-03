using FluentValidation;
using OrganizerApp.BllDtos.Tasks;
using OrganizerApp.ValidationCommunications;

namespace OrganizerApp.WebUI.Infrastructure.Validation
{
    public class TaskDetailValidator : AbstractValidator<TaskDetail>
    {
        public TaskDetailValidator()
        {
            RuleFor(task => task.Name)
                .NotEmpty()
                .WithMessage(Task.NameRequired);

            RuleFor(task => task.ID)
                .GreaterThanOrEqualTo(0)
                .WithMessage(Task.IdRequired);
            
            RuleFor(task => task.Priority)
                .NotEmpty()
                .WithMessage(Task.PriorityRequired);

            RuleFor(task => task.ExecutionTime)
                .NotEmpty()
                .WithMessage(Task.ExecutionTimeRequired);

            RuleFor(task => task.StartTime)
                .NotEmpty()
                .When(task => task.ExecutionTime == /*GetDataHelpers.Helpers.ExecutionTime.Scheduled*/ "scheduled")
                .WithMessage(Task.StartTimeRequired);

            RuleFor(task => task.State)
                .NotEmpty()
                .WithMessage(Task.StateRequired);
        }
    }
}