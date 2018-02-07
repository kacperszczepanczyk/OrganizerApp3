using OrganizerApp.DalEntities.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrganizerApp.DalEntities.Entities
{
    public class Project
    {
        public Project()
        {
            ProjectTasks = new HashSet<Task>();
        }


        [Required(ErrorMessageResourceType = typeof(ValidationCommunications.Project.LocalizedText) , ErrorMessageResourceName = nameof(ValidationCommunications.Project.LocalizedText.NameRequired))]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationCommunications.Project.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Project.LocalizedText.IdRequired))]
        [Range(0 , int.MaxValue , ErrorMessageResourceType = typeof(ValidationCommunications.Project.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Project.LocalizedText.IdRange))]
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationCommunications.Project.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Project.LocalizedText.PriorityRequired))]
        [RegularExpression("^low$|^medium$|^high$", ErrorMessageResourceType = typeof(ValidationCommunications.Project.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Project.LocalizedText.PriorityAcceptedValues))]
        public string Priority { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationCommunications.Project.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Project.LocalizedText.ExecutionTimeRequired))]
        [RegularExpression("^next$|^scheduled$|^someday$", ErrorMessageResourceType = typeof(ValidationCommunications.Project.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Project.LocalizedText.ExecutionTimeAcceptedValues))]
        public string ExecutionTime { get; set; }

        [RequiredIf("ExecutionTime", "scheduled", typeof(ValidationCommunications.Project.LocalizedText), nameof(ValidationCommunications.Project.LocalizedText.StartTimeRequired))]
        public DateTime? StartTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationCommunications.Project.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Project.LocalizedText.StateRequired))]
        [RegularExpression("^todo$|^done$|^deleted$", ErrorMessageResourceType = typeof(ValidationCommunications.Project.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Project.LocalizedText.StateAcceptedValues))]
        public string State { get; set; }

        public ICollection<Task> ProjectTasks { get; set; }
    }
}

