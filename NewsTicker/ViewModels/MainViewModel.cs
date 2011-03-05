using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using CityIndex.JsonClient;


namespace NewsTicker
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Retrieves top 25 news and adds them to news items collection.
        /// </summary>
        public void LoadData(string keywords)
        {
            App.ctx.BeginListNewsHeadlines(keywords, 25, result =>
                {
                    var response = App.ctx.EndListNewsHeadlines(result);
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        Items.Clear();
                        foreach (var item in response.Headlines)
                        {
                            Items.Add(new ItemViewModel() { Headline = item.Headline, PublishDate = item.PublishDate.ToString(), StoryId = item.StoryId });
                        }
                    });

                    IsDataLoaded = true;
                }, null);
        }

        /// <summary>
        /// Adds given item to news item collection
        /// </summary>
        /// <param name="item"></param>
        public void LoadData(ItemViewModel item)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                Items.Add(item);
            });
            
            IsDataLoaded = true;
        }

        /// <summary>
        /// Removes all items from news item collection
        /// </summary>
        public void Clear()
        {
            Items.Clear();
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