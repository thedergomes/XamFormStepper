using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CustomTab.Models.Market;
using FFImageLoading;
using Java.Lang;
using Custom = CustomTab.CustomObject.ReciclerView;

namespace CustomTab.Droid.Renderer.ReciclerView
{
    public class ProductAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;

        private static int TYPE_HEADER = -1;

        private static int TYPE_SMALL = 0;
        private static int TYPE_TALL = 1;


        private Custom.RecyclerView _grid;
        private Context _contex;

        public ProductAdapter (Custom.RecyclerView grid, Context contex)
        {
            _contex = contex;
            _grid = grid;
        }

        //public override void OnViewRecycled(Java.Lang.Object holder)
        //{
        //    if (holder != null)
        //    {
        //        MarketViewHolder vh = holder as MarketViewHolder;
        //        vh.Image.SetImageDrawable(null);
        //    }

        //    base.OnViewRecycled(holder);
        //}


        public override int GetItemViewType(int position)
        {
            var Product = _grid.ItemsSource[position];
            if (Product.Index == -1)
            {
                return TYPE_HEADER;
            }

            return Product.LayoutOption == 1 ? TYPE_TALL : TYPE_SMALL;
        }

        public override RecyclerView.ViewHolder
            OnCreateViewHolder (ViewGroup parent, int viewType)
        {
            View itemView;

            if (viewType == TYPE_HEADER)
            {
                itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.CategoriesLayout, parent, false);
            }
            else if (viewType == TYPE_SMALL)
            { 
                itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.ProductLayout, parent, false);
            }
            else
            { 
                itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.ProductLayout2, parent, false);
            }

            return new MarketViewHolder(itemView, OnClick);
        }

        private int DpToPixels(float valueInDp)
        {
            Android.Util.DisplayMetrics metrics = _contex.Resources.DisplayMetrics;
            var pixels = Android.Util.TypedValue.ApplyDimension(Android.Util.ComplexUnitType.Dip, valueInDp, metrics);
            return Convert.ToInt32(System.Math.Round(pixels));
        }

        public override void
            OnBindViewHolder (RecyclerView.ViewHolder holder, int position)
        {
            var Product = _grid.ItemsSource[position];

            if (Product.Index != -1)
            {
                MarketViewHolder vh = holder as MarketViewHolder;
                var height = Product.LayoutOption == 1 ? DpToPixels(150) : DpToPixels(90);

                if (!string.IsNullOrWhiteSpace(Product.MainImage))
                {
                    ImageService.Instance.LoadUrl(Product.MainImage, TimeSpan.FromMinutes(1))
                    .LoadingPlaceholder("placeholderimg", FFImageLoading.Work.ImageSource.CompiledResource)
                    //.LoadingPlaceholder(Product.Thumbnail, FFImageLoading.Work.ImageSource.Url)
                    .Retry(3, 200)
                    .DownSample(height: height)
                    .Into(vh.Image);
                }
                else 
                {
                    ImageService.Instance.LoadCompiledResource("placeholderimg")
                     .LoadingPlaceholder("placeholderimg", FFImageLoading.Work.ImageSource.CompiledResource)
                     .DownSample(height: height)
                     .Into(vh.Image);
                }


                vh.Title.Text = Product.Title;
                vh.Seller.Text = Product.Seller;
                vh.Price.Text = Product.Price;
            }
            else 
            {
                StaggeredGridLayoutManager.LayoutParams layoutParams = (StaggeredGridLayoutManager.LayoutParams)holder.ItemView.LayoutParameters;
                layoutParams.FullSpan = true;
            }

        }

        public override int ItemCount
        {
            get { return _grid.ItemsSource.Count; }
        }

        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }
}
