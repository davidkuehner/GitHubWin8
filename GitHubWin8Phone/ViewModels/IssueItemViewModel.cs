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
    /// View-Model class, the purpose of this class is to link an Issue object to a graphical context (GUI)
    /// </summary>
    public class IssueItemViewModel : INotifyPropertyChanged
    {
        public IssueItemViewModel(Issue issue)
        {
            this.Issue = issue;
            this.LineOne = "#" + issue.Number + " " + issue.Title;
            
            if(issue.Assignee == null )
            {
                this.lineTwo = "Every one";
            }
            else
            {
                this.lineTwo = issue.Assignee.Login;
            }

            this.lineTwo += " " + issue.CreatedAt.ToString();
        }

        private Issue issue;
        /// <summary>
        /// Issue object attached to this item
        /// </summary>
        public Issue Issue
        {
            get
            {
                return issue;
            }
            set
            {
                issue = value;
                NotifyPropertyChanged("Issue");
            }
        }
       
        private string lineOne;
        /// <summary>
        /// String representation of the activity (Line1)
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
        /// String representation of the activity (Line2)
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