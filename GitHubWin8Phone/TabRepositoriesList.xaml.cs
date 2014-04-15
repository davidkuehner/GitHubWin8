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
    public partial class TabRepositoriesList : UserControl
    {
        public TabRepositoriesList()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.RepositoriesViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = App.RepositoriesViewModel;
            App.RepositoriesViewModel.LoadData();
        }       

        private void llsRepositories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(llsRepositories.SelectedItem != null)
            {
                var repoItem = llsRepositories.SelectedItem as RepositoryItemViewModel;

                PhoneApplicationService.Current.State["repository"] = repoItem.Repository;

                var frame = App.Current.RootVisual as PhoneApplicationFrame;
                frame.Navigate(new Uri("/ViewRepositoryPage.xaml", UriKind.Relative));                
            }
            llsRepositories.SelectedItem = null;
        }
        
        
       
    }
}
