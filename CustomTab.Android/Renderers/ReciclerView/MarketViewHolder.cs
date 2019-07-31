using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace CustomTab.Droid.Renderer.ReciclerView
{
    public class MarketViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }

        public TextView Seller { get; private set; }
        public TextView Title { get; private set; }
        public TextView Price { get; private set; }

        public MarketViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);

            Seller = itemView.FindViewById<TextView>(Resource.Id.seller);
            Title = itemView.FindViewById<TextView>(Resource.Id.title);
            Price = itemView.FindViewById<TextView>(Resource.Id.price);

            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }
}
