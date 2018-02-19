using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.DalInterfaces.Task
{
    public interface ITasksSetActions <T>
    {
        void Remove(int id);
        void Save(T entity, params Func<T, string>[] modifiedPropertyNames);
    }
}
