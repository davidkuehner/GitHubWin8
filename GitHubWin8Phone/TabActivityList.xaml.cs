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
using System.Collections.ObjectModel;
using Octokit;

namespace GitHubWin8Phone
{
    public partial class TabActivityList : UserControl
    {
        public TabActivityList()
        {
            InitializeComponent();

            DataContext = App.ActivityViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = App.ActivityViewModel;
            App.ActivityViewModel.LoadData();
        }

        private async void llsActivity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (llsActivity.SelectedItem != null)
            {
                var activityItem = llsActivity.SelectedItem as ActivityItemViewModel;

                Repository repo = await activityItem.getRelatedRepository();
                PhoneApplicationService.Current.State["repository"] = repo;

                var frame = App.Current.RootVisual as PhoneApplicationFrame;
                frame.Navigate(new Uri("/ViewRepositoryPage.xaml", UriKind.Relative));
            }
            llsActivity.SelectedItem = null;
        }
    }
}
