﻿using System;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Cirrious.Conference.Core;
using Cirrious.Conference.Core.Converters;
using Cirrious.Conference.UI.Droid.Bindings;
using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding.Droid;
using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.Plugins.Json;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;

namespace Cirrious.Conference.UI.Droid
{
    public class Setup
        : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new NoSplashScreenConferenceApp();
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

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterFactory(new MvxCustomBindingFactory<Button>("IsFavorite", (button) => new FavoritesButtonBinding(button)));
        }

        protected override void InitializeLastChance()
        {
            Cirrious.MvvmCross.Plugins.DownloadCache.PluginLoader.Instance.EnsureLoaded();
            base.InitializeLastChance();
        }
    }
}

