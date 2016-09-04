using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MvvmCross.Controls.Core.IncrementalLoadingList
{
    public interface IIncrementalCollectionFactory
    {
        ObservableCollection<T> GetCollection<T>(Func<int, int, Task<ObservableCollection<T>>> sourceDataFunc, int defaultPageSize = 10);
    }
}