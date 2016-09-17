using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

namespace MvvmCross.Controls.IncrementalLoadingList.iOS
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterSingleton<IIncrementalCollectionFactory>(new IncrementalCollectionFactory());
        }
    }
}