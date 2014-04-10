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
            Console.WriteLine("Repo loaded");

            App.RepositoriesViewModel.LoadData();
        }
        
       
    }
}
