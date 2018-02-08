using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.BLL.Interfaces
{
    public interface ITaskGetActions <V , X>
    {
        T GetById<T>(int id) where T : V;
        IEnumerable<T> GetFiltered<T>(X arguments) where T : V;
    }
}
