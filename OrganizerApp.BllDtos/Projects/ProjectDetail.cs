
using System;
using System.Collections.Generic;

namespace OrganizerApp.BllDtos.Projects
{
    public class ProjectDetail : ProjectBase
    {
        public string Description { get; set; }
        public string Priority { get; set; }
        public string ExecutionTime { get; set; }
        public DateTime? StartTime { get; set; }
        public string State { get; set; }
        public ICollection<Tasks.TaskDetailWithoutProjectRef> ProjectTasks { get; set; }
    }
}
