using System.ComponentModel.DataAnnotations;
using OrganizerApp.DalEntities.Helpers;
using System;

namespace OrganizerApp.DalEntities.Entities
{
    public class Task
    {
        [Required(ErrorMessage = ValidationCommunications.Task.NameRequired)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = ValidationCommunications.Task.IdRequired)]
        [Range(0 , int.MaxValue, ErrorMessage = ValidationCommunications.Task.IdRange)]
        public int ID { get; set; }

        [Required(ErrorMessage = ValidationCommunications.Task.PriorityRequired)]
        [RegularExpression("^low$|^medium$|^high$" , ErrorMessage = ValidationCommunications.Task.PriorityAcceptedValues)]
        public string Priority { get; set; }

        public string Context { get; set; }

        [Required(ErrorMessage = ValidationCommunications.Task.ExecutionTimeRequired)]
        [RegularExpression("^next$|^scheduled$|^someday$", ErrorMessage = ValidationCommunications.Task.ExecutionTimeAcceptedValues)]
        public string ExecutionTime { get; set; }

        [RequiredIf("ExecutionTime", "scheduled", ValidationCommunications.Task.StartTimeRequired)]
        public DateTime? StartTime { get; set; }

        [Required(ErrorMessage = ValidationCommunications.Task.StateRequired)]
        [RegularExpression("^todo$|^done$|^deleted$" , ErrorMessage = ValidationCommunications.Task.StateAcceptedValues)]
        public string State { get; set; }

        public int? ProjectID { get; set; }

        public virtual Project Project { get; set; }
    }
}
