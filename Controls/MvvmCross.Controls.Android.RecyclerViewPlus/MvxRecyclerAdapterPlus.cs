using System.Collections;
using System.Threading.Tasks;
using MvvmCross.Controls.Core.IncrementalLoadingList;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace MvvmCross.Controls.Android.RecyclerViewPlus
{
    public class MvxRecyclerAdapterPlus : MvxRecyclerAdapter
    {
        protected override void SetItemsSource(IEnumerable value)
        {
            base.SetItemsSource(value);
            if (value != null) { LoadMoreItems(); }
        }

        private async void LoadMoreItems()
        {
            await LoadMoreItemsAsync();
        }

        public async Task LoadMoreItemsAsync()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var source = ItemsSource as ICoreSupportIncrementalLoading;
            if (source == null) { return; }
            await source.LoadMoreItemsAsync();
        }
    }
}
