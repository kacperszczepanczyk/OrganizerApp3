Jak wystartować solucję:
- otworzyć solucję w Visual Studio
- ustawić projekty startowe (OrganizerApp.WebUI, OrganizerApp.WebApi)
- Kliknąć Start

Dodatkowe info: 
   - baza danych wygeneruje się automatycznie (przetestowane na Visual Studio 2015 Community oraz Visual Studio 2017 Community)
   - baza wygenerowana automatycznie nie zawiera ograniczeń CHECK, które powinny występować dla kolumn priority, executionTime, state
   
Gloabalna obsługa wyjątków:
   - dla OrganizerApp.WebUI: Global.asax/Application_Error() <- ta metoda realizuje globalną obsługę wyjątków
   - dla OrganizerApp.WebApi App_Start/WebApiConfig.cs/ line 31: "config.Services.Replace(typeof(IExceptionHandler), new CustomExceptionHandler());" <- ta linia realizuje globalną obsługę wyjątków
