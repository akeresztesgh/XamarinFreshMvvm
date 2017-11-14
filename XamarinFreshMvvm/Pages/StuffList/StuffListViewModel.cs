using System;
using System.Collections.ObjectModel;
using FreshMvvm;
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

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            Stuff = new ObservableCollection<StuffViewModel>(_stuffService.GetList());
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
    }
}
