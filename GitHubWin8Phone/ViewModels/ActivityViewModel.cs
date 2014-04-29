using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GitHubWin8Phone.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubWin8Phone.ViewModels
{
    /// <summary>
    /// Manages all GitHub activities in the app
    /// </summary>
    public class ActivityViewModel : INotifyPropertyChanged
    {
        public ActivityViewModel()
        {
            this.Items = new ObservableCollection<ActivityItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ActivityItemViewModel> Items { get; private set; }

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Loads data from GitHub and puts it into an observable collection
        /// </summary>
        public async void LoadData()
        {
            if (!IsDataLoaded)
            {
                this.Items.Clear();
                String currentUserName = App.GitHubClient.Credentials.Login;
                IReadOnlyList<Octokit.Activity> activities = await App.GitHubClient.Activity.Events.GetUserReceived(currentUserName);

                foreach (Octokit.Activity activity in activities)
                {
                    this.Items.Add(new ActivityItemViewModel(activity));
                }

                this.IsDataLoaded = true;
            }
        }

        /// <summary>
        /// Force data reloading
        /// </summary>
        public void ReloadData()
        {
            IsDataLoaded = false;
            LoadData();
        }

        /// <summary>
        /// Property changed event fired when a property of this class changes. Useful for WPF Data Binding
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}