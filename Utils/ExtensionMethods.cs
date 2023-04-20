using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MVVM2004PurchasingManaging.Utils;

public static class ExtensionsMethods
{
    public static ObservableCollection<T>? ToObservableCollection<T>(this IEnumerable<T> enumerableList)
    {
        if (enumerableList != null)
        {
            var observableCollection = new ObservableCollection<T>(enumerableList);

            return observableCollection;
        }
        return null;
    }


}
