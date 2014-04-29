using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GitHubWin8Phone.ViewModels;
using Octokit;

namespace GitHubWin8Phone
{
    /// <summary>
    /// Logic related to the branches list UserControl
    /// </summary>
    public partial class TabBranchesList : UserControl
    {
        public TabBranchesList()
        {
            InitializeComponent();
            DataContext = App.BranchesViewModel;
        }        

        private void llsBranches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (llsBranches.SelectedItem != null)
            {
                Repository repo = PhoneApplicationService.Current.State["repository"] as Repository;
                var branchItem = llsBranches.SelectedItem as BranchItemViewModel;
                branchItem.Branch.Commit.Repository = repo;

                PhoneApplicationService.Current.State["branch"] = branchItem.Branch;

                var frame = App.Current.RootVisual as PhoneApplicationFrame;
                frame.Navigate(new Uri("/ViewBranchPage.xaml", UriKind.Relative));
            }
            llsBranches.SelectedItem = null;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(App.BranchesViewModel != null)
            {
                DataContext = App.BranchesViewModel;
                App.BranchesViewModel.LoadData();                                
            }
        }

    }
}
