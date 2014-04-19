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

        public Task<IReadOnlyList<Commit>> GetAllCommits(ApiConnection apiConnection, string owner, string name, string reference)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("sha", reference);

            return apiConnection.GetAll<Commit>(RepoCommits(owner, name), parameters);
        }

        public static Uri RepoCommits(string owner, string repo)
        {
            return new Uri(String.Format(App.GitHubApiPrefix+"repos/{0}/{1}/commits", owner, repo));
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