using System;
using System.Collections;
using System.Collections.ObjectModel;
using CustomTab.Models.Market;
using Xamarin.Forms;

namespace CustomTab.CustomObject.ReciclerView
{
    public class ItemTappedEventArgs : EventArgs
    {
        public ItemTappedEventArgs(object item, int itemIndex) 
        {
            Item = item;
            ItemIndex = itemIndex;
        }

        public object Item
        {
            get;
            private set;
        }

        public int ItemIndex
        {
            get;
            private set;
        }
    }

    public class RecyclerView : View
    {
        public static readonly BindableProperty LastScrollDirectionProperty =
            BindableProperty.Create(nameof(LastScrollDirection), typeof(string), typeof(RecyclerView), null);
        public static readonly BindableProperty AtStartOfListProperty =
            BindableProperty.Create(nameof(AtStartOfList), typeof(bool), typeof(RecyclerView), true);
        public static readonly BindableProperty AtEndOfListProperty =
            BindableProperty.Create(nameof(AtEndOfList), typeof(bool), typeof(RecyclerView), false);

        public string LastScrollDirection
        {
            get { return (string)GetValue(LastScrollDirectionProperty); }
            set
            {
                SetValue(LastScrollDirectionProperty, value);
                OnPropertyChanged(nameof(ShouldShowSearchBar));
                VoidShouldShowSearchBar();
            }
        }
        public bool AtStartOfList
        {
            get { return (bool)GetValue(AtStartOfListProperty); }
            set
            {
                SetValue(AtStartOfListProperty, value);
                OnPropertyChanged(nameof(ShouldShowSearchBar));
                VoidShouldShowSearchBar();
            }
        }
        public bool AtEndOfList
        {
            get { return (bool)GetValue(AtEndOfListProperty); }
            set
            {
                SetValue(AtEndOfListProperty, value);
            }
        }

        public const string NoPreviousScroll = "None";
        public const string ScrollToStart = "Towards start";
        public const string ScrollToEnd = "Towards end";

        public void VoidShouldShowSearchBar()
        {
            var newValue = AtStartOfList || LastScrollDirection == ScrollToStart;
            var shouldChange = (!_lastShowValue.HasValue) || (_lastShowValue.Value != newValue && _nextShowChange < DateTime.Now);

            if (shouldChange)
            {
                _lastShowValue = newValue;
                _nextShowChange = DateTime.Now.AddMilliseconds(250);
            }

            var IsVisibleTopBar = shouldChange ? newValue : _lastShowValue.Value;

            ScrollChange?.Invoke(this, !IsVisibleTopBar);
        }

        public event EventHandler<bool> ScrollChange;
        private bool? _lastShowValue;
        private DateTime _nextShowChange;

        public bool ShouldShowSearchBar
        {
            get
            {
                var newValue = AtStartOfList || LastScrollDirection == ScrollToStart;
                var shouldChange = (!_lastShowValue.HasValue) || (_lastShowValue.Value != newValue && _nextShowChange < DateTime.Now);

                if (shouldChange)
                {
                    _lastShowValue = newValue;
                    _nextShowChange = DateTime.Now.AddMilliseconds(250);
                }

                return shouldChange ? newValue : _lastShowValue.Value;
            }
        }






        public event EventHandler IsEndEvent;

        public event EventHandler<ItemTappedEventArgs> ItemTapped;

        public void InvokeIsEndEvent()
        {
            IsEndEvent?.Invoke(this, EventArgs.Empty);
        }

        public void InvokeItemTapped(int position)
        {
            var arg = new ItemTappedEventArgs(ItemsSource[position], position);
            ItemTapped?.Invoke(this, arg);
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(ObservableCollection<ProductModel>), typeof(RecyclerView), null);

        public ObservableCollection<ProductModel> ItemsSource
        {
            get { return (ObservableCollection<ProductModel>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
    }
}
