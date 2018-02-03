using FluentValidation;
using OrganizerApp.BllDtos.Projects;
using OrganizerApp.ValidationCommunications;

namespace OrganizerApp.WebUI.Infrastructure.Validation
{
    public class ProjectDetailValidator : AbstractValidator<ProjectDetail>
    {
        public ProjectDetailValidator()
        {
            RuleFor(project => project.Name)
                .NotEmpty()
                .WithMessage(Project.NameRequired);
            
            RuleFor(project => project.Priority)
                .NotEmpty()
                .WithMessage(Project.PriorityRequired);

            RuleFor(project => project.ExecutionTime)
                .NotEmpty()
                .WithMessage(Project.ExecutionTimeRequired);
            
            RuleFor(project => project.StartTime)
                .NotEmpty()
                .When(project => project.ExecutionTime == "scheduled")
                .WithMessage(Project.StartTimeRequired);
        }
    }
}