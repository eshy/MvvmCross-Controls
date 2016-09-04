using Android.Runtime;
using MvvmCross.Controls.Sample.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;

namespace MvvmCross.Controls.Sample.Droid.Views.Fragments
{
    [MvxFragment(typeof(FirstViewModel), Resource.Id.contentFrame)]
    [Register("mvvmcross.controls.sample.droid.views.fragments.IncrementalListFragment")]
    public class IncrementalListFragment : BaseFragment<IncrementalListViewModel>
    {
        protected override int FragmentId => Resource.Layout.IncrementalListView;
    }
}