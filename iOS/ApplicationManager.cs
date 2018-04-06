using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

[assembly: Xamarin.Forms.Dependency(typeof(ProjectIncident.iOS.ApplicationManager))]
namespace ProjectIncident.iOS
{
    public class ApplicationManager : ProjectIncident.Core.Interfaces.IApplicationManager
    {
        // Méthode permettant de fermer l’application
        public void CloseApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}
