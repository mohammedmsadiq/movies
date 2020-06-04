using System;
using System.Globalization;
using DLToolkit.Forms.Controls;
using movies.Interfaces;
using movies.Services;
using movies.ViewModels;
using movies.Views;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace movies
{
    public partial class App
    {
        /*
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor.
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            FlowListView.Init();

            // Width (in pixels)
            var width = DeviceDisplay.MainDisplayInfo.Width;

            // Height (in pixels)
            var height = DeviceDisplay.MainDisplayInfo.Height;

            var resources = Application.Current.Resources;
            resources["width"] = width;
            resources["height"] = height;
            resources["HomePageCarouselHeight"] = (height / 2) - 50;

            Device.SetFlags(new string[]
            {
                "CarouselView_Experimental",
                "MediaElement_Experimental",
                "IndicatorView_Experimental",
                "SwipeView_Experimental"
            });

            await NavigationService.NavigateAsync("MyTabbedPage?selectedTab=MoviesPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MyTabbedPage>();
            containerRegistry.RegisterForNavigation<MoviesPage, MoviesPageViewModel>();
            containerRegistry.RegisterForNavigation<TVPage, TVPageViewModel>();
            containerRegistry.RegisterForNavigation<MovieDetailPage, MovieDetailPageViewModel>();


            containerRegistry.RegisterSingleton<ISimpleRequestService, SimpleRequestService>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
