using FluentValidation;
using OrganizerApp.BllDtos.Tasks;
using OrganizerApp.ValidationCommunications.Task;

namespace OrganizerApp.WebUI.Infrastructure.Validation
{
    public class TaskDetailValidator : AbstractValidator<TaskDetail>
    {
        public TaskDetailValidator()
        {
            RuleFor(task => task.Name)
                .NotEmpty()
                .WithMessage(LocalizedText.NameRequired);

            RuleFor(task => task.ID)
                .GreaterThanOrEqualTo(0)
                .WithMessage(LocalizedText.IdRequired);
            
            RuleFor(task => task.Priority)
                .NotEmpty()
                .WithMessage(LocalizedText.PriorityRequired);

            RuleFor(task => task.ExecutionTime)
                .NotEmpty()
                .WithMessage(LocalizedText.ExecutionTimeRequired);

            RuleFor(task => task.StartTime)
                .NotEmpty()
                .When(task => task.ExecutionTime == "scheduled")
                .WithMessage(LocalizedText.StartTimeRequired);

            RuleFor(task => task.State)
                .NotEmpty()
                .WithMessage(LocalizedText.StateRequired);
        }
    }
}