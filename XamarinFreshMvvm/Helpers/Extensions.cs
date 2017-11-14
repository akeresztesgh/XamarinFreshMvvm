using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamarinFreshMvvm.Helpers
{
    public static class Extensions
    {
        public static void AddRange<T>(this ObservableCollection<T> col, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                col.Add(item);
            }
        }
    }
}
