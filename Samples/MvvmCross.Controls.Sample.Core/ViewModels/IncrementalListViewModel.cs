using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Controls.IncrementalLoadingList;
using MvvmCross.Controls.Sample.Core.Model;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace MvvmCross.Controls.Sample.Core.ViewModels
{
    public class IncrementalListViewModel : MvxViewModel
    {
        private readonly IIncrementalCollectionFactory _incrementalCollectionFactory;

        public IncrementalListViewModel(IIncrementalCollectionFactory incrementalCollectionFactory)
        {
            _incrementalCollectionFactory = incrementalCollectionFactory;
            Mvx.TaggedTrace(nameof(IncrementalListViewModel), "CTOR");
        }

        protected const int PageSize = 25;

        //private ObservableCollection<ToDoItem> _listItems;
        //public ObservableCollection<ToDoItem> ListItems => _listItems ?? (_listItems = _incrementalCollectionFactory.GetCollection(LoadIncrementalDataAsync, PageSize));
        private ICoreSupportIncrementalLoading _incrementalCollection;

        private ObservableCollection<ToDoItem> _listItems;

        public ObservableCollection<ToDoItem> ListItems
        {
            get
            {
                Mvx.TaggedTrace(nameof(IncrementalListViewModel), "ListItems - Get");
                if (_listItems == null)
                {
                    Mvx.TaggedTrace(nameof(IncrementalListViewModel), "ListItems - Get (null, create new)");
                    _incrementalCollection = _incrementalCollectionFactory.GetCollection(LoadIncrementalDataAsync, PageSize) as ICoreSupportIncrementalLoading;
                    _incrementalCollection.ItemsLoaded += OnItemsLoaded;
                    _listItems = (ObservableCollection<ToDoItem>)_incrementalCollection;
                }
                return _listItems;

            }
        }

        private void OnItemsLoaded(object sender, ItemsLoadedEventArgs e)
        {
            Mvx.TaggedTrace(nameof(IncrementalListViewModel), "OnItemsLoaded (current count={0}, total count={1})", e.CurrentLoadCount, e.TotalRecordCount);

        }


        private async Task<ObservableCollection<ToDoItem>> LoadIncrementalDataAsync(int count, int pageSize)
        {
            var items = new ObservableCollection<ToDoItem>();
            for (var i = count; i <= count + PageSize; i++)
            {
                items.Add(ToDoItem.GetToDoItem(i));
            }

            return items;
        }

    }
}
