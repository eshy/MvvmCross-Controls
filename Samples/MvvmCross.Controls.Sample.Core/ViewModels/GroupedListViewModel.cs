using System.Collections.Generic;
using System.Linq;
using MvvmCross.Controls.Sample.Core.Helpers;
using MvvmCross.Controls.Sample.Core.Model;
using MvvmCross.Core.ViewModels;

namespace MvvmCross.Controls.Sample.Core.ViewModels
{
    public class GroupedListViewModel : MvxViewModel
    {
        public ObservableRangeCollection<Grouping<string, ToDoItem>> GroupedItems { get; set; } = new ObservableRangeCollection<Grouping<string, ToDoItem>>();


        public override void Start()
        {
            base.Start();

            LoadToDoItems();
        }

        private void LoadToDoItems()
        {
            var items = new List<ToDoItem>();
            for (var i = 1; i <= 20; i++)
            {
                items.Add(ToDoItem.GetToDoItem(i));
            }

            var sorted = from item in items
                orderby item.FinishBy
                group item by item.FinishByDisplay into itemGroup
                select new Grouping<string, ToDoItem>(itemGroup.Key, itemGroup);
            GroupedItems.Clear();
            GroupedItems.AddRange(sorted);
        }
    }
}