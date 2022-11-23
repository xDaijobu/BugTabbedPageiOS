using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ab.Services;
using ab.Views;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace ab
{
    public partial class App : Application
    {

        public App ()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPageCS();
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }

    public class FlyoutPageItem
    {
        public string Title { get; set; }
        //public string IconSource { get; set; }
        //public Type TargetType { get; set; }

        public FlyoutPageItem(string title)
        {
            Title = title;
        }
    }

    public class MainPageCS : FlyoutPage
    {
        MainPage flyoutPage;

        public MainPageCS()
        {
            flyoutPage = new MainPage();
            Flyout = flyoutPage;
            (flyoutPage as MainPage).ListView.ItemSelected += OnItemSelected;
            Detail = new NavigationPage(new TestPage());
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutPageItem;
            if (item != null)
            {
                Detail = new NavigationPage(new TestPage());
                (flyoutPage as MainPage).ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }

    public class MainPage : ContentPage
    {
        ListView listView;
        public ListView ListView { get { return listView; } }

        public MainPage()
        {
            var flyoutPageItems = new List<FlyoutPageItem>();
            flyoutPageItems.Add(new FlyoutPageItem("Hi"));
            flyoutPageItems.Add(new FlyoutPageItem("Yeah"));
            flyoutPageItems.Add(new FlyoutPageItem("Noooo"));

            listView = new ListView
            {
                ItemsSource = flyoutPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    label.SetBinding(Label.TextProperty, "Title");

                    grid.Children.Add(label, 1, 0);

                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.None
            };

            Title = "Punch Me";
            Padding = new Thickness(0, 40, 0, 0);
            Content = new StackLayout
            {
                Children = { listView }
            };
        }
    }

    public class TestPage : TabbedPage
    {
        public TestPage()
        {
            Random rnd = new Random();
            Children.Add(new ContentPage()
            {
                BackgroundColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)),
                Title = "1",
            });
            Children.Add(new ContentPage()
            {
                BackgroundColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)),
                Title ="2",
            });
            Children.Add(new ContentPage()
            {
                BackgroundColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)),
                Title = "3",
            });

            BarBackgroundColor = Color.Blue;
        }
    }
}

