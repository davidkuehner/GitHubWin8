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
    /// <summary>
    /// Logic related to the branche view page
    /// </summary>
    public partial class ViewBranchPage : PhoneApplicationPage
    {
        public ViewBranchPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Branch branch = PhoneApplicationService.Current.State["branch"] as Branch;

            App.CommitsViewModel = new CommitsViewModel(branch);
            App.CommitsViewModel.LoadData();

            this.DataContext = App.CommitsViewModel;          
        }

        private void BtnBackAppBar_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }        
    }
}