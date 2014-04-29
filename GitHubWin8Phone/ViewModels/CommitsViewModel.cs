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
    /// <summary>
    /// Manages all commits for the current repo in the app
    /// </summary>
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
        /// <summary>
        /// GitHub Branch the commits are related to
        /// </summary>
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
        /// Loads data from GitHub and puts it into an observable collection
        /// </summary>
        public async void LoadData()
        {
            if (!IsDataLoaded)
            {
                this.Items.Clear();

                Octokit.ApiConnection apiConnection = new ApiConnection(App.GitHubClient.Connection);
                CommitsClient commitsClient = new CommitsClient(apiConnection);
                Repository repo = branch.Commit.Repository;
                IReadOnlyList<Octokit.Commit> commits = await GetAllCommits(apiConnection, repo.Owner.Login, repo.Name, branch.Commit.Sha);

                foreach (Octokit.Commit commit in commits)
                    {
                        Commit c = await commitsClient.Get(repo.Owner.Login, repo.Name, commit.Sha);
                        this.Items.Add(new CommitItemViewModel(c));
                    } 

                this.IsDataLoaded = true;
            }
        }

        /// <summary>
        /// Loads all commits from github for a given user and reference (branch, ...)
        /// </summary>
        /// <param name="apiConnection">apiConnection object from Octokit</param>
        /// <param name="owner">Name of the repo owner (login)</param>
        /// <param name="name">Name of the repo</param>
        /// <param name="reference"></param>
        /// <returns>List of found commits</returns>
        public Task<IReadOnlyList<Commit>> GetAllCommits(ApiConnection apiConnection, string owner, string name, string reference)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("sha", reference);

            return apiConnection.GetAll<Commit>(RepoCommits(owner, name), parameters);
        }

        /// <summary>
        /// Creates a github URI from owner name and repo name
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repo"></param>
        /// <returns></returns>
        public static Uri RepoCommits(string owner, string repo)
        {
            return new Uri(String.Format(App.GitHubApiPrefix+"repos/{0}/{1}/commits", owner, repo));
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