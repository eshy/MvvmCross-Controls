using System;
using System.Threading.Tasks;

namespace MvvmCross.Controls.IncrementalLoadingList
{
    //This should be in a portable class library
    public interface ICoreSupportIncrementalLoading
    {
        Task LoadMoreItemsAsync();

        event EventHandler ItemsLoaded;
    }
}