using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MvvmCross.Controls.Core.IncrementalLoadingList
{
    /// <summary>
    /// based on https://github.com/HBSequence/Sequence.Plugins/tree/master/InfiniteScrollPlugin
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IncrementalCollection<T> : ObservableCollection<T>, ICoreSupportIncrementalLoading
    {
        private readonly Func<int, int, Task<ObservableCollection<T>>> _sourceDataFunc;

        public IncrementalCollection(Func<int, int, Task<ObservableCollection<T>>> sourceDataFunc, int defaultPageSize)
        {
            _sourceDataFunc = sourceDataFunc;
            DefaultPageSize = defaultPageSize;
        }

        public int DefaultPageSize { get; set; }

        public async Task LoadMoreItemsAsync(bool allowDuplicates = false)
        {
            var sourceData = await _sourceDataFunc(Count, DefaultPageSize);
            AddItemsToList(sourceData, allowDuplicates);
        }

        private void AddItemsToList(IEnumerable<T> items, bool allowDuplicates)
        {
            if (items == null) { return; }

            foreach (var item in items.Where(item => allowDuplicates || !Contains(item)))
            {
                Add(item);
            }
        }

    }
}



