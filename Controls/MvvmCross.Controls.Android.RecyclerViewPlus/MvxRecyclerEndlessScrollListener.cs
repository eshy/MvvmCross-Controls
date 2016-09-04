using System;
using System.Linq;
using System.Threading.Tasks;
using Android.Support.V7.Widget;

namespace MvvmCross.Controls.Android.RecyclerViewPlus
{
    /// <summary>
    /// Based on https://gist.github.com/nesquena/d09dc68ff07e845cc622 and https://guides.codepath.com/android/Endless-Scrolling-with-AdapterViews-and-RecyclerView
    /// </summary>
    public class MvxRecyclerEndlessScrollListener : RecyclerView.OnScrollListener
    {
        private readonly RecyclerView.LayoutManager _layoutManager;
        private readonly Func<int, int, Task<int>> _loadMoreDataFunc;
        private readonly int _visibleThreshold = 5;
        private int _currentPage;
        private int _previousTotalItemCount;
        private bool _loading = true;
        private readonly int _startingPageIndex = 0;

        public MvxRecyclerEndlessScrollListener(RecyclerView.LayoutManager layoutManager)
        {
            _layoutManager = layoutManager;

            if (_layoutManager is StaggeredGridLayoutManager)
            {
                _visibleThreshold = _visibleThreshold * ((StaggeredGridLayoutManager)_layoutManager).SpanCount;
            }
            else if (_layoutManager is GridLayoutManager)
            {
                _visibleThreshold = _visibleThreshold * ((GridLayoutManager)_layoutManager).SpanCount;
            }
        }

        public MvxRecyclerEndlessScrollListener(RecyclerView.LayoutManager layoutManager, Func<int, int, Task<int>> loadMoreDataFunc) : this(layoutManager)
        {
            _loadMoreDataFunc = loadMoreDataFunc;
        }

        public int GetLastVisibleItem(int[] lastVisibleItemPositions)
        {
            var max = lastVisibleItemPositions.Max();
            var maxSize = 0;
            for (var i = 0; i < lastVisibleItemPositions.Length; i++)
            {
                if (i == 0)
                {
                    maxSize = lastVisibleItemPositions[i];
                } else if (lastVisibleItemPositions[i] > maxSize)
                {
                    maxSize = lastVisibleItemPositions[i];
                }
            }

            return maxSize;
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            //base.OnScrolled(recyclerView, dx, dy);
            var lastVisibleItemPosition = 0;
            var totalItemCount = _layoutManager.ItemCount;
            if (_layoutManager is StaggeredGridLayoutManager)
            {
                var lastVisibleItemPositions =((StaggeredGridLayoutManager) _layoutManager).FindLastVisibleItemPositions(null);
                lastVisibleItemPosition = GetLastVisibleItem(lastVisibleItemPositions);
            }
            else if (_layoutManager is LinearLayoutManager)
            {
                lastVisibleItemPosition = ((LinearLayoutManager) _layoutManager).FindLastVisibleItemPosition();
            }
            else if (_layoutManager is GridLayoutManager)
            {
                lastVisibleItemPosition = ((GridLayoutManager)_layoutManager).FindLastVisibleItemPosition();
            }

            //if the total item count is zero and the previous isn't, assume the list is invalidated and should be reset back to inital state
            if (totalItemCount < _previousTotalItemCount)
            {
                _currentPage = _startingPageIndex;
                _previousTotalItemCount = totalItemCount;
                if (totalItemCount == 0)
                {
                    _loading = true;
                }
            }

            //if it's still loading, we check to see if the dataset count has changed, if so we conclude it has finished loading and update the current pgae number and total item count
            if (_loading && totalItemCount > _previousTotalItemCount)
            {
                _loading = false;
                _previousTotalItemCount = totalItemCount;
            }

            //if it isn't current loading we check to see if we have breached the visibleThreshold and need to reload mroe data.
            //if we do need to reload more data we invoke the loadmoredata func that was passed in to the ctor
            if (!_loading && lastVisibleItemPosition + _visibleThreshold > totalItemCount)
            {
                _currentPage++;
                _loadMoreDataFunc?.Invoke(_currentPage, totalItemCount);
                _loading = true;
            }

        }

    }
}