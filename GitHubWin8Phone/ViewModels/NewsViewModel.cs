using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GitHubWin8Phone.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubWin8Phone.ViewModels
{
    public class NewsViewModel : INotifyPropertyChanged
    {
        public NewsViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

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
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public async void LoadData()
        {
            if(!IsDataLoaded)
            {
                this.Items.Clear();

                IReadOnlyList<Octokit.Notification> notifications = await App.GitHubClient.Notification.GetAllForCurrent();

                foreach (Octokit.Notification notif in notifications)
                {
                    this.Items.Add(new ItemViewModel() { LineOne = notif.Subject.Title, LineTwo = notif.Subject.Url, LineThree = notif.Subject.LatestCommentUrl });
                }

                this.IsDataLoaded = true;
            }
            
        }

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