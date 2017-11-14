using FreshMvvm;
using Xamarin.Forms;
using XamarinFreshMvvm.Helpers;
using XamarinFreshMvvm.Pages.StuffList;

namespace XamarinFreshMvvm
{
    public partial class App : Application
    {
        private FreshMasterDetailNavigationContainer _masterDetailNav;

        public App()
        {
            InitializeComponent();
            FreshMvvmRegister.Register();
            SetMainPage();

            //MainPage = new XamarinFreshMvvmPage();
        }

        public async void SetMainPage()
        {
            //var loginPage = FreshPageModelResolver.ResolvePageModel<LoginViewModel>();
            //_loginContainer = new FreshNavigationContainer(loginPage, NavigationContainerNames.Auth);

            _masterDetailNav = new FreshMasterDetailNavigationContainer();
            _masterDetailNav.Init("Menu", "hamburger.png");
            _masterDetailNav.AddPage<StuffListViewModel>("Stuff", null);

            Current.MainPage = _masterDetailNav;

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
