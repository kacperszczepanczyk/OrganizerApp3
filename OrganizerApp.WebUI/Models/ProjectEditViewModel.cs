using OrganizerApp.BllDtos.Projects;
using OrganizerApp.WebUI.Helpers;
using OrganizerApp.WebUI.Helpers.View.ContentGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizerApp.WebUI.Models
{
    public class ProjectEditViewModel
    {
        public ProjectDetail Project { get; set; }
        public IProjectContentGenerator ContentGenerator { get; set; }
    }
}