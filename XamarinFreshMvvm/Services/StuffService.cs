﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinFreshMvvm.ViewModels;

namespace XamarinFreshMvvm.Services
{
    public interface IStuffService 
    {
        Task<List<StuffViewModel>> GetList();
        void Save(StuffViewModel vm);
        void Update(StuffViewModel vm);
    }

    public class StuffService : IStuffService
    {
        private readonly List<StuffViewModel> _list = new List<StuffViewModel>();

        public StuffService()
        {
            for (var i = 0; i < 10; i++)
            {
                _list.Add(new StuffViewModel()
                {
                    Id = i,
                    Title = $"Title {i}",
                    String1 = $"String1-{i}",
                    String2 = $"String2-{i}",
                    Date1 = DateTime.Today - TimeSpan.FromDays(i)
                });
            }
        }

        public async Task<List<StuffViewModel>> GetList()
        {
            await Task.Delay(3000);
            return _list;
        }

        public void Save(StuffViewModel vm)
        {
            _list.Add(vm);   
        }

        public void Update(StuffViewModel vm)
        {
            // Normally something happens here    
        }

    }
}
