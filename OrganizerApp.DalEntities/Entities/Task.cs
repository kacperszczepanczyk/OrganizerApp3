using System.ComponentModel.DataAnnotations;
using OrganizerApp.DalEntities.Helpers;
using System;

namespace OrganizerApp.DalEntities.Entities
{
    public class Task
    {
        [Required(ErrorMessageResourceType = typeof(ValidationCommunications.Task.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Task.LocalizedText.NameRequired))]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationCommunications.Task.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Task.LocalizedText.IdRequired))]
        [Range(0 , int.MaxValue, ErrorMessageResourceType = typeof(ValidationCommunications.Task.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Task.LocalizedText.IdRange))]
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationCommunications.Task.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Task.LocalizedText.PriorityRequired))]
        [RegularExpression("^low$|^medium$|^high$" , ErrorMessageResourceType = typeof(ValidationCommunications.Task.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Task.LocalizedText.PriorityAcceptedValues))]
        public string Priority { get; set; }

        public string Context { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationCommunications.Task.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Task.LocalizedText.ExecutionTimeRequired))]
        [RegularExpression("^next$|^scheduled$|^someday$", ErrorMessageResourceType = typeof(ValidationCommunications.Task.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Task.LocalizedText.ExecutionTimeAcceptedValues))]
        public string ExecutionTime { get; set; }

        [RequiredIf("ExecutionTime", "scheduled", typeof(ValidationCommunications.Task.LocalizedText), nameof(ValidationCommunications.Task.LocalizedText.StartTimeRequired))]
        public DateTime? StartTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationCommunications.Task.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Task.LocalizedText.StateRequired))]
        [RegularExpression("^todo$|^done$|^deleted$" , ErrorMessageResourceType = typeof(ValidationCommunications.Task.LocalizedText), ErrorMessageResourceName = nameof(ValidationCommunications.Task.LocalizedText.StateAcceptedValues))]
        public string State { get; set; }

        public int? ProjectID { get; set; }

        public virtual Project Project { get; set; }
    }
}
