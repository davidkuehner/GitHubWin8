using System;
using System.Security;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Data;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Octokit;

namespace GitHubWin8Phone
{
    public partial class LoginPage : PhoneApplicationPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Username = "";
            this.DataContext = this;
        }

        private void BtnLoginAppBar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                MessageBox.Show("Username or password cannot be empty!");
            }
            else if(Username.IndexOf('@') != -1)
            {
                MessageBox.Show("Username only, not e-mail.");
            }
            else
            {
                Credentials credentials = new Credentials(Username, passwordBox.Password);
                Octokit.Internal.InMemoryCredentialStore inMemoryCredentialStore = new Octokit.Internal.InMemoryCredentialStore(credentials);
                App.GitHubClient = new Octokit.GitHubClient(new Octokit.ProductHeaderValue("GitHubWin8"), inMemoryCredentialStore);

                MessageBox.Show("DEV: There is no crediential falidation for now !");
                // TODO
                bool isCredentialsVaild = true;

                if (isCredentialsVaild)
                {
                    this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Invalid password of username !");
                }
            }

            
        }

        public String Username {get; set;}

        public String Password { get; set; }
        
        private void textBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            BindingExpression bindingExpr = textBox.GetBindingExpression(TextBox.TextProperty);
            bindingExpr.UpdateSource();
        }

        private void passwordBox_PasswordChanged(object sender, TextChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            BindingExpression bindingExpr = passwordBox.GetBindingExpression(TextBox.TextProperty);
            bindingExpr.UpdateSource();
        }

    }
}