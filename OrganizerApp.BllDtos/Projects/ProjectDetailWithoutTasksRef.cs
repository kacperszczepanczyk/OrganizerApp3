using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.BllDtos.Projects
{
    public class ProjectDetailWithoutTasksRef : ProjectBase
    {
        public string Description { get; set; }
        public string Priority { get; set; }
        public string ExecutionTime { get; set; }
        public DateTime? StartTime { get; set; }
        public string State { get; set; }
    }
}
