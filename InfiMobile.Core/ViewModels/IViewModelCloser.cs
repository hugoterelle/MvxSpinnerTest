using Cirrious.MvvmCross.ViewModels;

namespace InfiMobile.Core.ViewModels
{
    public interface IViewModelCloser
    {
        void RequestClose(IMvxViewModel viewModel);
    }
}
