using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using MvvmCross.Controls.Sample.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.WindowsCommon.Views;

namespace MvvmCross.Controls.Sample.WindowsPhoneApp.Views
{
    public sealed partial class FirstView : MvxWindowsPage
    {
        public FirstView()
        {
            this.InitializeComponent();
        }


        private IMvxViewModel InitializeViewModel(Type type, NavigationMode navigationMode)
        {
            var request = new MvxViewModelRequest(type, null, null, null);
            var vm = Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel(request, null);
            if (vm == null)
            {
                return null;
            }
            vm.Start();
            return vm;
        }

        private void SetDataContext(FrameworkElement item, Type viewModelType, NavigationMode navigationMode)
        {
            if (item == null) { return; }
            var viewModel = InitializeViewModel(viewModelType, navigationMode);
            item.DataContext = viewModel;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SetDataContext(TabsContainer.Items[0] as FrameworkElement, typeof(GroupedListViewModel), e.NavigationMode);
            SetDataContext(TabsContainer.Items[1] as FrameworkElement, typeof(IncrementalListViewModel), e.NavigationMode);

        }

    }
}
