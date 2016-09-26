using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MvvmCross.Controls.IncrementalLoadingList
{
    /// <summary>
    /// based on https://github.com/HBSequence/Sequence.Plugins/tree/master/InfiniteScrollPlugin
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IncrementalCollection<T> : ObservableCollection<T>, ICoreSupportIncrementalLoading
    {
        private readonly Func<int, int, Task<ObservableCollection<T>>> _sourceDataFunc;
        private readonly Action _onBatchStart;
        private readonly Action<ObservableCollection<T>> _onBatchComplete;

        public IncrementalCollection(Func<int, int, Task<ObservableCollection<T>>> sourceDataFunc, int defaultPageSize)
        {
            _sourceDataFunc = sourceDataFunc;
            DefaultPageSize = defaultPageSize;
        }

        public IncrementalCollection(Func<int, int, Task<ObservableCollection<T>>> sourceDataFunc, Action onBatchStart, Action<ObservableCollection<T>> onBatchComplete, int defaultPageSize) : this(sourceDataFunc, defaultPageSize)
        {
            _onBatchStart = onBatchStart;
            _onBatchComplete = onBatchComplete;
        }

        public int DefaultPageSize { get; set; }

        public async Task LoadMoreItemsAsync()
        {
            _onBatchStart?.Invoke();
            var sourceData = await _sourceDataFunc(Count, DefaultPageSize);
            AddItemsToList(sourceData);
            _onBatchComplete?.Invoke(sourceData);
            OnItemsLoaded(new ItemsLoadedEventArgs
            {
                CurrentLoadCount = sourceData.Count,
                TotalRecordCount = Items.Count
            });
        }

        public event ItemsLoadedEventHandler ItemsLoaded;
        protected virtual void OnItemsLoaded(ItemsLoadedEventArgs e)
        {
            ItemsLoaded?.Invoke(this, e);
        }

        private void AddItemsToList(IEnumerable<T> items)
        {
            if (items == null) { return; }

            foreach (var item in items)
            {
                //Don't add duplicates
                if (!Contains(item))
                {
                    Add(item);
                }
            }
        }

    }
}



