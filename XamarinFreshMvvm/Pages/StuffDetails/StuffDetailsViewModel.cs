using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using PropertyChanged;
using XamarinFreshMvvm.Helpers;
using XamarinFreshMvvm.Services;
using XamarinFreshMvvm.ViewModels;

namespace XamarinFreshMvvm.Pages.StuffDetails
{
    [AddINotifyPropertyChangedInterface]
    public class StuffDetailsViewModel : FreshBasePageModel
    {
        private readonly IStuffService _stuffService;

        private StuffViewModel _stuff;

        public StuffDetailsViewModel(IStuffService stuffService)
        {
            _stuffService = stuffService;
        }

        public override async void Init(object initData)
        {
            _stuff = initData as StuffViewModel;
            if (_stuff == null)
                _stuff = new StuffViewModel();

            Title = _stuff.Title;
            String1 = _stuff.String1;
            String2 = _stuff.String2;
            Date1 = _stuff.Date1;
        }


        public string Title { get; set; }
        public string String1 { get; set; }
        public string String2 { get; set; }
        public DateTime Date1 { get; set; }

        private ICommand _saveCommand;
        public ICommand SaveCommand 
        {
            get 
            {
                return _saveCommand ?? (_saveCommand = new DelegateCommand(Save));
            }
        }

        private async void Save()
        {
            
            _stuff.Title = Title;
            _stuff.String1 = String1;
            _stuff.String2 = String2;
            _stuff.Date1 = Date1;
            if (_stuff.Id == 0)
                _stuffService.Save(_stuff);
            else
                _stuffService.Update(_stuff);

            await CoreMethods.PopPageModel();
        }

    }
}
