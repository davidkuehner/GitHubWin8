using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Octokit;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace GitHubWin8Phone
{
    public partial class CreateIssuePage : PhoneApplicationPage, INotifyPropertyChanged
    {
        public CreateIssuePage()
        {
            InitializeComponent();
            this.Issue = new Issue();
            LoadCollaborators();
            LoadMilestones();
            DataContext = this;
        }

        private async void LoadCollaborators()
        {
            this.Collaborators = await App.IssuesViewModel.GetCollaboratorsAsString();
        }

        private async void LoadMilestones()
        {
            this.Milestones = await App.IssuesViewModel.GetMilestones();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Repository = PhoneApplicationService.Current.State["repository"] as Repository;
        }

        private void BtnCancelNewIssueAppBar_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void textBoxIssueTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            // Update the binding source
            BindingExpression bindingExpr = textBox.GetBindingExpression(TextBox.TextProperty);
            bindingExpr.UpdateSource();
        }

        private async void BtnSaveNewIssueAppBar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Issue.Title) || string.IsNullOrWhiteSpace(Issue.Title))
            {
                MessageBox.Show("Title cannot be empty!");
            }
            else
            {
                NewIssue newIssue = new NewIssue(Issue.Title);

                //Set assignee
                newIssue.Assignee = ListPickerCollaborator.SelectedItem as string;

                //Set milestone
                newIssue.Milestone = null;
                Milestone milestone = ListPickerMilestone.SelectedItem as Milestone;                
                if(milestone != null)
                {
                    newIssue.Milestone = milestone.Number;
                }

                //Try to create the issue
                bool ok = await App.IssuesViewModel.CreateIssue(newIssue);

                if (!ok)
                {
                    MessageBox.Show("Error while creating issue");
                }
                else
                {
                    MessageBox.Show("Issue created successfuly");
                    NavigationService.GoBack();
                }
            }
        }

        private void textBoxIssueTitle_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Focus();
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

        private List<string> collaborators;
        public List<string> Collaborators
        {
            get { return collaborators; }
            set
            {
                collaborators = value;
                NotifyPropertyChanged("Collaborators");
            }
        }

        private IList<Milestone> milestones;
        public IList<Milestone> Milestones
        {
            get { return milestones; }
            set
            {
                milestones = value;
                NotifyPropertyChanged("Milestones");
            }
        }

        private Issue issue;
        public Issue Issue
        {
            get { return issue; }
            set
            {
                issue = value;
                NotifyPropertyChanged("Issue");
            }
        }

        private Repository repository;
        public Repository Repository
        {
            get { return repository; }
            set
            {
                repository = value;
                NotifyPropertyChanged("Repository");
            }
        }
    }
}