using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace WhiteMvvm.CustomControls
{
    public class GridView : ScrollView
    {
        private ICommand _innerSelectedCommand;
        private FlexLayout _flxMedia;

        public Thickness ItemMargin { get; set; }

        public event EventHandler SelectedItemChanged;

        public static readonly BindableProperty SelectedCommandProperty =
            BindableProperty.Create("SelectedCommand", typeof(ICommand), typeof(GridView), null);

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(GridView), default(IEnumerable<object>), BindingMode.TwoWay, propertyChanged: ItemsSourceChanged);

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem", typeof(object), typeof(GridView), null, BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(GridView), default(DataTemplate), BindingMode.TwoWay, propertyChanged: OnItemTemplateChanged);


        public ICommand SelectedCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        private static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (GridView)bindable;
            if (itemsLayout.ItemTemplate != null && itemsLayout.ItemsSource != null)
            {
                itemsLayout.SetItems();
            }
        }
        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (GridView)bindable;
            if(itemsLayout.ItemTemplate != null && itemsLayout.ItemsSource != null)
            {
                itemsLayout.SetItems();
            }            
        }

        public GridView()
        {
            _flxMedia = new FlexLayout()
            {
                JustifyContent = FlexJustify.Start,
                AlignContent = FlexAlignContent.Start,
                Wrap = FlexWrap.Wrap,
            };
            this.Content = _flxMedia;
        }

        protected virtual void SetItems()
        {
            _flxMedia.Children.Clear();

            _innerSelectedCommand = new Command<View>(view =>
            {
                SelectedItem = view.BindingContext;
                SelectedItem = null; // Allowing item second time selection
            });

            if (ItemsSource == null)
            {
                return;
            }

            foreach (var item in ItemsSource)
            {
                var view = GetItemView(item);
                _flxMedia.Children.Add(view);
            }

            _flxMedia.BackgroundColor = BackgroundColor;
            SelectedItem = null;
        }

        protected virtual View GetItemView(object item)
        {
            var content = ItemTemplate?.CreateContent();

            if (!(content is View view))
            {
                return null;
            }
            view.Margin = ItemMargin;
            view.BindingContext = item;

            var gesture = new TapGestureRecognizer
            {
                Command = _innerSelectedCommand,
                CommandParameter = view
            };

            AddGesture(view, gesture);

            return view;
        }

        private void AddGesture(View view, TapGestureRecognizer gesture)
        {
            view.GestureRecognizers.Add(gesture);

            if (!(view is Layout<View> layout))
            {
                return;
            }

            foreach (var child in layout.Children)
            {
                AddGesture(child, gesture);
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsView = (GridView)bindable;
            if (newValue == oldValue && newValue != null)
            {
                return;
            }

            itemsView.SelectedItemChanged?.Invoke(itemsView, new GridViewEventArgs(newValue));

            if (itemsView.SelectedCommand?.CanExecute(newValue) ?? false)
            {
                itemsView.SelectedCommand?.Execute(newValue);
            }
        }
    }
    public class GridViewEventArgs : EventArgs
    {
        public GridViewEventArgs(object selectedItem)
        {
            SelectedItem = selectedItem;
        }

        public object SelectedItem { get; set; }
    }
}
