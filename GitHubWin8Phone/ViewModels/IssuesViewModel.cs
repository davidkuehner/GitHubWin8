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
    /// Manages all issues for the current repo in the app
    /// </summary>
    public class IssuesViewModel : INotifyPropertyChanged
    {
        public IssuesViewModel(Repository repository)
        {
            this.OpenIssues = new ObservableCollection<IssueItemViewModel>();
            this.ClosedIssues = new ObservableCollection<IssueItemViewModel>();
            this.Repository = repository;
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<IssueItemViewModel> OpenIssues { get; private set; }
        public ObservableCollection<IssueItemViewModel> ClosedIssues { get; private set; }

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

        private Repository repository;
        /// <summary>
        /// Repo this issue is related to
        /// </summary>
        public Repository Repository
        {
            get { return repository; }
            set
            {
                repository = value;
                NotifyPropertyChanged("Repository");
            }
        }

        /// <summary>
        /// Loads data from GitHub and puts it into an observable collection
        /// </summary>
        public async void LoadData()
        {
            if (!IsDataLoaded)
            {
                this.OpenIssues.Clear();
                this.ClosedIssues.Clear();

                //Open issues
                RepositoryIssueRequest r = new RepositoryIssueRequest();
                r.State = ItemState.Open;

                IReadOnlyList<Octokit.Issue> issues = await App.GitHubClient.Issue.GetForRepository(Repository.Owner.Login, Repository.Name, r);                

                foreach (Issue issue in issues)
                {                    
                    this.OpenIssues.Add(new IssueItemViewModel(issue));                 
                }

                //Closed issues                
                r.State = ItemState.Closed;

                issues = await App.GitHubClient.Issue.GetForRepository(Repository.Owner.Login, Repository.Name, r);

                foreach (Issue issue in issues)
                {
                    this.ClosedIssues.Add(new IssueItemViewModel(issue));
                }


                this.IsDataLoaded = true;
            }
        }

        /// <summary>
        /// Closes an issue on github
        /// </summary>
        /// <param name="issue">Issue to close</param>
        /// <returns>true if issue closed successfully, false otherwise</returns>
        public async Task<bool> CloseIssue(Issue issue)
        {
            IssueUpdate update = new IssueUpdate();
            update.State = ItemState.Closed;
            Issue result = await App.GitHubClient.Issue.Update(Repository.Owner.Login, Repository.Name, issue.Number, update);

            if(result == null)
            {
                return false;
            }
            else
            {
                ReloadData();
                return true;
            }
        }

        /// <summary>
        /// Reopen an issue on github
        /// </summary>
        /// <param name="issue">Issue to repoen</param>
        /// <returns>true if issue reopened successfully, false otherwise</returns>
        public async Task<bool> ReopenIssue(Issue issue)
        {
            IssueUpdate update = new IssueUpdate();
            update.State = ItemState.Open;
            Issue result = await App.GitHubClient.Issue.Update(Repository.Owner.Login, Repository.Name, issue.Number, update);

            if (result == null)
            {
                return false;
            }
            else
            {
                ReloadData();
                return true;
            }
        }

        /// <summary>
        /// Creates an issue on github
        /// </summary>
        /// <param name="issue">New issue</param>
        /// <returns>true if issue created successfully, false otherwise</returns>
        public async Task<bool> CreateIssue(NewIssue issue)
        {            
            Issue result = await App.GitHubClient.Issue.Create(Repository.Owner.Login, Repository.Name, issue);            
            return result != null;            
        }

        /// <summary>
        /// Gets all milestones for the current repo
        /// </summary>
        /// <returns>list of milestones</returns>
        public async Task<IList<Milestone>> GetMilestones()
        {
            List<Milestone> result = new List<Milestone>();
            result.Add(null); //No milestone

            IReadOnlyList<Octokit.Milestone> milestones = await App.GitHubClient.Issue.Milestone.GetForRepository(Repository.Owner.Login, Repository.Name);
            result.AddRange(milestones);

            return result;
        }

        /// <summary>
        /// Gets all collaborators for the current repo
        /// </summary>
        /// <returns>list of collaborators names</returns>
        public async Task<List<string>> GetCollaboratorsAsString()
        {
            List<string> result = new List<string>();
            result.Add(null);
            IReadOnlyList<Octokit.User> users = await App.GitHubClient.Repository.RepoCollaborators.GetAll(Repository.Owner.Login, Repository.Name);            

            foreach(User user in users)
            {
                result.Add(user.Login);
            }

            return result;
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