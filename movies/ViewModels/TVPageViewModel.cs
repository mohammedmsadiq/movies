using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using movies.Interfaces;
using movies.Models;
using Prism.Navigation;
using Prism.Services;

namespace movies.ViewModels
{
    public class TVPageViewModel : ViewModelBase
    {
        public TVPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ISimpleRequestService simpleRequestService) : base(navigationService, dialogService, simpleRequestService)
        {
        }


        private async Task loadData()
        {
            IsBusy = true;

            //this.NowPlayingData = new ObservableCollection<Result>();
            //this.UpcomingData = new ObservableCollection<Result>();
            //this.TopRatedData = new ObservableCollection<Result>();
            //this.PopularData = new ObservableCollection<Result>();

            //var URLParams = new NavigationParameters();
            //URLParams.Add("api_key", AppConfig.ConfigConstants.ApiKey);
            //URLParams.Add("region", RegionInfo.CurrentRegion.TwoLetterISORegionName);
            //URLParams.Add("language", CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);

            //string nowPLayingUrl = AppConfig.ConfigConstants.NowPlaying + URLParams.ToString();
            //var nowPLayingResult = await SimpleRequestService.GetSimpleAsync<MoviesModel>(nowPLayingUrl);
            //loadNowPlayingModel(nowPLayingResult);


            //IsBusy = false;
            //isNoImage = true;

        }
    }
}
