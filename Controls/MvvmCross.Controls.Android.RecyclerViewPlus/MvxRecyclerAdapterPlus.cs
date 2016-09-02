using System.Threading.Tasks;
using Android.Support.V7.Widget;
using MvvmCross.Controls.Core.IncrementalLoadingList;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platform;

namespace MvvmCross.Controls.Android.RecyclerViewPlus
{
    public class MvxRecyclerAdapterPlus : MvxRecyclerAdapter
    {
        private const string LogTag = nameof(MvxRecyclerAdapterPlus);

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Mvx.TaggedTrace(LogTag, "OnBindViewHolder Start position={0}, ItemCount={1}", position, ItemCount);
            base.OnBindViewHolder(holder, position);

            if (position >= ItemCount - 1)
            {
                Mvx.TaggedTrace(LogTag, "OnBindViewHolder Load position={0}, ItemCount={1}", position, ItemCount);
                LoadMoreItems();
            }
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
