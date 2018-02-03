using FluentValidation;
using OrganizerApp.BllDtos.Projects;
using OrganizerApp.ValidationCommunications;

namespace OrganizerApp.WebApi.Infrastructure.Validation
{
    public class ProjectDetailValidator : AbstractValidator<ProjectDetail>
    {
        public ProjectDetailValidator()
        {
            RuleFor(project => project.ID)
                .GreaterThanOrEqualTo(0)
                .WithMessage(Project.IdRange);

            RuleFor(project => project.Name)
                .NotEmpty()
                .WithMessage(Project.NameRequired);

            RuleFor(project => project.Priority)
                .Matches("^low$|^medium$|^high$")
                .WithMessage(Project.PriorityAcceptedValues);

            RuleFor(project => project.ExecutionTime)
                .Matches("^next$|^scheduled$|^someday$")
                .WithMessage(Project.ExecutionTimeAcceptedValues);

            RuleFor(project => project.StartTime)
                .NotEmpty()
                .When(project => project.ExecutionTime == "scheduled")
                .WithMessage(Project.StartTimeRequired);

            RuleFor(project => project.State)
               .Matches("^todo$|^done$|^deleted$")
               .WithMessage(Project.StateAcceptedValues); 

        }
    }
}