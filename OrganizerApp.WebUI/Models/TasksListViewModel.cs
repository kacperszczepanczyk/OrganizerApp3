using OrganizerApp.BllDtos.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizerApp.WebUI.Models
{
    public class TasksListViewModel
    {
        public IEnumerable<TaskBaseWithPriority> Tasks { get; set; }
        public string DoneTaskActionUri { get; set; }
    }
}