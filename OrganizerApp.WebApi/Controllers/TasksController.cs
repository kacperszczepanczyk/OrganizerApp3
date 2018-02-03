using OrganizerApp.WebApi.Infrastructure.Validation;
using System;
using System.Web.Http;
using System.Web.Http.Cors;
using OrganizerApp.BLL.Interfaces;
using OrganizerApp.BllDtos.Tasks;
using FluentValidation;
using OrganizerApp.BLL.Helpers.FilterData;
using OrganizerApp.DataCirculationHelpers;

namespace OrganizerApp.WebApi.Controllers
{
    [EnableCors(origins: "http://localhost:63758", headers: "*", methods: "*")]
    public class TasksController : ApiController
    {
        private readonly ITasksBusinessDataManager _tasksDataManager;


        public TasksController(ITasksBusinessDataManager tasksDataManager)
        {
            _tasksDataManager = tasksDataManager;
        }


        [HttpGet]
        public IHttpActionResult GetFiltered(string timeType = null, int? projectID = null, string searchPhrase = null , DateTime? date = null, TaskType tasksType = TaskType.All, TaskResponseDataSetType responseDataSetType = TaskResponseDataSetType.Detail)
        {
            switch (responseDataSetType)
            {
                case TaskResponseDataSetType.Detail:
                    {
                        var tasks = _tasksDataManager.GetFiltered<TaskDetail>(new TaskGetFilteredArgs(timeType, tasksType, projectID, searchPhrase, date));
                        return Ok(tasks);
                    }
                case TaskResponseDataSetType.Header:
                    {
                        var tasks = _tasksDataManager.GetFiltered<TaskBase>(new TaskGetFilteredArgs(timeType, tasksType, projectID, searchPhrase, date));
                        return Ok(tasks);
                    }
                case TaskResponseDataSetType.HeaderWithPriority:
                    {
                        var tasks = _tasksDataManager.GetFiltered<TaskBaseWithPriority>(new TaskGetFilteredArgs(timeType, tasksType, projectID, searchPhrase, date));
                        return Ok(tasks);
                    }
            }

            throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id, TaskResponseDataSetType responseDataSetType = TaskResponseDataSetType.Detail)
        {
            switch (responseDataSetType)
            {
                case TaskResponseDataSetType.Detail:
                    {
                        var task = _tasksDataManager.GetById<TaskDetail>(id);
                        return Ok(task);
                    }
                case TaskResponseDataSetType.Header:
                    {
                        var task = _tasksDataManager.GetById<TaskBase>(id);
                        return Ok(task);
                    }
                case TaskResponseDataSetType.HeaderWithPriority:
                    {
                        var task = _tasksDataManager.GetById<TaskBaseWithPriority>(id);
                        return Ok(task);
                    }
            }

            throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
        }

        // void zwraca 204 - No Content
        [HttpPatch] //ajax requesty działają dla [HttpPost]
        public void Deactivate(int id)
        {
            _tasksDataManager.Deactivate(id);
        }

        // void zwraca 204 - No Content
        [HttpPatch] //ajax requesty działają dla [HttpPost]
        public void Remove(int id)
        {
            _tasksDataManager.Remove(id);
        }

        // void zwraca 204 - No Content
        [HttpPatch] //ajax requesty działają dla [HttpPost]
        public void Done(int id)
        {
            _tasksDataManager.Done(id);
        }

        // void zwraca 204 - No Content
        [HttpPost]
        public void Save(TaskDetail task)
        {
            TaskDetailValidator validator = new TaskDetailValidator();
            validator.ValidateAndThrow(task);
            _tasksDataManager.Save(task);
        }
    }
}
