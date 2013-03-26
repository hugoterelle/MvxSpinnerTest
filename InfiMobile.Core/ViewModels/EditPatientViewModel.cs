using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.ViewModels;
using InfiMobile.Core.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

namespace InfiMobile.Core.ViewModels
{
    public class EditPatientViewModel : BaseViewModel<Patient>
    {
        private List<Gender> _genders;
        public List<Gender> Genders
        {
            get
            {
                if (_genders == null)
                    _genders = Gender.List();
                return _genders;
            }
        }

        private Gender _gender;
        public Gender Gender
        {
            get
            {
                return _gender;
            }

            set
            {
                if (value != null)
                {
                    Model.Gender = value.GenderEnum;
                    _gender = value;
                    RaisePropertyChanged(() => Gender);
                }
            }
        }

        public ICommand CommandSave
        {
            get
            {
                return new MvxCommand(() =>
                {
                    Debug.WriteLine("Patient=" + Model.ToString());
                });
            }
        }
    }
}
