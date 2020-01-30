using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project_Notifications.Notifications;
using Xamarin.Forms;

namespace Project_Notifications.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        int _notificationNumber = 0;
        public MainPageViewModel(INavigationService navigationService, INotificationManager notificationManager)
            : base(navigationService, notificationManager)
        {
            Title = "Main Page";
        }

        #region Public props

        private string _notificationTitle;
        public string NotificationTitle
        {
            get { return _notificationTitle; }
            set { SetProperty(ref _notificationTitle, value); }
        }

        private string _notificationMessage;
        public string NotificationMessage
        {
            get { return _notificationMessage; }
            set { SetProperty(ref _notificationMessage, value); }
        }

        private DelegateCommand _createNotificationCommand;
        public DelegateCommand CreateNotificationCommand =>
            _createNotificationCommand ?? (_createNotificationCommand = new DelegateCommand(ExecuteCreateNotificationCommand));

        void ExecuteCreateNotificationCommand()
        {
            _notificationNumber++;
            string title = $"Local Notification #{_notificationNumber}";
            string message = $"You have now received {_notificationNumber} notifications!";
            NotificationManager.ScheduleNotification(title, message);
        }

        #endregion


        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            NotificationTitle = "Here will be message title";
            NotificationMessage = "Some message";

            NotificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
        }

        private void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                NotificationTitle = title;
                NotificationMessage = message;
            });
        }
    }
}
