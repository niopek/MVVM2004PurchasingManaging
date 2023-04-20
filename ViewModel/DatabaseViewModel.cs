using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM2004PurchasingManaging.ViewModel
{
    class DatabaseViewModel : BaseViewModel
    {
        public DatabaseViewModel()
        {
            testText = "test23213123";
        }
        public string testText { get; set; } 
    }
}
