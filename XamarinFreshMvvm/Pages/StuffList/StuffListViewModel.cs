using System;
using System.Collections.ObjectModel;
using FreshMvvm;
using XamarinFreshMvvm.Helpers;
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
            Stuff.AddRange(_stuffService.GetList());
        }

        public ObservableCollection<StuffViewModel> Stuff { get; set; } = new ObservableCollection<StuffViewModel>();

        public StuffViewModel SelectedStuff { get; set; }
    }
}
