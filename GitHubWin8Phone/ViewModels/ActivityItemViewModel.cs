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
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GitHubWin8Phone.Resources;
using System.Threading.Tasks;

namespace GitHubWin8Phone.ViewModels
{
    /// <summary>
    /// View-Model class, the purpose of this class is to link an Activity object to a graphical context (GUI)
    /// </summary>
    public class ActivityItemViewModel : INotifyPropertyChanged
    {
        public ActivityItemViewModel(Activity activity)
        {

            this.Activity = activity;
            this.LineOne = Regex.Replace(activity.Type.Replace("Event", ""), "(\\B[A-Z])", " $1") + " at " + activity.Repo.Name;
            this.LineTwo = activity.Actor.Login;
            this.LineThree = activity.CreatedAt.DateTime.ToLongDateString() +" at "+ activity.CreatedAt.DateTime.ToLongTimeString();

        }

        private Activity activity;

        /// <summary>
        /// Activity object attached to this item
        /// </summary>
        public Activity Activity
        {
            get
            {
                return activity;
            }
            set
            {
                activity = value;
                NotifyPropertyChanged("Activity");
            }
        }

        /// <summary>
        /// Gets the repository related to the activity of this item
        /// </summary>
        /// <returns>Repository</returns>
        public async Task<Repository> getRelatedRepository()
        {
                String url = activity.Repo.Url;
                String[] param = url.Split('/');
                Repository repository = await App.GitHubClient.Repository.Get(param[param.Length - 2], param[param.Length - 1]);
                return repository;
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
        
        private string lineThree;
        /// <summary>
        /// String representation of the activity (Line3)
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
