using MvvmCross.Core.ViewModels;

namespace MvvmCross.Controls.Sample.Core.ViewModels
{
    public class FirstViewModel : MvxViewModel
    {
        public IMvxCommand ShowGroupedListCommand => new MvxCommand(() => ShowViewModel<GroupedListViewModel>());

        public IMvxCommand ShowIncrementalListCommand => new MvxCommand(() => ShowViewModel<IncrementalListViewModel>());

    }


}
