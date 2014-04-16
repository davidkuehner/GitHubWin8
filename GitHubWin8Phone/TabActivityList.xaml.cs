﻿using System;
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
    }
}