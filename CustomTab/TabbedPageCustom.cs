using System;
using Xamarin.Forms;

namespace CustomTab
{
    public class TabbedPageCustom : TabbedPage
    {
        public TabbedPageCustom()
        {

            for (int i = 0; i < 3; i++)
            {
                var grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                grid.Children.Add(new Image
                {
                    Aspect = Aspect.Fill,
                    Source = ImageSource.FromResource("CustomTab.fondoDegradado.png"),
                });

                grid.Children.Add(new StackLayout 
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Children = 
                    {
                        new Label
                        {
                            FontSize = 25,
                            Text = "page number "+i
                        }
                    }
                }, 0, 0);

                Children.Add(new ContentPage
                {
                    Title = "Page "+i,
                    Content = grid
                });
            }
        }
    }
}
