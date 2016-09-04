using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Controls.Sample.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Views;

namespace MvvmCross.Controls.Sample.Droid.Views
{
    [Activity(Label = "View for FirstViewModel",LaunchMode = LaunchMode.SingleTop, Name="mvvmcross.controls.sample.droid.views.FirstView")]
    public class FirstView : MvxCachingFragmentActivity<FirstViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FirstView);
        }
    }
}
