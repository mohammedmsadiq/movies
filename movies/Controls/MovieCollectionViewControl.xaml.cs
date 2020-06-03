using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DLToolkit.Forms.Controls;
using movies.Models;
using Xamarin.Forms;

namespace movies.Controls
{
    public partial class MovieCollectionViewControl : ContentView
    {
        public ObservableCollection<Result> MovieItemSource
        {
            get
            {
                return (ObservableCollection<Result>)GetValue(MovieItemSourceProperty);
            }
            set
            {
                SetValue(MovieItemSourceProperty, value);
            }
        }


        public static readonly BindableProperty MovieItemSourceProperty =
                  BindableProperty.Create("MovieItemSource", typeof(ObservableCollection<Result>), typeof(MovieCollectionViewControl), null,
                                     BindingMode.Default, null, OnItemsSourceChanged);

        static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            System.Diagnostics.Debug.WriteLine("source changed");
        }

        public MovieCollectionViewControl()
        {
            InitializeComponent();
        }
    }
}
