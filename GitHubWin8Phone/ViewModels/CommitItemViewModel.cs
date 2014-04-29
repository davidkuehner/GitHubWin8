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
    /// <summary>
    /// View-Model class, the purpose of this class is to link a Commit object to a graphical context (GUI)
    /// </summary>
    public class CommitItemViewModel : INotifyPropertyChanged
    {
        public CommitItemViewModel(Commit commit)
        {
            this.Commit = commit;
            this.LineOne = commit.Message;
            this.LineTwo = commit.Author.Name;
            this.LineThree = commit.Committer.Date.ToLongDateString() + " at " + commit.Committer.Date.ToLongTimeString();
        }

        private Commit commit;
        /// <summary>
        /// Commit object attached to this item
        /// </summary>
        public Commit Commit
        {
            get
            {
                return commit;
            }
            set
            {
                commit = value;
                NotifyPropertyChanged("Commit");
            }
        }
 
        private string lineOne;
        /// <summary>
        /// String representation of the branch (Line1)
        /// </summary>
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
        /// String representation of the branch (Line2)
        /// </summary>
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
              
        private string lineThree;
        /// <summary>
        /// String representation of the branch (Line3)
        /// </summary>
        public string LineThree
        {
            get
            {
                return lineThree;
            }
            set
            {
                if (value != lineThree)
                {
                    lineThree = value;
                    NotifyPropertyChanged("LineThree");
                }
            }
        }

        /// <summary>
        /// Property changed event fired when a property of this class changes. Useful for WPF Data Binding
        /// </summary>
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