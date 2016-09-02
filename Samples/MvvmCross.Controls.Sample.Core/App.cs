using MvvmCross.Controls.Sample.Core.ViewModels;
using MvvmCross.Platform.IoC;

namespace MvvmCross.Controls.Sample.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<FirstViewModel>();
        }
    }
}
