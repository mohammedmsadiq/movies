using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using movies.Interfaces;
using movies.Models;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace movies.ViewModels
{
    public class MovieDetailPageViewModel : ViewModelBase
    {
        Result selectedItem;

        MovieDetailModel itemDetail;
        private ObservableCollection<Genre> genreData;

        public MovieDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ISimpleRequestService simpleRequestService) : base(navigationService, dialogService, simpleRequestService)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters == null)
            {
                return;
            }
            this.LoadSelectedItemDetail(parameters);
        }

        private async Task LoadSelectedItemDetail(INavigationParameters parameters)
        {
            this.selectedItem = (Result)parameters["ItemSelected"];

            var URLParams = new NavigationParameters();
            URLParams.Add("api_key", AppConfig.ConfigConstants.ApiKey);
            URLParams.Add("language", CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);

            string detailItem = selectedItem.id + URLParams.ToString();
            ItemDetail = await SimpleRequestService.GetSimpleAsync<MovieDetailModel>(detailItem);

            GenreData = new ObservableCollection<Genre>();
            GenreData.Clear();

            if (ItemDetail.genres != null)
            {
                foreach (var item in ItemDetail.genres)
                {
                    this.GenreData.Add(item);
                }
            }
        }

        public ObservableCollection<Genre> GenreData
        {
            get => this.genreData;
            set => SetProperty(ref this.genreData, value);
        }

        public MovieDetailModel ItemDetail
        {
            get => this.itemDetail;
            set => SetProperty(ref this.itemDetail, value);
        }
    }
}