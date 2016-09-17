using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

namespace MvvmCross.Controls.IncrementalLoadingList.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterSingleton<IIncrementalCollectionFactory>(new IncrementalCollectionFactory());
        }
    }
}