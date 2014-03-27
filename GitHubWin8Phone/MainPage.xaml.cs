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

namespace GitHubWin8Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            this.test();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void test()
        {
            Credentials credential = new Credentials("GitHubWin8Tester", "hearc2014");
            InMemoryCredentialStore inMemoryCredentialStore = new InMemoryCredentialStore(credential);

            var github = new GitHubClient(new ProductHeaderValue("GitHubWin8"), inMemoryCredentialStore);
            //var user = await github.User.Get("davidkuhner");

            NewRepository nr = new NewRepository();
            nr.Name = "APItest";
            //github.Repository.Create(nr);
            MessageBox.Show("Created");

            /*MessageBox.Show("Befor getting task");
            Task<Repository> task = github.Repository.Get("davidkuhner", "GitHubWin8");
            MessageBox.Show("Waiting on task");
            MessageBox.Show("Task ready");
            Repository repo = task.;

            MessageBox.Show("Result ready");

            MessageBox.Show("hello");//repo.Description);
            MessageBox.Show("END");*/
            //Console.WriteLine(user.Followers + " folks love the half ogre!");
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

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