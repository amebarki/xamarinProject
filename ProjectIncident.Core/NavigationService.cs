using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectIncident.Core
{
    public class NavigationService
    {

        public enum RootPageType
        {
            Simple = 0,
            Menu = 1
        }

        private NavigationPage simpleRootPage;
        private MasterDetailPage menuRootPage;

        private RootPageType currentRootPageType;
        public Page RootPage
        {
            get
            {
                switch(currentRootPageType){
                    case RootPageType.Simple :
                        return simpleRootPage;
                    case RootPageType.Menu :
                        return menuRootPage;
                }
                return null;
            }    
        }

        public NavigationPage NavPage
        {
            get{
                switch(currentRootPageType)
                {
                    case RootPageType.Simple:
                        return simpleRootPage;
                    case RootPageType.Menu:
                        return (NavigationPage) menuRootPage.Detail;
                }
                return null;
            }
            set
            {
                switch(currentRootPageType)
                {
                    case RootPageType.Simple:
                        simpleRootPage = value;
                        break;
                    case RootPageType.Menu:
                        menuRootPage.Detail = value;
                        break;
                }
            }
        }

        public bool isMenuPresented
        {
            get{
                return currentRootPageType == RootPageType.Menu ? menuRootPage.IsPresented : false;
            }
            set {
                if (currentRootPageType == RootPageType.Menu)
                    menuRootPage.IsPresented = value;
            }
        }

        #region Singleton

        private static NavigationService _currentService = null;
        public static NavigationService GetCurrent()
        {
            if (_currentService == null)
            {
                _currentService = new NavigationService();
            }

            return _currentService;
        }

        #endregion


        private NavigationService()
        {
            simpleRootPage = new NavigationPage(new Views.HomePage()) { BindingContext = new ViewModels.HomeViewModel() };
            menuRootPage = new MasterDetailPage();
            menuRootPage.Master = new Views.MainMenuPage() { BindingContext = new ViewModels.MainMenuViewModel() };
            menuRootPage.Detail = new NavigationPage(new Views.MapPage()) { BindingContext = new ViewModels.MapViewModel() };
            currentRootPageType = RootPageType.Simple;
        }


        public async Task NavigateToRootPage(RootPageType type)
        {
            if (currentRootPageType == type)
            {
                await NavPage.Navigation.PopToRootAsync();
            }else{
                currentRootPageType = type;
                Application.Current.MainPage = RootPage;
            }
        }



        public async Task NavigateToWithPush(Page page, object viewModel = null)
        {
            if (viewModel != null)
                page.BindingContext = viewModel;
            await NavPage.Navigation.PushAsync(page);
        }

        public void NavigateToWithoutPush(NavigationPage page, object viewModel = null)
        {
            page.BindingContext = viewModel;
            NavPage = page;
        }


        public async Task NavigateReturnPop(){
            await RootPage.Navigation.PopAsync();
        }

    }
}
