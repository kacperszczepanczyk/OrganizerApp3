using FluentValidation;
using OrganizerApp.BllDtos.Tasks;
using OrganizerApp.ValidationCommunications.Task;


namespace OrganizerApp.WebApi.Infrastructure.Validation
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
                .WithMessage(LocalizedText.IdRange);

            RuleFor(task => task.Priority)
                .Matches("^low$|^medium$|^high$")
                .WithMessage(LocalizedText.PriorityAcceptedValues);

            RuleFor(task => task.ExecutionTime)
                .Matches("^next$|^scheduled$|^someday$")
                .WithMessage(LocalizedText.ExecutionTimeAcceptedValues);

            RuleFor(task => task.StartTime)
                .NotEmpty()
                .When(task => task.ExecutionTime == "scheduled")
                .WithMessage(LocalizedText.StartTimeRequired);

            RuleFor(task => task.State)
                .Matches("^todo$|^done$|^deleted$")
                .WithMessage(LocalizedText.StateAcceptedValues);
        }
    }
}