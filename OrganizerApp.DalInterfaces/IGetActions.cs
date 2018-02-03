using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.DalInterfaces
{
    public interface IGetActions <T , V>
    {
        T GetById(int id);
        V GetAll();
    }
}
