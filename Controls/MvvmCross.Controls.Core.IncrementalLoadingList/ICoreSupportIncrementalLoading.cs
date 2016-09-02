using System.Threading.Tasks;

namespace MvvmCross.Controls.Core.IncrementalLoadingList
{
    //This should be in a portable class library
    public interface ICoreSupportIncrementalLoading
    {
        Task LoadMoreItemsAsync(bool allowDuplicates = false);
    }
}