using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Bindings;
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using UIKit;

namespace MvvmCross.Controls.IncrementalLoadingList.iOS
{
    public class IncrementalStandardTableViewSource : MvxStandardTableViewSource
    {
        public IncrementalStandardTableViewSource(UITableView tableView) : base(tableView)
        {
        }

        public IncrementalStandardTableViewSource(UITableView tableView, NSString cellIdentifier) : base(tableView, cellIdentifier)
        {
        }

        public IncrementalStandardTableViewSource(UITableView tableView, string bindingText) : base(tableView, bindingText)
        {
        }

        public IncrementalStandardTableViewSource(IntPtr handle) : base(handle)
        {
        }

        public IncrementalStandardTableViewSource(UITableView tableView, UITableViewCellStyle style, NSString cellIdentifier, string bindingText, UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None) : base(tableView, style, cellIdentifier, bindingText, tableViewCellAccessory)
        {
        }

        public IncrementalStandardTableViewSource(UITableView tableView, UITableViewCellStyle style, NSString cellIdentifier, IEnumerable<MvxBindingDescription> descriptions, UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None) : base(tableView, style, cellIdentifier, descriptions, tableViewCellAccessory)
        {
        }

        #region Incremental
        //from https://github.com/HBSequence/Sequence.Plugins/blob/c289b7de47ecd2aadb98923979d285314e6c3b15/InfiniteScrollPlugin/Sequence.Plugins.InfiniteScroll.iOS/IncrementalTableViewSource.cs
        private int _lastViewedPosition;
        public void CreateBinding<TSource>(MvxViewController controller, Expression<Func<TSource, object>> sourceProperty)
        {
            controller.CreateBinding(this).To(sourceProperty).Apply();
            _lastViewedPosition = 0;
            LoadMoreItems();
        }

        private async void LoadMoreItems()
        {
            await LoadMoreItemsAsync();
        }

        public async Task LoadMoreItemsAsync()
        {
            var source = ItemsSource as ICoreSupportIncrementalLoading;
            if (source != null)
            {
                await source.LoadMoreItemsAsync();
            }

        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var position = indexPath.Row;
            if (position > _lastViewedPosition && position == ItemsSource.Count() - 1)
            {
                _lastViewedPosition = position;
                LoadMoreItems();
            }
            return base.GetOrCreateCellFor(tableView, indexPath, item);
        }

        #endregion
    }
}