using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GitHubWin8Phone.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;
using System.Windows;

namespace GitHubWin8Phone.ViewModels
{
    public class CommitsViewModel : INotifyPropertyChanged
    {
        public CommitsViewModel(Branch branch)
        {
            this.Items = new ObservableCollection<CommitItemViewModel>();
            this.Branch = branch;
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<CommitItemViewModel> Items { get; private set; }

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

        private Branch branch;

        public Branch Branch
        {
            get { return branch; }
            set
            {
                branch = value;
                NotifyPropertyChanged("Branch");
            }
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public async void LoadData()
        {
            if (!IsDataLoaded)
            {
                this.Items.Clear();
                
                //TODO LOAD commits


                this.IsDataLoaded = true;
            }
        }

        public void ReloadData()
        {
            IsDataLoaded = false;
            LoadData();
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