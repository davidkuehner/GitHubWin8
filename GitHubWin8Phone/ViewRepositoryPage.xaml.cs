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
    /// <summary>
    /// Logic related to the repository view page
    /// </summary>
    public partial class ViewRepositoryPage : PhoneApplicationPage, INotifyPropertyChanged
    {
        public ViewRepositoryPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Repository = PhoneApplicationService.Current.State["repository"] as Repository;
            if (Repository != null)
            {                
                App.BranchesViewModel = new BranchesViewModel(Repository);
                App.IssuesViewModel = new IssuesViewModel(Repository);
                App.IssuesViewModel.LoadData();
                LoadReadme();             
            }
            else
            {
                MessageBox.Show("Repository not found, sorry...");
                NavigationService.GoBack();
            }
        }

        private async void LoadReadme()
        {
            Readme = await App.GitHubClient.Repository.GetReadmeHtml(Repository.Owner.Login, Repository.Name);
            web.NavigateToString(Readme);            
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

        #region Properties
        private Repository repository;
        /// <summary>
        /// Current repository, bound to the GUI
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

        private string readme;
        /// <summary>
        /// Readme as HTML of the current repo
        /// </summary>
        public string Readme
        {
            get { return readme; }
            set
            {
                readme = value;
                NotifyPropertyChanged("Readme");
            }
        }
        #endregion

        #region Event handlers
        private void MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((Pivot)sender).SelectedIndex)
            {
                case 0:
                    ApplicationBar = (ApplicationBar)this.Resources["BranchesAppBar"];
                    break;
                case 1:
                    ApplicationBar = (ApplicationBar)this.Resources["IssuesAppBar"];
                    break;
                case 2:
                    ApplicationBar = (ApplicationBar)this.Resources["ReadmeAppBar"];
                    break;
                default:
                    ApplicationBar = null;
                    break;
            }
        }

        private void BtnBackIssuesAppBar_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnRefreshIssuesAppBar_Click(object sender, EventArgs e)
        {
            if(App.IssuesViewModel != null)
            {
                App.IssuesViewModel.ReloadData();
            }
        }

        private void BtnAddIssuesAppBar_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/CreateIssuePage.xaml", UriKind.Relative));
        }

        private void BtnBackBranchesAppBar_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnRefreshBranchesAppBar_Click(object sender, EventArgs e)
        {
            if (App.BranchesViewModel != null)
            {
                App.BranchesViewModel.ReloadData();
            }
        }        

        private void BtnBackReadmeAppBar_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnRefreshReadmeAppBar_Click(object sender, EventArgs e)
        {
            LoadReadme();
        }
        #endregion
    }
}