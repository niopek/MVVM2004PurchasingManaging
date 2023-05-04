using MVVM2004PurchasingManaging.Entities;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MVVM2004PurchasingManaging.Interfaces;

public interface IHomeViewService
{
    ObservableCollection<Post>? GetAll();
    Task<ObservableCollection<Post>?> AddPost(Post post);
    Task<ObservableCollection<Post>?> RemovePost(Post post);
    
}
