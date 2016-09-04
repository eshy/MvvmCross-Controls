using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Controls.Core.IncrementalLoadingList;
using MvvmCross.Controls.Sample.Core.Model;
using MvvmCross.Core.ViewModels;

namespace MvvmCross.Controls.Sample.Core.ViewModels
{
    public class IncrementalListViewModel : MvxViewModel
    {
        private readonly IIncrementalCollectionFactory _incrementalCollectionFactory;
        private ObservableCollection<ToDoItem> _listItems;

        public IncrementalListViewModel(IIncrementalCollectionFactory incrementalCollectionFactory)
        {
            _incrementalCollectionFactory = incrementalCollectionFactory;
        }

        protected const int PageSize = 25;

        public ObservableCollection<ToDoItem> ListItems => _listItems ?? (_listItems = _incrementalCollectionFactory.GetCollection(LoadIncrementalDataAsync, PageSize));

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
