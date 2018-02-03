using FluentValidation;
using OrganizerApp.BllDtos.Tasks;
using OrganizerApp.ValidationCommunications;


namespace OrganizerApp.WebApi.Infrastructure.Validation
{
    public class TaskDetailValidator : AbstractValidator<TaskDetail>
    {
        public TaskDetailValidator()
        {
            RuleFor(task => task.Name)
                .NotEmpty()
                .WithMessage(Task.NameRequired);

            //RuleFor(task => task.ID)
            //    .NotEmpty() //typ int nigdy nie będzie empty, a 0 jest prawidłową wartością
            //    .WithMessage(Task.IdRequired);

            RuleFor(task => task.ID)
                .GreaterThanOrEqualTo(0)
                .WithMessage(Task.IdRange);

            RuleFor(task => task.Priority)
                .Matches("^low$|^medium$|^high$")
                .WithMessage(Task.PriorityAcceptedValues);

            RuleFor(task => task.ExecutionTime)
                .Matches("^next$|^scheduled$|^someday$")
                .WithMessage(Task.ExecutionTimeAcceptedValues);

            RuleFor(task => task.StartTime)
                .NotEmpty()
                .When(task => task.ExecutionTime == "scheduled")
                .WithMessage(Task.StartTimeRequired);

            RuleFor(task => task.State)
                .Matches("^todo$|^done$|^deleted$")
                .WithMessage(Task.StateAcceptedValues);
        }
    }
}