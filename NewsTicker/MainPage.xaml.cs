using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using ConsoleSpikes;

namespace NewsTicker
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (MainListBox.SelectedIndex == -1)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + MainListBox.SelectedIndex, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            MainListBox.SelectedIndex = -1;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
                    
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searchKeywords = SearchTextBox.Text.Trim();

            App.ctx.BeginLogIn(App.USERNAME, App.PASSWORD, result =>
            {
                App.ctx.EndLogIn(result);

                App.ViewModel.LoadData(searchKeywords);

                //if (App.ViewModel.Items.Count() <= 0)
                //{
                //    try
                //    {
                //        Deployment.Current.Dispatcher.BeginInvoke(() =>
                //        {
                //            MessageBox.Show("No news results found.", "Results", MessageBoxButton.OKCancel);
                //        });
                        
                //    }
                //    catch (Exception ex)
                //    {
                //        throw ex;
                //    }
                //}
            }, null);
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SubscribeNewsPage.xaml" , UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {         
            //Destroy your session
            App.ctx.BeginLogOut(result =>
            {
            }, null);

            base.OnBackKeyPress(e);
        }
    }
}