using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ProjectIncident.Core.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {

        public class MenuItem
        {
            private NavigationPage _page;
            private Type _pageType;
            private object _viewModel;
            private Type _viewModelType;
            private object[] _viewModelCtorParams;

            public string ImageName { get; }
            public string Title { get; set; }
            public NavigationPage Page
            {
                get
                {
                    if (_page == null && _pageType != null) _page = new NavigationPage((Page) Activator.CreateInstance(_pageType));
                    return _page;
                }
            }
            public object ViewModel
            {
                get
                {
                    if (_viewModel == null && _viewModelType != null) _viewModel = Activator.CreateInstance(_viewModelType, _viewModelCtorParams);
                    return _viewModel;
                }
            }

            public MenuItem(string imageName, string title,
                            NavigationPage page, object viewModel = null)
            {
                ImageName = imageName;
                Title = title;
                _page = page;
                _viewModel = viewModel;
                _pageType = null;
                _viewModelType = null;
                _viewModelCtorParams = null;
            }

            public MenuItem(string imageName, string title,
                            Type pageType, Type viewModelType = null,
                            params object[] viewModelCtorParams)
                : this(imageName, title, (NavigationPage)null, (object)null)
            {
                _pageType = pageType;
                _viewModelType = viewModelType;
                _viewModelCtorParams = viewModelCtorParams;
            }
        }


        public string MenuTitle
        {
            get { return (string)GetProperty(); }
            set { SetProperty(value); }
        }

        public ObservableCollection<MenuItem> Menus
        {
            get { return (ObservableCollection<MenuItem>)GetProperty(); }
            private set { SetProperty(value); }
        }


#pragma warning disable 4014
        public MenuItem SelectedMenu
        {
            get { return (MenuItem)GetProperty(); }
            set
            {
                if (SetProperty(value) && value != null)
                {
                    SelectedMenu = null;
                    NavigationService.GetCurrent().isMenuPresented = false;
                    if (value.Title == "Log Out")
                        NavigationService.GetCurrent().NavigateToRootPage(NavigationService.RootPageType.Simple);
                    else
                        NavigationService.GetCurrent().NavigateToWithoutPush(value.Page, value.ViewModel);
                }
            }
        }
#pragma warning restore 4014

        public MainMenuViewModel()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    MenuTitle = "Menu";
                    break;
                default:
                    MenuTitle = "ProjectIncident";
                    break;
            }

            Menus = new ObservableCollection<MenuItem>();
            Menus.Add(new MenuItem("home.png", "Profil", typeof(Views.HomePage), typeof(HomeViewModel)));
            Menus.Add(new MenuItem("map.png", "Map", typeof(Views.MapPage), typeof(MapViewModel)));
            Menus.Add(new MenuItem("photo.png", "Photos", typeof(Views.PhotoPage), typeof(PhotoViewModel)));
            Menus.Add(new MenuItem("settings.png", "Settings", typeof(Views.SettingsPage), typeof(SettingsViewModel)));
            Menus.Add(new MenuItem("settings.png", "Log Out", typeof(Views.HomePage), typeof(HomeViewModel)));
        }
    }
}
