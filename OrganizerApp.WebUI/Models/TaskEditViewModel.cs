using OrganizerApp.BllDtos.Projects;
using OrganizerApp.BllDtos.Tasks;
using OrganizerApp.WebUI.Helpers;
using OrganizerApp.WebUI.Helpers.View.ContentGenerator.Implementations;
using OrganizerApp.WebUI.Helpers.View.ContentGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizerApp.WebUI.Models
{
    public class TaskEditViewModel
    {
        public TaskDetail Task { get; set; }
        public IEnumerable<ProjectBase> ActiveProjects { get; set; }
        public ITaskContentGenerator ContentGenerator { get; set; }


        public TaskEditViewModel()
        {
            ContentGenerator = new TaskEditContentGenerator();
        }
    }
}