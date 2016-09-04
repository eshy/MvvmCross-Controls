using System;

namespace MvvmCross.Controls.Sample.Core.Model
{
    public class ToDoItem
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime FinishBy { get; set; }

        public string FinishByDisplay => FinishBy.ToString("hh:mm tt dddd");

        public static ToDoItem GetToDoItem(int i)
        {
            var num = (i % 3) + 1;
            return new ToDoItem
            {
                Title = "To Do " + i,
                Description = "The description for task " + i,
                FinishBy = GetDeadlineTime(num)
            };
        }


        private static DateTime GetDeadlineTime(int num)
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
