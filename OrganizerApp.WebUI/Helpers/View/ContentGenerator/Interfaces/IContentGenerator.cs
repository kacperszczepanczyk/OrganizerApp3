using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.WebUI.Helpers.View.ContentGenerator.Interfaces
{
    public interface IContentGenerator
    {
        string GetHeaderText(int id, string name);
        string GetPriorityHtmlCheckedState(string priority, string expectedPriorityValue);
        string GetExecutionTimeHtmlSelectedState(string executionTime, string expectedExecutionTime);
        string GetDateHtmlValue(DateTime? date);
    }
}
