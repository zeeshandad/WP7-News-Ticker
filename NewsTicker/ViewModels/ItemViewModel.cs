using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace NewsTicker
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private int _storyId;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public int StoryId
        {
            get
            {
                return _storyId;
            }
            set
            {
                if (_storyId == value)
                    return;
                _storyId = value;
                NotifyPropertyChanged("StoryId");
            }
        }

        private string _headline;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Headline
        {
            get
            {
                return _headline;
            }
            set
            {
                if (value != _headline)
                {
                    _headline = value;
                    NotifyPropertyChanged("Headline");
                }
            }
        }

        private string _publishDate;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string PublishDate
        {
            get
            {
                return _publishDate;
            }
            set
            {
                if (value != _publishDate)
                {
                    _publishDate = value;
                    NotifyPropertyChanged("PublishDate");
                }
            }
        }
                
        private string _story;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Story
        {
            get
            {
                return _story;
            }
            set
            {
                if (value != _story)
                {
                    _story = value;
                    NotifyPropertyChanged("Story");
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