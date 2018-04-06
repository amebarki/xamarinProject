using ProjectIncident.Core.Commands;
using ProjectIncident.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectIncident.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public string HomeLogo
        {
            get { return (string)GetProperty(); }
            set { SetProperty(value); }
        }

        public string TitleButton
        {
            get { return (string)GetProperty(); }
            set { SetProperty(value); }
        }

        public DelegateCommand CloseAppCommand
        {
            get => new DelegateCommand(() =>
            {
                var manager = DependencyService.Get<IApplicationManager>();
                manager?.CloseApplication();
            });
        }


        public DelegateCommand EnterCommand
        {
            get => new DelegateCommand(async() =>
            {
                Console.WriteLine("Coucou");
                await NavigationService.GetCurrent().NavigateToWithPush(new Views.LoginPage(), new LoginViewModel());
            });
        }

        public HomeViewModel()
        {
            HomeLogo = "Bonjour à tous";
            TitleButton = "Enter";
        }

    }
}
