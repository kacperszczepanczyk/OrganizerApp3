using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizerApp.WebUI.Infrastructure
{
    public class ExternalDataCirculationException : Exception
    {
        public ExternalDataCirculationException() { }
        public ExternalDataCirculationException(string message) : base(message) { }
        public ExternalDataCirculationException(string message , Exception innerException) : base(message , innerException) { }
    }
}