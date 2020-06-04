using System;
using System.Collections.Generic;
using System.Diagnostics;
using movies.Controls;
using movies.Models;
using movies.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace movies.Views
{
    public partial class MoviesPage : ContentPage
    {
        public MoviesPage()
        {
            InitializeComponent();
        }

        void TapGestureRecognizer_Tapped(Object sender, EventArgs e)
        {
            int currentIndex = LatestCarousel.Position;
            if (currentIndex >= 0 && currentIndex < ((MoviesPageViewModel)this.BindingContext).UpcomingData.Count)
            {
                var selectedItem = ((MoviesPageViewModel)this.BindingContext).UpcomingData[currentIndex];
                ((MoviesPageViewModel)this.BindingContext).ItemSelectedCommand.Execute(selectedItem as Result);
            }
        }
    }
}