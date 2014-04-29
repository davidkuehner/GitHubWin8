using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GitHubWin8Phone.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubWin8Phone.ViewModels
{
    /// <summary>
    /// Manages all repositories for the current logged in user
    /// </summary>
    public class RepositoriesViewModel : INotifyPropertyChanged
    {
        public RepositoriesViewModel()
        {
            this.Items = new ObservableCollection<RepositoryItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<RepositoryItemViewModel> Items { get; private set; }             

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

                IReadOnlyList<Octokit.Repository> repositories = await App.GitHubClient.Repository.GetAllForCurrent();

                foreach (Octokit.Repository repo in repositories)
                {
                    this.Items.Add(new RepositoryItemViewModel(repo));
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