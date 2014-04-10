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
    public partial class TabNewsList : UserControl
    {
        public TabNewsList()
        {
            InitializeComponent();

            DataContext = App.NewsViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            App.NewsViewModel.LoadData();
        }
    }
}
