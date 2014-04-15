using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Octokit;
using GitHubWin8Phone.ViewModels;
using System.ComponentModel;

namespace GitHubWin8Phone
{
    public partial class ViewRepositoryPage : PhoneApplicationPage, INotifyPropertyChanged
    {
        public ViewRepositoryPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void BtnBackAppBar_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();               
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Repository = PhoneApplicationService.Current.State["repository"] as Repository;
            if (Repository != null)
            {                
                App.BranchesViewModel = new BranchesViewModel(Repository);
                LoadReadme();             
            }
            else
            {
                NavigationService.GoBack();
            }
        }

        private async void LoadReadme()
        {
            Readme = await App.GitHubClient.Repository.GetReadmeHtml(Repository.Owner.Login, Repository.Name);
            web.NavigateToString(Readme);            
        }
        
        private void BtnRefreshAppBar_Click(object sender, EventArgs e)
        {
            if(App.BranchesViewModel != null)
            {
                App.BranchesViewModel.ReloadData();
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
        private string readme;

        public string Readme
        {
            get { return readme; }
            set 
            { 
                readme = value;
                NotifyPropertyChanged("Readme");
            }
        }
    }
}