using FluentValidation;
using OrganizerApp.BllDtos.Projects;
using OrganizerApp.ValidationCommunications.Project;

namespace OrganizerApp.WebUI.Infrastructure.Validation
{
    public class ProjectDetailValidator : AbstractValidator<ProjectDetail>
    {
        public ProjectDetailValidator()
        {
            RuleFor(project => project.Name)
                .NotEmpty()
                .WithMessage(LocalizedText.NameRequired);
            
            RuleFor(project => project.Priority)
                .NotEmpty()
                .WithMessage(LocalizedText.PriorityRequired);

            RuleFor(project => project.ExecutionTime)
                .NotEmpty()
                .WithMessage(LocalizedText.ExecutionTimeRequired);
            
            RuleFor(project => project.StartTime)
                .NotEmpty()
                .When(project => project.ExecutionTime == "scheduled")
                .WithMessage(LocalizedText.StartTimeRequired);
        }
    }
}