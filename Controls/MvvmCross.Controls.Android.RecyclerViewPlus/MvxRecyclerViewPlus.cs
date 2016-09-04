using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Runtime;
using Android.Util;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace MvvmCross.Controls.Android.RecyclerViewPlus
{
    public class MvxRecyclerViewPlus : MvxRecyclerView
    {

        public MvxRecyclerViewPlus(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public MvxRecyclerViewPlus(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        private void Initialize()
        {
            IMvxRecyclerAdapter adapter = new MvxRecyclerAdapterPlus();
            Adapter = adapter;
            var layoutManager = GetLayoutManager();
            var scrollListener = new MvxRecyclerEndlessScrollListener(layoutManager, LoadMoreDataAsync);
            AddOnScrollListener(scrollListener);
        }

        private async Task<int> LoadMoreDataAsync(int page, int totalRecordCount)
        {
            var adapter = (Adapter as MvxRecyclerAdapterPlus);
            if (adapter != null)
            {
                await adapter.LoadMoreItemsAsync();
            }
            return 0;
        }

        public MvxRecyclerViewPlus(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
        }

        public MvxRecyclerViewPlus(Context context, IAttributeSet attrs, int defStyle, IMvxRecyclerAdapter adapter) : base(context, attrs, defStyle, adapter)
        {
        }

    }
}