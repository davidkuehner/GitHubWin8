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
    public partial class TabBranchesList : UserControl
    {
        public TabBranchesList()
        {
            InitializeComponent();
            DataContext = App.BranchesViewModel;
        }        

        private void llsBranches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(App.BranchesViewModel != null)
            {
                DataContext = App.BranchesViewModel;
                App.BranchesViewModel.LoadData();                                
            }
        }

    }
}
