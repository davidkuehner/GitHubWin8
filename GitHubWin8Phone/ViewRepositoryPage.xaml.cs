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

namespace GitHubWin8Phone
{
    public partial class ViewRepositoryPage : PhoneApplicationPage
    {
        public ViewRepositoryPage()
        {
            InitializeComponent();
        }

        private void BtnBackAppBar_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();               
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Repository repo = PhoneApplicationService.Current.State["repository"] as Repository;
            if(repo != null)
            { 
                this.DataContext = repo;
                App.BranchesViewModel = new BranchesViewModel(repo);                                
            }
            else
            {
                NavigationService.GoBack();
            }
        }

        private void BtnRefreshAppBar_Click(object sender, EventArgs e)
        {
            if(App.BranchesViewModel != null)
            {
                App.BranchesViewModel.ReloadData();
            }
        }               
    }
}