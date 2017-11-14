using FreshMvvm;
using Xamarin.Forms;
using XamarinFreshMvvm.Helpers;

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

            MainPage = new XamarinFreshMvvmPage();
        }

        public async void SetMainPage()
        {
            //var loginPage = FreshPageModelResolver.ResolvePageModel<LoginViewModel>();
            //_loginContainer = new FreshNavigationContainer(loginPage, NavigationContainerNames.AuthenticationContainer);

        //    _masterDetailNav = new FreshMasterDetailNavigationContainer();
        //    _masterDetailNav.Init("Menu", "hamburger.png");
        //    _masterDetailNav.AddPage<>("Stuff", null);
        //    _masterDetailNav.AddPage<CarListViewModel>("CARS", null);
        //    _masterDetailNav.AddPage<SignOutViewModel>("Sign Out", null);

        //    var authenticator = FreshIOC.Container.Resolve<IAuthenticator>();
        //    //authenticator.SignOut(LoginViewModel.authority);

        //    var isAuthenticated = authenticator.IsAuthenticated();
        //    // if (isAuthenticated)
        //    // {
        //    //     await _masterDetailNav.SwitchSelectedRootPageModel<ActionItemListViewModel>();
        //    //     Current.MainPage = _masterDetailNav;
        //    // }
        //    // else
        //    {
        //        Current.MainPage = _loginContainer;
        //    }
        //}

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
