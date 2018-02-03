using OrganizerApp.DataCirculationHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.BLL.Helpers.FilterData
{
    public class TaskGetFilteredArgs
    {
        public readonly DateTime? Date;
        public readonly string TimeType;
        public readonly TaskType TasksType;
        public readonly int? ProjectID;
        public readonly string SearchPhrase;

        public TaskGetFilteredArgs(string timeType = null, TaskType tasksType = TaskType.All, int? projectID = null, string searchPhrase = null, DateTime? date = null)
        {
            TimeType = timeType;
            TasksType = tasksType;
            ProjectID = projectID;
            SearchPhrase = searchPhrase;
            Date = date;
        }
    }
}
