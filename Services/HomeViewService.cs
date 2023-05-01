using Microsoft.EntityFrameworkCore;
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using MVVM2004PurchasingManaging.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM2004PurchasingManaging.Services
{
    internal class HomeViewService : IHomeViewService
    {
        private readonly MyDbContext context;

        public HomeViewService(MyDbContext context)
        {
            this.context = context;
        }
        public ObservableCollection<Post>? GetAll() => context.Posts.OrderByDescending(p => p.Date).ToObservableCollection();
        public async Task<ObservableCollection<Post>?> AddPost(Post post)
        {
            await Task.Run(() =>
            {
                context.Posts.Add(post);
                context.SaveChanges();
            });
            return GetAll();
        }
        public async Task<ObservableCollection<Post>?> RemovePost(Post post)
        {
            await Task.Run(() =>
            {
                context.Posts.Remove(post);
                context.SaveChanges();
            });
            return GetAll();
        }

        
    }
}
