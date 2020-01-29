using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project_Notifications.Notifications;

namespace Project_Notifications.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService,
            INotificationManager notificationManager)
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

        #endregion


        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            NotificationTitle = "Here will be message title";
            NotificationMessage = "Some message";
        }
    }
}
