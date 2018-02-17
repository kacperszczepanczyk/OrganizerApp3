using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizerApp.WebUI.Helpers.Api.OrganizerApp
{
    public static class ApiUriInfo
    {
        public const string Scheme = "http";
        public const int Port = 53311;
        public const string Host = "localhost";
        public static Uri Domain
        {
            get
            {
                return new UriBuilder(Scheme, Host, Port).Uri;
            }
        }


        public static class Path
        {
            public const string DeactivateTask = "api/tasks/deactivate";
            public const string DoneTask = "api/tasks/done";
            public const string DoneProject = "api/projects/done";
            public const string RemoveTask = "api/tasks/remove";
            public const string RemoveProject = "api/projects/remove";
            public const string GetTaskById = "api/tasks/getById";
            public const string GetTasks = "api/tasks/getFiltered";
            public const string SaveTask = "api/tasks/save";
            public const string GetProjects = "api/projects/getFiltered";
            public const string GetProjectById = "api/projects/getById";
            public const string SaveProject = "api/projects/save";
        }
    }
}