using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace GitHubWin8Phone
{
    public partial class TabCommitsList : UserControl
    {
        public TabCommitsList()
        {
            InitializeComponent();

            DataContext = App.CommitsViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.CommitsViewModel != null)
            {
                DataContext = App.CommitsViewModel;
            }
        }
    }
}
