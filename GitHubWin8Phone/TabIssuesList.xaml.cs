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

namespace GitHubWin8Phone
{
    /// <summary>
    /// Logic related to the issues list UserControl
    /// </summary>
    public partial class TabIssuesList : UserControl
    {
        public TabIssuesList()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = App.IssuesViewModel;
        }        

        private async void MenuItemCloseIssue_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as MenuItem).DataContext as IssueItemViewModel;

            bool ok = await App.IssuesViewModel.CloseIssue(item.Issue);
            llsOpenIssues.SelectedItem = null;

            if(ok)
            {
                MessageBox.Show("Issue closed successfuly");
            }
            else
            {
                MessageBox.Show("Error while closing issue...");
            }
        }

        private async void MenuItemOpenIssue_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as MenuItem).DataContext as IssueItemViewModel;

            bool ok = await App.IssuesViewModel.ReopenIssue(item.Issue);
            llsClosedIssues.SelectedItem = null;

            if (ok)
            {
                MessageBox.Show("Issue reopened successfuly");
            }
            else
            {
                MessageBox.Show("Error while reopening issue...");
            }
        }
        
    }
}
