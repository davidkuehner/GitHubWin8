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


        public async Task<Repository> getRelatedRepository()
        {
                String url = activity.Repo.Url;
                String[] param = url.Split('/');
                Repository repository = await App.GitHubClient.Repository.Get(param[param.Length - 2], param[param.Length - 1]);
                return repository;
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
        
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        private string lineThree;
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
