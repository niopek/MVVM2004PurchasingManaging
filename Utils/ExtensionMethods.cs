using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace MVVM2004PurchasingManaging.Utils;

public static class ExtensionsMethods
{
    public static ObservableCollection<T>? ToObservableCollection<T>(this IEnumerable<T> enumerableList)
    {
        try
        {
            if (enumerableList != null)
            {
                var observableCollection = new ObservableCollection<T>(enumerableList);

                return observableCollection;
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return new();
    }


}
