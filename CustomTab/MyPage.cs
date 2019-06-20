using System;
using Sharpnado.Presentation.Forms.CustomViews.Tabs;
using Xamarin.Forms;

namespace CustomTab
{
    public class MyPage : ContentPage
    {
        public MyPage()
        {

            var grid = new Grid();
            //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(200, GridUnitType.Absolute) });
            //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40, GridUnitType.Absolute) });
            //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30, GridUnitType.Absolute) });
            //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30, GridUnitType.Absolute) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(200) });
            //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });


            var a = new UnderlinedTabItem
            {
                BackgroundColor = Color.Blue,
                Label = "holis"
            };

            var b = new UnderlinedTabItem
            {
                BackgroundColor = Color.Yellow,
                Label = "holis 2"
            };

            var Switcher = new ViewSwitcher
            {
                TabIndex = 0,
                Animate = true,
                SelectedIndex = 0,
            };
            Switcher.Children.Add(new Label 
            {
                Text = "Page 1",
                BackgroundColor = Color.Red
            });

            //Switcher.Children.Add(new Label
            //{
            //    Text = "Page 2",
            //    BackgroundColor = Color.Blue
            //});

            Switcher.Children.Add(new StackLayout 
            {
                Children = 
                {
                    new Entry
                    {
                    },
                    new Label
                    {
                        Text = "Page 2",
                        BackgroundColor = Color.Blue
                    }
                } 
            });


            var TabHost = new TabHostView
            {
                ShadowType = Sharpnado.Presentation.Forms.RenderedViews.ShadowType.Top,
                //SelectedIndex = 1
            };
            TabHost.Children.Add(a);
            TabHost.Children.Add(b);

            //TabHost.Children.Add(new Label 
            //{
            //    Text = "page 1" 
            //});

            //TabHost.Children.Add(new Label
            //{
            //    Text = "page 2"
            //});

            var binding = new Binding
            {
                Source = Switcher,
                Path = nameof(ViewSwitcher.SelectedIndex),
                Mode = BindingMode.TwoWay,
            };
            TabHost.SetBinding(TabHostView.SelectedIndexProperty, binding);
            grid.Children.Add(TabHost, 0, 0);
            grid.Children.Add(new ScrollView 
            {
                Content = Switcher
            }, 0, 1);
            //TabHost.SetBinding();



            //            < tabs:TabItem x:Class = "SillyCompany.Mobile.Practices.Presentation.CustomViews.SpamTab"
            //              xmlns = "http://xamarin.com/schemas/2014/forms"
            //              xmlns: x = "http://schemas.microsoft.com/winfx/2009/xaml"
            //              xmlns: tabs = "clr-namespace:Sharpnado.Presentation.Forms.CustomViews.Tabs;assembly=Sharpnado.Presentation.Forms"
            //              x: Name = "RootLayout" >

            //     < ContentView.Content >

            //         < Grid ColumnSpacing = "0" RowSpacing = "0" >

            //                < Image x: Name = "Spam"
            //                   VerticalOptions = "End"
            //                   Aspect = "Fill"
            //                   Source = "{Binding Source={x:Reference RootLayout}, Path=SpamImage}" />
            //            < Image x: Name = "Foot"
            //                   Aspect = "Fill"
            //                   Source = "monty_python_foot" />
            //        </ Grid >
            //    </ ContentView.Content >
            //</ tabs:TabItem >

            // ...

            //< tabs:TabHostView x:Name = "TabHost"
            //              Grid.Row = "2"
            //              BackgroundColor = "White"
            //              SelectedIndex = "{Binding Source={x:Reference Switcher}, Path=SelectedIndex, Mode=TwoWay}"
            //              ShadowType = "Top" >
            //< tb:SpamTab SpamImage = "spam_classic_home" />

            //< tb:SpamTab SpamImage = "spam_classic_list" />

            //< tb:SpamTab SpamImage = "spam_classic_grid" />

            Content = new StackLayout
            {
                Children = 
                {
                    grid,
                    //TabHost,
                    //Switcher,
                    //new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

