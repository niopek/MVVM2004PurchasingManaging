using CommunityToolkit.Mvvm.ComponentModel;
using MVVM2004PurchasingManaging.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MVVM2004PurchasingManaging.Views
{
    /// <summary>
    /// Logika interakcji dla klasy BulkAddingSupplier.xaml
    /// </summary>
    public partial class BulkAddingSupplier : Window, INextWindowView
    {
        public BulkAddingSupplier()
        {
            InitializeComponent();
        }

        public void Open(ObservableObject obj)
        {
            this.DataContext = obj;
            this.Show();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            e.Cancel = true;
        }
    }
}
