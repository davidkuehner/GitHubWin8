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
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = this;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        #region Event handlers

        private void MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((Pivot)sender).SelectedIndex)
            {
                case 0:
                    ApplicationBar = (ApplicationBar)this.Resources["NewsAppBar"];
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

        private void BtnRefreshNewsAppBar_Click(object sender, EventArgs e)
        {
            //Refresh news here!
            MessageBox.Show("Still to implement :) ");
        }

        #endregion


        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}