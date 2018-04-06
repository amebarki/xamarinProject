using System;
using ProjectIncident.Core.Commands;
using Xamarin.Forms;

namespace ProjectIncident.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            titleUsername = "Username";
            titlePassword = "Password";
            titleLogin = "Login";
        }
		public string titleUsername
		{
            get { return (string)GetProperty(); }
            set { SetProperty(value); }
		}

		public string titlePassword
		{
            get { return (string)GetProperty(); }
            set { SetProperty(value); }
		}

		public string titleLogin
		{
            get { return (string)GetProperty(); }
            set { SetProperty(value); }
		}
    

        public DelegateCommand OnLoginCommand
        {
            get => new DelegateCommand(async() =>
            {
                await NavigationService.GetCurrent().NavigateToRootPage(NavigationService.RootPageType.Menu);
               // await NavigationService.GetCurrent().NavigateToWithPush(new Views.MainMenuPage());

            });
        }
        public DelegateCommand OnSignInCommand
        {
            get => new DelegateCommand(() =>
            {
              // await NavigationService.GetCurrent().NavigateToWithPush(new LoginPage());

            });
        }
    }
}
