using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Controls.Sample.Core.Helpers;
using MvvmCross.Controls.Sample.Core.Model;
using MvvmCross.Core.ViewModels;

namespace MvvmCross.Controls.Sample.Core.ViewModels
{
    public class FirstViewModel 
        : MvxViewModel
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
            var rnd = new Random();
            for (var i=1; i <= 20; i++)
            {
                var num = (i%3) + 1;// rnd.Next(1, 13);
                items.Add(new ToDoItem
                {
                    Title = "To Do " + i,
                    Description = "The description for task " + i,
                    FinishBy = GetDeadlineTime(num)
                });
            }

            var sorted = from item in items
                         orderby item.FinishBy
                         group item by item.FinishByDisplay into itemGroup
                         select new Grouping<string, ToDoItem>(itemGroup.Key, itemGroup);
            GroupedItems.Clear();
            GroupedItems.AddRange(sorted);
        }

        private DateTime GetDeadlineTime(int num)
        {
            switch (num)
            {
                case 1:
                    return new DateTime(2016, 10, 1, 9, 30, 0);
                case 2:
                    return new DateTime(2016, 10, 1, 10, 45, 0);
                case 3:
                    return new DateTime(2016, 10, 1, 12, 30, 0);
                case 4:
                    return new DateTime(2016, 10, 1, 13, 45, 0);
                case 5:
                    return new DateTime(2016, 10, 1, 15, 0, 0);
                case 6:
                    return new DateTime(2016, 10, 1, 16, 15, 0);
                case 7:
                    return new DateTime(2016, 10, 2, 8, 30, 0);
                case 8:
                    return new DateTime(2016, 10, 2, 9, 45, 0);
                case 9:
                    return new DateTime(2016, 10, 2, 11, 00, 0);
                case 10:
                    return new DateTime(2016, 10, 2, 12, 30, 0);
                case 11:
                    return new DateTime(2016, 10, 2, 13, 45, 0);
                case 12:
                    return new DateTime(2016, 10, 2, 15, 0, 0);
            }

            return default(DateTime);
        }
    }

    
}
