using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FreshMvvm;
using Xamarin.Forms;
using XamarinFreshMvvm.Helpers;
using XamarinFreshMvvm.Pages.StuffDetails;
using XamarinFreshMvvm.Services;
using XamarinFreshMvvm.ViewModels;

namespace XamarinFreshMvvm.Pages.StuffList
{
    public class StuffListViewModel : FreshBasePageModel
    {
        private readonly IStuffService _stuffService;

        public StuffListViewModel(IStuffService stuffService)
        {
            _stuffService = stuffService;
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            try
            {
                MessagingCenter.Send(this, Helpers.Constants.Messages.ShowLoadingScreen);
                Stuff = new ObservableCollection<StuffViewModel>(await _stuffService.GetList());    
            }
            finally
            {
                MessagingCenter.Send(this, Helpers.Constants.Messages.HideLoadingScreen);
            }
        }

        public ObservableCollection<StuffViewModel> Stuff { get; set; } = new ObservableCollection<StuffViewModel>();

        private StuffViewModel _selectedStuff;
        public StuffViewModel SelectedStuff 
        { 
            get { return _selectedStuff; }
            set
            {
                if(_selectedStuff != value)
                {
                    _selectedStuff = value;
                    CoreMethods.PushPageModel<StuffDetailsViewModel>(value);
                }
            }
        }

        private ICommand _addStuffCommand;
        public ICommand AddStuffCommand
        {
            get
            {
                return _addStuffCommand ?? (_addStuffCommand = new DelegateCommand(AddStuff)); 
            }
        }

        private void AddStuff()
        {
            CoreMethods.PushPageModel<StuffDetailsViewModel>();
        }
    }
}
