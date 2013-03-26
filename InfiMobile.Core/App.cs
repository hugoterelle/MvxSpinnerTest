using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.ViewModels;
using InfiMobile.Core.Models;
using InfiMobile.Core.ViewModels;
using System.Collections.Generic;

namespace InfiMobile.Core
{
    public class App : MvxApplication
    {
        //TODO Need to load language from database
        public const Language DefaultLanguage = Language.French;

        public App()
        {
            RegisterAppStart<EditPatientViewModel>();
        }
    }
}
