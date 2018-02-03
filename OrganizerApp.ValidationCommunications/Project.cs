using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.ValidationCommunications
{
    public static class Project
    {
        public const string NameRequired = "Podaj nazwę projektu.";
        public const string IdRange = "Nieprawidłowa wartość ID. ID musi równać się 0 (dla nowego rekordu) lub być większe od 0 dla rekordu aktualizowanego.";
        public const string IdRequired = "Podaj wartość id";
        public const string StateAcceptedValues = "Nieprawidłowa wartość stanu projektu. Stan projektu musi być równy jednej z następujących wartości: todo, done, deleted";
        public const string PriorityRequired = "Podaj priorytet projektu.";
        public const string PriorityAcceptedValues = "Nieprawidłowa wartość typu priorytetu. Priorytet musi być równy jednej z następujących wartości: low, medium, high";
        public const string ExecutionTimeRequired = "Podaj typ czasu wykonania projektu.";
        public const string ExecutionTimeAcceptedValues = "Nieprawidłowa wartość typu czasu. Typ czasu wykonania musi być równy jednej z następujących wartości: next, scheduled, someday";
        public const string StartTimeRequired = "Projekt o typie czasu \"scheduled\" musi mieć podaną konkretną datę rozpoczęcia.";
        public const string StateRequired = "Podaj stan projektu";
    }
}
