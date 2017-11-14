using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace XamarinFreshMvvm.Pages.StuffList
{
    public partial class StuffListPage : ContentPage
    {
        private PopupPage _loadingScreen = new LoadingScreen();

        public StuffListPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<StuffListViewModel>(this, Helpers.Constants.Messages.ShowLoadingScreen, ShowLoadingScreen);
            MessagingCenter.Subscribe<StuffListViewModel>(this, Helpers.Constants.Messages.HideLoadingScreen, HideLoadingScreen);
        }

        private async void ShowLoadingScreen(object sender)
        {
            if (PopupNavigation.PopupStack != null && PopupNavigation.PopupStack.Count == 0)
                await Navigation.PushPopupAsync(_loadingScreen, false);
        }

        private async void HideLoadingScreen(object sender)
        {
            await Navigation.PopPopupAsync();
        }

    }
}
