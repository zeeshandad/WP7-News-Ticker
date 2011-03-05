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
using CIAPI;
using CIAPI.DTO;
using CIAPI.Streaming;
using StreamingClient;

namespace NewsTicker
{
    public partial class SubscribeNewsPage : PhoneApplicationPage
    {
        IStreamingListener<NewsDTO> newsListener;
        IStreamingClient streamingClient;

        public SubscribeNewsPage()
        {
            InitializeComponent();

            App.ViewModel.Clear();
            DataContext = App.ViewModel;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SubscribeToNewsHeadlineStream();
            SuccessfulMessage.Visibility = System.Windows.Visibility.Visible;
        }

        public void SubscribeToNewsHeadlineStream()
        {            
            string searchKeywords = SearchTextBox.Text;

            App.ctx.BeginLogIn(App.USERNAME, App.PASSWORD, a =>
            {
                App.ctx.EndLogIn(a);
                
                //Next we create a connection to the streaming api, using the authenticated session                
                streamingClient = App.StreamingClient;
                streamingClient.Connect();

                //And instantiate a listener for news headlines on the appropriate topic
                //You can have multiple listeners on one connection
                newsListener = streamingClient.BuildListener<NewsDTO>(searchKeywords);
                newsListener.Start();

                //The MessageRecieved event will be triggered every time a new News headline is available,
                //so attach a handler for that event, and wait until something comes through                
                NewsDTO recievedNewsHeadline = null;
                newsListener.MessageRecieved += (s, e) =>
                {
                    recievedNewsHeadline = e.Data;
                    //Add this new news headline to the main items collection.
                    ItemViewModel item = new ItemViewModel();
                    item.Headline = recievedNewsHeadline.Headline;
                    item.PublishDate = recievedNewsHeadline.PublishDate.ToString();
                    recievedNewsHeadline.StoryId = recievedNewsHeadline.StoryId;

                    App.ViewModel.LoadData(item);                    
                };
              
            }, null);


        }
                
        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
            //Clean up
            //Stop any listeners
            newsListener.Stop();
            //Shut down the connection
            streamingClient.Disconnect();
            //Destroy your session
            App.ctx.BeginLogOut(result =>
            {               
            }, null);
        }              
    }
}