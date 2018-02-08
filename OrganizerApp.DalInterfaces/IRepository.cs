using OrganizerApp.DalEntities.Entities;
using OrganizerApp.DalInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.Dalnterfaces
{
    public interface IRepository <T , V , Z> : IGetActions<T , V> , ISetDataActions<Z>
    {
    }
}
