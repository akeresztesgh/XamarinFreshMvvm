using System;
namespace XamarinFreshMvvm.ViewModels
{
    public class StuffViewModel
    {
        public StuffViewModel()
        {
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string String1 { get; set; }
        public string String2 { get; set; }
        public DateTime Date1 { get; set; } = DateTime.Today;
        public string DateString { get { return Date1.ToString("g"); } }
    }
}
