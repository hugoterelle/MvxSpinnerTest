using Android.App;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.ViewModels;
using InfiMobile.Core.ViewModels;

namespace InfiMobile.Droid.Views
{
    [Activity(Label = "@string/patient", MainLauncher = true, Icon = "@null")]
    [MvxViewFor(typeof(EditPatientViewModel))]
    public class EditPatientView : MvxActivity
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.EditPatient);
        }
    }
}
