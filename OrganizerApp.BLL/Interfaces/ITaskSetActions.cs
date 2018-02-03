using OrganizerApp.BllDtos.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.BLL.Interfaces
{
    public interface ITaskSetActions
    {
        void Save(TaskDetail task);
        void Deactivate(int id);
        void Remove(int id);
        void Done(int id);
    }
}
