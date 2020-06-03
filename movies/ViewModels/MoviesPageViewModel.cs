using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using movies.Interfaces;
using movies.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace movies.ViewModels
{
    public class MoviesPageViewModel : ViewModelBase
    {
        private ObservableCollection<Result> nowPlayingData;
        private ObservableCollection<Result> upcomingData;
        private ObservableCollection<Result> popularData;
        private ObservableCollection<Result> topRatedData;
        private int totalPagesAvaliable;
        private int currentLoadedPage;
        private bool isLoading;
        private bool isLoadingInfinite;
        private int selectedViewModelIndex;


        public MoviesPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ISimpleRequestService simpleRequestService) : base(navigationService, dialogService, simpleRequestService)
        {
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            this.loadData();
        }

        private async Task loadData()
        {
            IsBusy = true;

            this.NowPlayingData = new ObservableCollection<Result>();
            this.UpcomingData = new ObservableCollection<Result>();
            this.TopRatedData = new ObservableCollection<Result>();
            this.PopularData = new ObservableCollection<Result>();

            var URLParams = new NavigationParameters();
            URLParams.Add("api_key", AppConfig.ConfigConstants.ApiKey);
            URLParams.Add("region", RegionInfo.CurrentRegion.TwoLetterISORegionName);
            URLParams.Add("language", CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);

            string nowPLayingUrl = AppConfig.ConfigConstants.NowPlaying + URLParams.ToString();
            var nowPLayingResult = await SimpleRequestService.GetSimpleAsync<MoviesModel>(nowPLayingUrl);
            loadNowPlayingModel(nowPLayingResult);

            string upcomingUrl = AppConfig.ConfigConstants.Upcoming + URLParams.ToString();
            var UpcomingResult = await SimpleRequestService.GetSimpleAsync<MoviesModel>(upcomingUrl);
            loadUpcomingModel(UpcomingResult);

            string popularUrl = AppConfig.ConfigConstants.Popular + URLParams.ToString();
            var PopularResult = await SimpleRequestService.GetSimpleAsync<MoviesModel>(popularUrl);
            loadPopularModel(PopularResult);

            string topRatedUrl = AppConfig.ConfigConstants.Upcoming + URLParams.ToString();
            var topRatedResult = await SimpleRequestService.GetSimpleAsync<MoviesModel>(topRatedUrl);
            loadTopRatedModel(topRatedResult);

            IsBusy = false;
            IsNoImage = true;
        }

        private void loadTopRatedModel(MoviesModel topRatedResult)
        {
            if (topRatedResult != null)
            {
                foreach (var item in topRatedResult.results)
                {
                    var taskToAdd = new Result
                    {
                        poster_path = item.poster_path,
                        adult = item.adult,
                        backdrop_path = item.backdrop_path,
                        genre_ids = item.genre_ids,
                        id = item.id,
                        original_language = item.original_language,
                        original_title = item.original_title,
                        overview = item.overview,
                        popularity = Math.Round(item.popularity, 0),
                        release_date = item.release_date,
                        title = item.title,
                        video = item.video,
                        vote_average = item.vote_average,
                        vote_count = item.vote_count
                    };
                    this.TopRatedData.Add(taskToAdd);
                }
            }
        }

        private void loadPopularModel(MoviesModel popularResult)
        {
            if (popularResult != null)
            {
                foreach (var item in popularResult.results)
                {
                    var taskToAdd = new Result
                    {
                        poster_path = item.poster_path,
                        adult = item.adult,
                        backdrop_path = item.backdrop_path,
                        genre_ids = item.genre_ids,
                        id = item.id,
                        original_language = item.original_language,
                        original_title = item.original_title,
                        overview = item.overview,
                        popularity = Math.Round(item.popularity, 0),
                        release_date = item.release_date,
                        title = item.title,
                        video = item.video,
                        vote_average = item.vote_average,
                        vote_count = item.vote_count
                    };
                    this.PopularData.Add(taskToAdd);
                }
            }
        }

        private void loadUpcomingModel(MoviesModel upcomingResult)
        {
            if (upcomingResult != null)
            {
                foreach (var item in upcomingResult.results)
                {
                    var taskToAdd = new Result
                    {
                        poster_path = item.poster_path,
                        adult = item.adult,
                        backdrop_path = item.backdrop_path,
                        genre_ids = item.genre_ids,
                        id = item.id,
                        original_language = item.original_language,
                        original_title = item.original_title,
                        overview = item.overview,
                        popularity = Math.Round(item.popularity, 0),
                        release_date = item.release_date,
                        title = item.title,
                        video = item.video,
                        vote_average = item.vote_average,
                        vote_count = item.vote_count
                    };
                    this.UpcomingData.Add(taskToAdd);
                }
            }
        }

        private void loadNowPlayingModel(MoviesModel NowPlayingResult)
        {
            if (NowPlayingResult != null)
            {
                foreach (var item in NowPlayingResult.results)
                {
                    var taskToAdd = new Result
                    {
                        poster_path = item.poster_path,
                        adult = item.adult,
                        backdrop_path = item.backdrop_path,
                        genre_ids = item.genre_ids,
                        id = item.id,
                        original_language = item.original_language,
                        original_title = item.original_title,
                        overview = item.overview,
                        popularity = item.popularity,
                        release_date = item.release_date,
                        title = item.title,
                        video = item.video,
                        vote_average = item.vote_average,
                        vote_count = item.vote_count
                    };
                    this.NowPlayingData.Add(taskToAdd);
                }
            }
        }

        public ICommand LoadMoreCommand { get; set; }

        public ObservableCollection<Result> NowPlayingData
        {
            get => this.nowPlayingData;
            set => SetProperty(ref this.nowPlayingData, value);
        }

        public ObservableCollection<Result> UpcomingData
        {
            get => this.upcomingData;
            set => SetProperty(ref this.upcomingData, value);
        }

        public ObservableCollection<Result> PopularData
        {
            get => this.popularData;
            set => SetProperty(ref this.popularData, value);
        }

        public ObservableCollection<Result> TopRatedData
        {
            get => this.topRatedData;
            set => SetProperty(ref this.topRatedData, value);
        }

        public int TotalPagesAvaliable
        {
            get => this.totalPagesAvaliable;
            set => SetProperty(ref this.totalPagesAvaliable, value);
        }

        public int CurrentLoadedPage
        {
            get => this.currentLoadedPage;
            set => SetProperty(ref this.currentLoadedPage, value);
        }

        public bool IsLoading
        {
            get => this.isLoading;
            set => SetProperty(ref this.isLoading, value);
        }

        public bool IsLoadingInfinite
        {
            get => this.isLoadingInfinite;
            set => SetProperty(ref this.isLoadingInfinite, value);
        }       

        public int SelectedViewModelIndex
        {
            get { return selectedViewModelIndex; }
            set { SetProperty(ref selectedViewModelIndex, value); }
        }
    }
}