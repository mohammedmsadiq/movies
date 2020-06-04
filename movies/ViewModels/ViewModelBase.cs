using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using movies.Interfaces;
using Prism;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace movies.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible, IPageLifecycleAware, IApplicationLifecycleAware, IConfirmNavigationAsync,
      IConfirmNavigation, IActiveAware, INotifyPropertyChanged
    {
        protected readonly INavigationService NavigationService;
        protected readonly IPageDialogService DialogService;
        protected readonly ISimpleRequestService SimpleRequestService;

        public ICommand BackCommand { get; set; }

        public ViewModelBase(INavigationService navigationService, IPageDialogService dialogService, ISimpleRequestService simpleRequestService)
        {
            this.NavigationService = navigationService;
            this.DialogService = dialogService;
            this.SimpleRequestService = simpleRequestService;

            this.BackCommand = new DelegateCommand(async () => { await this.BackTaskAsync(); });
        }


        private bool isNoImage = false;
        public bool IsNoImage
        {
            get => this.isNoImage;
            set => SetProperty(ref this.isNoImage, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private async Task BackTaskAsync()
        {
            await this.NavigationService.GoBackAsync();
        }

        public bool IsNotBusy { get; set; }

        private void OnIsBusyChanged() => IsNotBusy = !IsBusy;

        private void OnIsNotBusyChanged() => IsBusy = !IsNotBusy;

        #region IActiveAware
        public bool IsActive { get; set; }

        public event EventHandler IsActiveChanged;

        private void OnIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);

            if (IsActive)
            {
                OnIsActive();
            }
            else
            {
                OnIsNotActive();
            }
        }

        protected virtual void OnIsActive() { }

        protected virtual void OnIsNotActive() { }
        #endregion IActiveAware

        #region IConfirmNavigation
        public virtual bool CanNavigate(INavigationParameters parameters) => true;
        #endregion IConfirmNavigation


        #region IConfirmNavigationAsync
        public virtual Task<bool> CanNavigateAsync(INavigationParameters parameters) => Task.FromResult(CanNavigate(parameters));
        #endregion IConfirmNavigationAsync


        #region IApplicationLifecycleAware
        public virtual void OnResume() { }

        public virtual void OnSleep() { }
        #endregion IApplicationLifecycleAware


        #region IPageLifecycleAwar
        public virtual void OnAppearing() { }

        public virtual void OnDisappearing() { }
        #endregion IPageLifecycleAware


        #region IDestructible
        public virtual void Destroy() { }
        #endregion IDestructible

        #region INavigationAware
        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        public virtual void OnNavigatedTo(INavigationParameters parameters) { }
        #endregion INavigationAware

        #region ExecuteAsyncTask
        protected async Task ExecuteAction(Action action)
        {
            Device.BeginInvokeOnMainThread(() => { IsBusy = true; });

            try
            {
                action();
            }
            catch (Exception ex)
            {
                await ShowErrorMessage(ex);
            }

            Device.BeginInvokeOnMainThread(() => { IsBusy = false; });
        }

        protected async Task ExecuteAsyncTask(Func<Task> actionDelegate)
        {
            Device.BeginInvokeOnMainThread(() => { IsBusy = true; });

            try
            {
                await ExecuteAsyncTaskWithNoLoading(actionDelegate);
            }
            catch (Exception ex)
            {
                await ShowErrorMessage(ex);
            }

            Device.BeginInvokeOnMainThread(() => { IsBusy = false; });

        }

        protected async Task ExecuteAsyncTaskWithNoLoading(Func<Task> actionDelegate)
        {
            try
            {
                await actionDelegate();
            }
            catch (Exception ex)
            {
                await ShowErrorMessage(ex);
            }
        }

        protected async Task ShowErrorMessage(Exception ex)
        {
            //Dialog service, show error. 
            await DialogService.DisplayAlertAsync("Error", ex.Message, "OK");
        }
        #endregion ExecuteAsyncTask
    }
}
