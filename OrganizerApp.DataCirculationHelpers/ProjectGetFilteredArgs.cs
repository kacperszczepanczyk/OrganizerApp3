using OrganizerApp.DataCirculationHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.DataCirculationHelpers
{
    public class ProjectGetFilteredArgs
    {
        public readonly string SearchPhrase;
        public readonly ProjectType ProjectsType;

        public ProjectGetFilteredArgs(string searchPhrase = null, ProjectType projectsType = ProjectType.All)
        {
            SearchPhrase = searchPhrase;
            ProjectsType = projectsType;
        }
    }
}
