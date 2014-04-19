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
        /// Creates and adds a few ItemViewModel objects into the Items collection.
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

        public async Task<bool> CreateIssue(NewIssue issue)
        {            
            Issue result = await App.GitHubClient.Issue.Create(Repository.Owner.Login, Repository.Name, issue);            
            return result != null;            
        }

        public async Task<IList<Milestone>> GetMilestones()
        {
            List<Milestone> result = new List<Milestone>();
            result.Add(null); //No milestone

            IReadOnlyList<Octokit.Milestone> milestones = await App.GitHubClient.Issue.Milestone.GetForRepository(Repository.Owner.Login, Repository.Name);
            result.AddRange(milestones);

            return result;
        }

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