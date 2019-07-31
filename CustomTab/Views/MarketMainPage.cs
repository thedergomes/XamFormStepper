using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CustomTab.CustomObject;
//using CustomTab.Helpers.UIComponents;
using CustomTab.Models.Market;
//using CustomTab.ViewModels.Market;
using FFImageLoading.Forms;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;

namespace CustomTab.Views.Market.Version2
{
    //Source = EmbeddedResourceImageSource.FromUri(new Uri("http://i.dailymail.co.uk/i/pix/2015/09/01/18/2BE1E88B00000578-3218613-image-m-5_1441127035222.jpg")),
    class CategoryTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NormalTemplate { get; set; }
        public DataTemplate LastTemplate { get; set; }

        public CategoryTemplateSelector()
        {
            NormalTemplate = new DataTemplate(() => 
            {
                return GetLayout(new Thickness(0, 0, 20, 0));
            });

            LastTemplate = new DataTemplate(() =>
            {
                return GetLayout();
            });
        }

        View GetLayout(Thickness thickness = new Thickness()) 
        {
            var CategoryContainer = new Grid();
            
            CategoryContainer.Children.Add(new BoxView 
            {
                BackgroundColor = Color.White 
            });

            CategoryContainer.Children.Add(new SvgCachedImage
            {
                Margin = thickness,
                WidthRequest = 40,
                ReplaceStringMap = new Dictionary<string, string>
                {
                    { "fill=\"currentColor\"" , "fill=\"#f7f8f9\""}
                },
                FadeAnimationEnabled = false,
                Source = "circulo.svg"
            }, 0, 0);

            var CategoryIcon = new SvgCachedImage
            {
                Margin = thickness,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 20,
                FadeAnimationEnabled = false,
            };
            CategoryIcon.SetBinding(SvgCachedImage.SourceProperty, nameof(CategoryModel.Icon));


            //var CategoryIcon = new CachedImage
            //{
            //    HorizontalOptions = LayoutOptions.Center,
            //    WidthRequest = 20,
            //    FadeAnimationEnabled = false,
            //};
            //CategoryIcon.SetBinding(CachedImage.SourceProperty, nameof(CategoryModel.Icon));


            CategoryContainer.Children.Add(CategoryIcon, 0, 0);

            return CategoryContainer;
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var Category = (CategoryModel)item;

            return (Category.IsLast && Category != null) ? LastTemplate : NormalTemplate;
        }
    }

    public class MarketMainPage : ContentPage
    {
        private int ValToSum = 0;
        ObservableCollection<ProductModel> Source;

        CollectionView Categories;

        public MarketMainPage()
        {
            //#region NAVIGATION PAGE


            //NavigationPage.SetTitleView(this, new RoundedEntry
            //{
            //    FontSize = 13.5,
            //    Placeholder = "Buscar en Chappsy Market",
            //    HeightRequest = 30,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //});

            //#endregion

            #region CATEGORIES CAROUSEL

            Categories = new CollectionView
            {
                BackgroundColor = Color.White,
                SelectionMode = SelectionMode.Single,
                Margin = new Thickness(10, 0),
                HeightRequest = 53,
                ItemsLayout = ListItemsLayout.HorizontalList,
                ItemTemplate = new CategoryTemplateSelector(),
                ItemsSource = new List<CategoryModel> 
                {
                    new CategoryModel
                    {
                        Name = "cube 1",
                        Icon = "cube.svg"
                    },
                    new CategoryModel
                    {
                        Name = "cube 1",
                        Icon = "cube.svg"
                    }
                }
            };
            #endregion

            #region PRODUCT TIMELINE
            Source = new ObservableCollection<ProductModel> 
            {
                // Header
                new ProductModel
                {
                    Index = -1
                } 
            };

            List<ProductModel> list = new List<ProductModel>();

            for (int i = 0; i < 25; i++)
            {
                list.Add(new ProductModel 
                {
                    Index = i,
                    Description = "Lorem ipsum dolor sit amet consectetur adipiscing elit phasellus mollis hac, accumsan interdum conubia suscipit per sollicitudin odio tristique id.",
                    Title = "Lorem ipsum.",
                    Price = "$10",
                    Pictures = new List<string> 
                    {
                        "http://i.dailymail.co.uk/i/pix/2015/09/01/18/2BE1E88B00000578-3218613-image-m-5_1441127035222.jpg"
                    }
                });
            }
            PopulateProductsLists(list);

            var collection = new CustomObject.ReciclerView.RecyclerView();

            collection.ScrollChange+= Collection_ScrollChange;
            collection.ItemTapped+= Collection_ItemTapped;;
            collection.ItemsSource = Source;

            #endregion


            //var ConectivityMessage = CreateConectivityMessage();
            //ConectivityMessage.SetBinding(View.IsVisibleProperty, nameof(MarketMainPageViewModel.IsConectivityDisable));

            var MainContainer = new Grid
            {
                Padding = new Thickness(5, 0),
                RowDefinitions = new RowDefinitionCollection 
                {
                    new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    }
                }
            };

            MainContainer.Children.Add(collection, 0, 0);

            CompressedLayout.SetIsHeadless(MainContainer, true);


            #region MainContainer

            var Absolutelayout = new AbsoluteLayout();

            AbsoluteLayout.SetLayoutBounds(Categories, new Rectangle(0, 0, 1, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(Categories, AbsoluteLayoutFlags.WidthProportional);

            //AbsoluteLayout.SetLayoutBounds(ConectivityMessage, new Rectangle(.5, .6, 64 * 2, 64 * 2));
            //AbsoluteLayout.SetLayoutFlags(ConectivityMessage, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(MainContainer, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(MainContainer, AbsoluteLayoutFlags.SizeProportional);

            Absolutelayout.Children.Add(MainContainer);
            //Absolutelayout.Children.Add(ConectivityMessage);
            Absolutelayout.Children.Add(Categories);

            #endregion

            Content = Absolutelayout;
        }

        void Collection_ItemTapped(object sender, CustomObject.ReciclerView.ItemTappedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[ITEM TAPPED] "+e.Item);
        }


        private void PopulateProductsLists(List<ProductModel> productsList)
        {
            ValToSum = 0;
            var LayoutOption = 0;

            for (int i = 0; i < productsList.Count; i++)
            {
                if (ValToSum == 0)
                {
                    ValToSum++;
                    LayoutOption = 0;
                }
                else if (ValToSum == 1 || ValToSum == 2)
                {
                    ValToSum++;
                    LayoutOption = 1;
                }
                else if (ValToSum == 3)
                {
                    ValToSum = 0;
                    LayoutOption = 0;
                }


                Source.Add(new ProductModel
                {
                    Index = productsList[i].Index,
                    LayoutOption = LayoutOption,
                    Pictures = productsList[i].Pictures,
                    Price = productsList[i].Price,
                    Title = productsList[i].Title,
                    Description = productsList[i].Description,
                });
            }
        }

        //StackLayout CreateConectivityMessage()
        //{
        //    var NoWifiSvg = new SvgCachedImage
        //    {
        //        HeightRequest = 64,
        //        WidthRequest = 64,
        //        Source = "resource://Chappsy2.Img.Market.no-wifi.svg",
        //    };

        //    var layout = new StackLayout
        //    {
        //        BackgroundColor = Color.Red,
        //        Children =
        //        {
        //            NoWifiSvg,
        //            new Label
        //            {
        //                HorizontalTextAlignment = TextAlignment.Center,
        //                Text = "Revise su conexion a internet"
        //            }
        //        }
        //    };

        //    CompressedLayout.SetIsHeadless(layout, true);

        //    return layout;
        //}

        async void Collection_ScrollChange(object sender, bool e)
        {
            var ShouldShowSearchBar = e;

            //Task<bool> SellButtonAnimation;
            Task<bool> CategoriesAnimation;

            if (ShouldShowSearchBar)
            {
                //SellButtonAnimation = SellButton.TranslateTo(0, 0, 100);
                CategoriesAnimation = Categories.TranslateTo(0, 0, 200);
            }
            else
            {
                //SellButtonAnimation = SellButton.TranslateTo(0, 140, 100);
                CategoriesAnimation = Categories.TranslateTo(0, -140, 200);
            }

            //await Task.WhenAll(SellButtonAnimation, CategoriesAnimation);
            await Task.WhenAll(CategoriesAnimation);
        }
    }
}

