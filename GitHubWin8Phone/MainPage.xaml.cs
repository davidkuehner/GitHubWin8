using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GitHubWin8Phone.Resources;
using Octokit;
using Octokit.Internal;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GitHubWin8Phone
{
    /// <summary>
    /// Logic related to the main page of the app
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
           
            // Set the data context of the listbox control to the sample data
            DataContext = this;
        }

        #region Event handlers

        private void MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((Pivot)sender).SelectedIndex)
            {
                case 0:
                    ApplicationBar = (ApplicationBar)this.Resources["ActivityAppBar"];
                    break;
                case 1:
                    ApplicationBar = (ApplicationBar)this.Resources["RepositoriesAppBar"];
                    break;               
                default:
                    ApplicationBar = null;
                    break;
            }
        }

        private void BtnRefreshRepositoriesAppBar_Click(object sender, EventArgs e)
        {
            App.RepositoriesViewModel.ReloadData();
        }

        private void BtnRefreshActivityAppBar_Click(object sender, EventArgs e)
        {
            App.ActivityViewModel.ReloadData();
        }

        #endregion

        private void BtnLogoutRepositoryAppBar_Click(object sender, EventArgs e)
        {
            this.leave();
        }

        private void BtnLogoutActivityAppBar_Click(object sender, EventArgs e)
        {
            this.leave();
        }

        private void leave()
        {
            App.GitHubClient = null;
            this.NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));

            //Clear previous application context
            ClearNavigationHistory();
            PhoneApplicationService.Current.State.Clear();
        }

        private void ClearNavigationHistory()
        {
            var entry = this.NavigationService.RemoveBackEntry();
            while (entry != null)
            {
                entry = this.NavigationService.RemoveBackEntry();
            }
        }        
    }
}