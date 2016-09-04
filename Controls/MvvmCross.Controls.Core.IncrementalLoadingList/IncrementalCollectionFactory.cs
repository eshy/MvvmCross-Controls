using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MvvmCross.Controls.Core.IncrementalLoadingList
{
    public class IncrementalCollectionFactory : IIncrementalCollectionFactory
    {
        public ObservableCollection<T> GetCollection<T>(Func<int, int, Task<ObservableCollection<T>>> sourceDataFunc, int defaultPageSize = 10)
        {
            return new IncrementalCollection<T>(sourceDataFunc, defaultPageSize);
        }
    }
}