using System;
using OrganizerApp.BllDtos.Projects;

namespace OrganizerApp.BllDtos.Tasks
{
    public class TaskDetail : TaskDetailWithoutProjectRef
    {
        public ProjectDetailWithoutTasksRef Project { get; set; }
    }
}