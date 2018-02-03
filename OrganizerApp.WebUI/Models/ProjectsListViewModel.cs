using OrganizerApp.BllDtos.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizerApp.WebUI.Models
{
    public class ProjectsListViewModel
    {
        public IEnumerable<ProjectBaseWithPriorityAndState> Projects { get; set; }
        public string DoneProjectActionUri { get; set; }
    }
}