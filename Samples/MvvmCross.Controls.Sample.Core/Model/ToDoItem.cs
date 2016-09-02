using System;

namespace MvvmCross.Controls.Sample.Core.Model
{
    public class ToDoItem
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime FinishBy { get; set; }

        public string FinishByDisplay => FinishBy.ToString("hh:mm tt dddd");
    }
}
