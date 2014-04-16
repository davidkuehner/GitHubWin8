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

                IReadOnlyList<Octokit.Issue> issues = await App.GitHubClient.Issue.GetForRepository(Repository.Owner.Login, Repository.Name);                
                
                foreach(Issue issue in issues)
                {
                    if(issue.State == ItemState.Open)
                    {
                        this.OpenIssues.Add(new IssueItemViewModel(issue));
                    }
                    else
                    {
                        this.ClosedIssues.Add(new IssueItemViewModel(issue));
                    }
                }                

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