using FluentValidation;
using OrganizerApp.BllDtos.Projects;
using OrganizerApp.ValidationCommunications.Project;

namespace OrganizerApp.WebApi.Infrastructure.Validation
{
    public class ProjectDetailValidator : AbstractValidator<ProjectDetail>
    {
        public ProjectDetailValidator()
        {
            RuleFor(project => project.ID)
                .GreaterThanOrEqualTo(0)
                .WithMessage(LocalizedText.IdRange);

            RuleFor(project => project.Name)
                .NotEmpty()
                .WithMessage(LocalizedText.NameRequired);

            RuleFor(project => project.Priority)
                .Matches("^low$|^medium$|^high$")
                .WithMessage(LocalizedText.PriorityAcceptedValues);

            RuleFor(project => project.ExecutionTime)
                .Matches("^next$|^scheduled$|^someday$")
                .WithMessage(LocalizedText.ExecutionTimeAcceptedValues);

            RuleFor(project => project.StartTime)
                .NotEmpty()
                .When(project => project.ExecutionTime == "scheduled")
                .WithMessage(LocalizedText.StartTimeRequired);

            RuleFor(project => project.State)
               .Matches("^todo$|^done$|^deleted$")
               .WithMessage(LocalizedText.StateAcceptedValues); 

        }
    }
}