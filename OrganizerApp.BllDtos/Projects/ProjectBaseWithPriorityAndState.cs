
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.BllDtos.Projects
{
    public class ProjectBaseWithPriorityAndState : ProjectBase
    {
        public string Priority { get; set; }
        public string State { get; set; }
    }
}
