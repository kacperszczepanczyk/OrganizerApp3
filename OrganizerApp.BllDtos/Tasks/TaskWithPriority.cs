
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.BllDtos.Tasks
{
    public class TaskBaseWithPriority : TaskBase
    {
        public string Priority { get; set; }
    }
}
