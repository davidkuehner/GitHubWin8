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
    public class BranchesViewModel : INotifyPropertyChanged
    {
        public BranchesViewModel(Repository repository)
        {
            this.Repository = repository;
            this.Items = new ObservableCollection<BranchItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<BranchItemViewModel> Items { get; private set; }

        private Repository repository;
        public Repository Repository
        {
            get
            {
                return repository;
            }
            set
            {
                repository = value;
                NotifyPropertyChanged("Repository");
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
            if (!IsDataLoaded)
            {
                this.Items.Clear();
                   
                IReadOnlyList<Octokit.Branch> branches = await App.GitHubClient.Repository.GetAllBranches(repository.Owner.Login, repository.Name);                

                foreach (Octokit.Branch branch in branches)
                {                    
                    this.Items.Add(new BranchItemViewModel(branch));
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