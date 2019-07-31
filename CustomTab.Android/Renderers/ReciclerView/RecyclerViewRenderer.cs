using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Android.Content;
using Android.Views;
using Android.Widget;
//using Chappsy2.CustomObject.ReciclerView;
using CustomTab.Droid.Renderer.ReciclerView;
using CustomTab.Models.Market;
using FFImageLoading;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Views.View;
using AWidget = Android.Support.V7.Widget;
using Shared = CustomTab.CustomObject.ReciclerView;

[assembly: ExportRenderer(typeof(Shared.RecyclerView), typeof(RecyclerViewRenderer))]
namespace CustomTab.Droid.Renderer.ReciclerView
{
    class CustomScrollListener : AWidget.RecyclerView.OnScrollListener
    {
        private int HIDE_THRESHOLD = 45;
        private int scrolledDistance = 0;
        private bool controlsVisible = true;

        Shared.RecyclerView _view;

        public CustomScrollListener(Shared.RecyclerView view)
        {
            _view = view;
        }

        public override void OnScrolled(AWidget.RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            if (scrolledDistance > HIDE_THRESHOLD && controlsVisible)
            {
                _view.LastScrollDirection = Shared.RecyclerView.ScrollToStart;

                controlsVisible = false;
                scrolledDistance = 0;
            }
            else if (scrolledDistance < -HIDE_THRESHOLD && !controlsVisible)
            {
                _view.LastScrollDirection = Shared.RecyclerView.ScrollToEnd;

                controlsVisible = true;
                scrolledDistance = 0;
            }

            if ((controlsVisible && dy > 0) || (!controlsVisible && dy < 0))
            {
                scrolledDistance += dy;
            }
        }

        public override void OnScrollStateChanged(AWidget.RecyclerView recyclerView, int newState)
        {
            base.OnScrollStateChanged(recyclerView, newState);

            switch (newState)
            {
                case AWidget.RecyclerView.ScrollStateDragging:
                    // all image loading requests will be silently canceled
                    ImageService.Instance.SetPauseWork(true);
                    break;

                case AWidget.RecyclerView.ScrollStateIdle:
                    // loading requests are allowed again
                    ImageService.Instance.SetPauseWork(false);
                    break;
            }
        }
    }

    public class RecyclerViewRenderer : ViewRenderer<Shared.RecyclerView, AWidget.RecyclerView>
    {
        AWidget.RecyclerView mRecyclerView;
        ProductAdapter mAdapter;
        CustomScrollListener listener;

        Shared.RecyclerView SharedRecicler;

        AWidget.StaggeredGridLayoutManager LayoutManager;

        public RecyclerViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Shared.RecyclerView> e)
        {
            var activity = this.Context.GetActivity();

            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources
                mAdapter.ItemClick -= OnItemClick;

                mRecyclerView.ScrollChange -= MRecyclerView_ScrollChange;
                if (Element == null) return;

                if (Element.ItemsSource != null && Element.ItemsSource is INotifyCollectionChanged)
                    ((INotifyCollectionChanged)Element.ItemsSource).CollectionChanged -= ItemsSource_CollectionChanged;

                mRecyclerView.RemoveOnScrollListener(listener);
            }

            if (e.NewElement != null)
            {
                // Configure the control and subscribe to event handlers
                if (Control == null)
                {
                    // Instantiate the native control and assign it to the Control property
                    mRecyclerView = new AWidget.RecyclerView(Context);
                    mRecyclerView.OverScrollMode = OverScrollMode.Never;
                         //setOverScrollMode(ScrollView.OVER_SCROLL_NEV‌​ER);
                    LayoutManager = new AWidget.StaggeredGridLayoutManager(2, AWidget.StaggeredGridLayoutManager.Vertical);

                    mRecyclerView.SetLayoutManager(LayoutManager);
                    SetNativeControl(mRecyclerView);
                }

                SharedRecicler = (Shared.RecyclerView)Element;
                listener = new CustomScrollListener(SharedRecicler);

                mRecyclerView.AddOnScrollListener(listener);

                if (Element.ItemsSource != null && Element.ItemsSource is INotifyCollectionChanged)
                            ((INotifyCollectionChanged)Element.ItemsSource).CollectionChanged += ItemsSource_CollectionChanged;


                if (Element.ItemsSource != null)
                {
                    mAdapter = new ProductAdapter(Element, this.Context);
                    mAdapter.ItemClick += OnItemClick;
                    mRecyclerView.SetAdapter(mAdapter);
                }
                mRecyclerView.ScrollChange += MRecyclerView_ScrollChange;
            }
        }

        void OnItemClick(object sender, int position) => Element.InvokeItemTapped(position);

        void MRecyclerView_ScrollChange(object sender, ScrollChangeEventArgs e)
        {
            // Llego al final!
            if (!this.CanScrollVertically(1))
            {
                SharedRecicler.AtStartOfList = !(SharedRecicler.AtEndOfList = true);
                Element.InvokeIsEndEvent();
            }

            //// Llego al tope!
            //if (!this.CanScrollVertically(-1))
            //{
            //    SharedRecicler.AtEndOfList = !(SharedRecicler.AtStartOfList = true);
            //    //Element.VoidShouldShowSearchBar();
            //}
        }

        void ItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (mAdapter == null) return;

            //// NewItems contains the item that was added.
            //// If NewStartingIndex is not -1, then it contains the index where the new item was added.
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                mAdapter.NotifyItemInserted(e.NewStartingIndex);
            }

            // OldItems contains the item that was removed.
            // If OldStartingIndex is not -1, then it contains the index where the old item was removed.
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                mAdapter.NotifyItemRemoved(e.NewStartingIndex);
            }
        }
    }
}
