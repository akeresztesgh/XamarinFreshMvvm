using System;
using FreshMvvm;
using XamarinFreshMvvm.Services;

namespace XamarinFreshMvvm.Helpers
{
    public class FreshMvvmRegister
    {
        public static void Register()
        {
            FreshIOC.Container.Register<IStuffService, StuffService>();
        }
    }
}
