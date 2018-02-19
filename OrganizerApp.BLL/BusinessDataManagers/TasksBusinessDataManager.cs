using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OrganizerApp.DalEntities.Entities;
using OrganizerApp.BllDtos.Tasks;
using OrganizerApp.BLL.Interfaces;
using OrganizerApp.DataCirculationHelpers;
using OrganizerApp.DalInterfaces.Task;

namespace OrganizerApp.BLL.BusinessDataManagers
{
    public class TasksBusinessDataManager : ITasksBusinessDataManager
    {
        private readonly ITasksRepository _tasksRepository;


        public TasksBusinessDataManager(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }


        public void Deactivate(int id)
        {
            Task task = new Task()
            {
                ID = id,
                ExecutionTime = "someday",
                StartTime = null
            };

            _tasksRepository.Save(task , x => nameof(x.ExecutionTime) , x => nameof(x.StartTime));
           
        }

        public void Done(int id)
        {
            Task task = new Task()
            {
                ID = id,
                State = "done"
            };

            _tasksRepository.Save(task, x => nameof(x.State));
        }


        public T GetById<T>(int id) where T : TaskBase
        {
            var task = _tasksRepository.GetById(id);
            var taskDto = Mapper.Map<T>(task);

            return taskDto;
        }

        public IEnumerable<T> GetFiltered<T>(TaskGetFilteredArgs arguments) where T : TaskBase
        {
            if (arguments == null)
            {
                throw new ArgumentNullException("Obiekt arguments musi mieć wartość różną od null");
            }

            var tasks = _tasksRepository.GetFiltered(arguments);
            var taskDtos = Mapper.Map<IEnumerable<T>>(tasks);
            
            return taskDtos;
        }

        public void Remove(int id)
        {
            Task task = new Task()
            {
                ID = id,
                State = "deleted"
            };

            _tasksRepository.Save(task, x => nameof(x.State));
        }

        public void Save(TaskDetail task)
        {
            var taskEntity = Mapper.Map<Task>(task);
            _tasksRepository.Save(taskEntity);
        }
    }
}
