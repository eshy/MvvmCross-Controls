using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MvvmCross.Controls.IncrementalLoadingList
{
    public class IncrementalCollectionFactory : IIncrementalCollectionFactory
    {
        public ObservableCollection<T> GetCollection<T>(Func<int, int, Task<ObservableCollection<T>>> sourceDataFunc, int defaultPageSize = 10)
        {
            return new IncrementalCollection<T>(sourceDataFunc, defaultPageSize);
        }

        public ObservableCollection<T> GetCollection<T>(Func<int, int, Task<ObservableCollection<T>>> sourceDataFunc, Action onBatchStart, Action<ObservableCollection<T>> onBatchComplete,
            int defaultPageSize = 10)
        {
            return new IncrementalCollection<T>(sourceDataFunc, onBatchStart, onBatchComplete, defaultPageSize);
        }
    }
}