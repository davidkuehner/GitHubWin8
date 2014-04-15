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

namespace GitHubWin8Phone
{
    public partial class ViewBranchPage : PhoneApplicationPage
    {
        public ViewBranchPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Branch branch = PhoneApplicationService.Current.State["branch"] as Branch;                  

            this.DataContext = branch;          
        }
    }
}