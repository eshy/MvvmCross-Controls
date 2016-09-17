using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using MvvmCross.Platform;

namespace MvvmCross.Controls.IncrementalLoadingList.WindowsCommon
{
    public class IncrementalCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading, ICoreSupportIncrementalLoading
    {
        private readonly Func<int, int, Task<ObservableCollection<T>>> _sourceDataFunc;
        private readonly Action _onBatchStart;
        private readonly Action<ObservableCollection<T>> _onBatchComplete;
        private int _defaultPageSize = 10;

        public IncrementalCollection(Func<int, int, Task<ObservableCollection<T>>> sourceDataFunc, int defaultPageSize)
        {
            HasMoreItems = true;
            _sourceDataFunc = sourceDataFunc;
            _defaultPageSize = defaultPageSize;
        }

        public IncrementalCollection(Func<int, int, Task<ObservableCollection<T>>> sourceDataFunc, Action onBatchStart, Action<ObservableCollection<T>> onBatchComplete, int defaultPageSize) : this(sourceDataFunc, defaultPageSize)
        {
            _onBatchStart = onBatchStart;
            _onBatchComplete = onBatchComplete;
        }

        public int DefaultPageSize
        {
            get { return _defaultPageSize; }
            set { _defaultPageSize = value; }
        }

        public bool HasMoreItems { get; private set; }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return InnerLoadMoreItemsAsync(count).AsAsyncOperation();
        }

        private async Task<LoadMoreItemsResult> InnerLoadMoreItemsAsync(uint expectedCount)
        {
            _onBatchStart?.Invoke();
            var sourceData = await _sourceDataFunc(Count, _defaultPageSize);

            uint resultCount = 0;

            if (sourceData != null)
            {
                resultCount = (uint)sourceData.Count;

                foreach (T item in sourceData)
                {
                    if (!Contains(item))
                    {
                        Add(item);
                    }
                }
            }
            HasMoreItems = resultCount > 0;

            _onBatchComplete?.Invoke(sourceData);
            ItemsLoaded?.Invoke(this,new EventArgs());

            return new LoadMoreItemsResult
            {
                Count = resultCount
            };
        }

        public Task LoadMoreItemsAsync()
        {
            return InnerLoadMoreItemsAsync(0);
        }

        public event EventHandler ItemsLoaded;
    }
}
