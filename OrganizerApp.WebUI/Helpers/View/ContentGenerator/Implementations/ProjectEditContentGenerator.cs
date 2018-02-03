using OrganizerApp.WebUI.Helpers.View.ContentGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizerApp.WebUI.Helpers.View.ContentGenerator.Implementations
{
    public class ProjectEditContentGenerator : IProjectContentGenerator
    {
        public string GetDateHtmlValue(DateTime? date)
        {
            return date?.ToShortDateString() ?? "";
        }

        public string GetExecutionTimeHtmlSelectedState(string executionTime, string expectedExecutionTime)
        {
            return String.Equals(expectedExecutionTime, executionTime, StringComparison.Ordinal) ? "selected" : "";
        }

        public string GetHeaderText(int id, string name)
        {
            return id == 0 ? "Tworzysz nowy projekt: " : "Edytujesz projekt: " + name;
        }

        public string GetPriorityHtmlCheckedState(string priority, string expectedPriorityValue)
        {
            return String.Equals(expectedPriorityValue, priority, StringComparison.Ordinal) ? "checked" : "";
        }
    }
}