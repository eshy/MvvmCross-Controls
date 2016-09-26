using System;
using System.Threading.Tasks;

namespace MvvmCross.Controls.IncrementalLoadingList
{
    public delegate void ItemsLoadedEventHandler(object sender, ItemsLoadedEventArgs e);
    public interface ICoreSupportIncrementalLoading
    {
        Task LoadMoreItemsAsync();

        event ItemsLoadedEventHandler ItemsLoaded;
    }

    public class ItemsLoadedEventArgs : EventArgs
    {
        /// <summary>
        /// The number of records fetched in the current incremental load
        /// </summary>
        public int CurrentLoadCount { get; set; }

        /// <summary>
        /// The total number of records loaded (number of items in the list)
        /// </summary>
        public int TotalRecordCount { get; set; }

    }
}