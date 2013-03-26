using System;
using Android.Content;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using System.Collections.Generic;
using InfiMobile.Core;
using InfiMobile.Core.Converters;
using InfiMobile.Core.ViewModels;
using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.Binding;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Plugins.Sqlite;
using Cirrious.MvvmCross.Plugins.Sqlite.Droid;

namespace InfiMobile.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
            MvxBindingTrace.TraceBindingLevel = MvxTraceLevel.Diagnostic;
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override IEnumerable<Type> ValueConverterHolders
        {
            get { return new[] { typeof(Converters) }; }
        }

        protected override IMvxNavigationSerializer CreateNavigationSerializer()
        {
            Cirrious.MvvmCross.Plugins.Json.PluginLoader.Instance.EnsureLoaded();
            return new MvxJsonNavigationSerializer();
        }

        protected override void InitializeLastChance()
        {
            var closer = new SimpleDroidViewModelCloser();
            Mvx.RegisterSingleton<IViewModelCloser>(closer);
            Mvx.RegisterSingleton<ISQLiteConnectionFactory>(new MvxDroidSQLiteConnectionFactory());
            base.InitializeLastChance();
        }
    }
}

