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


        [Required(ErrorMessage = ValidationCommunications.Project.NameRequired)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = ValidationCommunications.Project.IdRequired)]
        [Range(0, int.MaxValue, ErrorMessage = ValidationCommunications.Project.IdRange)]
        public int ID { get; set; }

        [Required(ErrorMessage = ValidationCommunications.Project.PriorityRequired)]
        [RegularExpression("^low$|^medium$|^high$", ErrorMessage = ValidationCommunications.Project.PriorityAcceptedValues)]
        public string Priority { get; set; }

        [Required(ErrorMessage = ValidationCommunications.Project.ExecutionTimeRequired)]
        [RegularExpression("^next$|^scheduled$|^someday$", ErrorMessage = ValidationCommunications.Project.ExecutionTimeAcceptedValues)]
        public string ExecutionTime { get; set; }

        [RequiredIf("ExecutionTime", "scheduled", ValidationCommunications.Project.StartTimeRequired)]
        public DateTime? StartTime { get; set; }

        [Required(ErrorMessage = ValidationCommunications.Project.StateRequired)]
        [RegularExpression("^todo$|^done$|^deleted$", ErrorMessage = ValidationCommunications.Project.StateAcceptedValues)]
        public string State { get; set; }

        public ICollection<Task> ProjectTasks { get; set; }
    }
}

