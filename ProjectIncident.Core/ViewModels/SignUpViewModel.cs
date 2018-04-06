using System;
using ProjectIncident.Core.Commands;
namespace ProjectIncident.Core.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {


        public SignUpViewModel()
        {
            titleUserFirstName = "FirstName";
            titleUserLastName = "Lastname";
            titlePassword = "Password";
            titleEmail = "Email address";
            titleSignUp = "Sign Up";

        }
		public string titleUserFirstName
		{
			get => (String)GetProperty();
            set => SetProperty(value);
		}

		public string titleUserLastName
		{
            get => (String)GetProperty();
            set => SetProperty(value);
		}

		public string titlePassword
		{
            get => (String)GetProperty();
            set => SetProperty(value);
		}

		public string titleEmail
		{
            get => (String)GetProperty();
            set => SetProperty(value);
		}

		public string titleSignUp
		{
            get => (String)GetProperty();
            set => SetProperty(value);
		}

		public string ContentFirstName
		{
            get => (String)GetProperty();
            set => SetProperty(value);
		}

		public string ContentLastName
		{
            get => (String)GetProperty();
            set => SetProperty(value);
		}

		public string ContentPassword
		{
            get => (String)GetProperty();
            set => SetProperty(value);
		}

		public string ContentEmail
		{
            get => (String)GetProperty();
            set => SetProperty(value);
		}


		public DelegateCommand CreateUserCommand
        {
            get => new DelegateCommand(() =>
            {


            });
        }
    
    }
}
