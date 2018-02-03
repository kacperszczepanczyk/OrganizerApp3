using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Data.Entity.Core;
using System.Collections;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using OrganizerApp.Dalnterfaces;
using OrganizerApp.DalEntities.Entities;
using OrganizerApp.BllDtos.Tasks;
using OrganizerApp.BLL.Interfaces;
using OrganizerApp.DataCirculationHelpers;
using OrganizerApp.BLL.Helpers;
using System.Data.Entity;
using OrganizerApp.BLL.Helpers.FilterData;

namespace OrganizerApp.BLL.BusinessDataManagers
{
    public class TasksBusinessDataManager : ITasksBusinessDataManager
    {
        private readonly IRepository<IQueryable<Task> , IQueryable<Task>, Task> _tasksRepository;


        public TasksBusinessDataManager(IRepository<IQueryable<Task>, IQueryable<Task> , Task> tasksRepository)
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

            try
            {
                _tasksRepository.Save(task , x => nameof(x.ExecutionTime) , x => nameof(x.StartTime));
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (UpdateException)
            {
                throw;
            }
            catch (DbEntityValidationException)
            {
                throw;
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (ObjectDisposedException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        public void Done(int id)
        {
            Task task = new Task()
            {
                ID = id,
                State = "done"
            };

            try
            {
                _tasksRepository.Save(task, x => nameof(x.State));
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (UpdateException)
            {
                throw;
            }
            catch (DbEntityValidationException)
            {
                throw;
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (ObjectDisposedException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }


        public T GetById<T>(int id) where T : TaskBase
        {
            IQueryable<Task> task;

            try
            {
                task = _tasksRepository.GetById(id);
            }
            catch (SqlException)
            {
                throw;
            }

            IQueryable<T> taskDto;

            try
            {
                taskDto = task.ProjectTo<T>();
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }

            return taskDto.FirstOrDefault();
        }

        public IEnumerable<T> GetFiltered<T>(TaskGetFilteredArgs arguments) where T : TaskBase
        {
            if (arguments == null)
            {
                throw new ArgumentNullException("Obiekt arguments musi mieć wartość różną od null");
            }

            IQueryable<Task> tasks;

            try
            {
                tasks = _tasksRepository.GetAll();
            }
            catch (SqlException)
            {
                throw;
            }

            if (arguments.ProjectID != null)
            {
                tasks = tasks.Where(x => x.ProjectID == arguments.ProjectID);
            }

            switch(arguments.TasksType)
            {
                case TaskType.Done:
                    {
                        tasks = tasks.Where(x => x.State == "done");
                        break;
                    }
                case TaskType.Deleted:
                    {
                        tasks = tasks.Where(x => x.State == "deleted");
                        break;
                    }
                case TaskType.Active:
                    {
                        tasks = tasks.Where(x => x.State == "todo")
                                     .Where(x => x.ExecutionTime != "someday" || x.ProjectID != null);
                        break;
                    }
                case TaskType.Disactive:
                    {
                        tasks = tasks.Where(x => x.ProjectID == null)
                                     .Where(x => x.ExecutionTime == "someday");
                        break;
                    }
                default:
                    {
                        if (arguments.TasksType != TaskType.All)
                        {
                            throw new ArgumentException("Argument tasksType przyjął wartość po której nie mogę przefiltrować danych");
                        }
                        break; 
                    }
            }

            if (arguments.TimeType != null)
            {
                tasks = tasks.Where(x => x.ExecutionTime == arguments.TimeType);
            }

            if (arguments.SearchPhrase != null)
            {
                tasks = tasks.Where(x =>
                    x.Name.Contains(arguments.SearchPhrase) || 
                    x.Description.Contains(arguments.SearchPhrase)
                );
            }

            if (arguments.Date != null)
            {
                tasks = tasks.Where(x => x.StartTime == arguments.Date);
                
            }

            IQueryable<T> taskDtos;

            try
            {
                taskDtos = tasks.ProjectTo<T>();
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            
            return taskDtos.ToList();
        }

        public void Remove(int id)
        {
            Task task = new Task()
            {
                ID = id,
                State = "deleted"
            };

            try
            {
                _tasksRepository.Save(task, x => nameof(x.State));
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (UpdateException)
            {
                throw;
            }
            catch (DbEntityValidationException)
            {
                throw;
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (ObjectDisposedException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        public void Save(TaskDetail task)
        {
            var taskEntity = AutoMapper.Mapper.Map<Task>(task);

            try
            {
                _tasksRepository.Save(taskEntity);
            }
            catch(ArgumentNullException)
            {
                throw;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (UpdateException)
            {
                throw;
            }
            catch (DbEntityValidationException)
            {
                throw;
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (ObjectDisposedException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }
    }
}
