using Octokit;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GitHubWin8Phone.ViewModels
{
    public class BranchItemViewModel : INotifyPropertyChanged
    {
        public BranchItemViewModel(Branch branch)
        {
            this.Branch = branch;
            this.LineOne = branch.Name;
            this.LineTwo = branch.Commit.Sha;            
        }

        private Branch branch;
        public Branch Branch
        {
            get
            {
                return branch;
            }
            set
            {
                branch = value;
                NotifyPropertyChanged("Branch");
            }
        }

        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        private string lineOne;
        public string LineOne
        {
            get
            {
                return lineOne;
            }
            set
            {
                if (value != lineOne)
                {
                    lineOne = value;
                    NotifyPropertyChanged("LineOne");
                }
            }
        }

        private string lineTwo;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string LineTwo
        {
            get
            {
                return lineTwo;
            }
            set
            {
                if (value != lineTwo)
                {
                    lineTwo = value;
                    NotifyPropertyChanged("LineTwo");
                }
            }
        }               

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}