using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(ProjectIncident.Droid.ApplicationManager))]
namespace ProjectIncident.Droid
{
    public class ApplicationManager : ProjectIncident.Core.Interfaces.IApplicationManager
    {
        public void CloseApplication()
        {
            var activity = (Activity)MainActivity.Activity;
            activity?.FinishAffinity();
        }
    }

}