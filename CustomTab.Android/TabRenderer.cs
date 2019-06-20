using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
//using Badoualy.StepperIndicatorLib;
using CustomTab;
using CustomTab.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using AView = Android.Views.View;

using Stepper = Badoualy.StepperIndicatorLib;

[assembly: ExportRenderer(typeof(TabbedPageCustom), typeof(TabRenderer))]
namespace CustomTab.Droid
{
    public class TabRenderer : TabbedPageRenderer
    {
        Stepper.StepperIndicator tabNew;

        AView layout;
        ViewPager viewpaper;

        public TabRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            var activity = this.Context.GetActivity();

            if (e.NewElement != null)
            {
                var tab = (TabLayout)ViewGroup.GetChildAt(1);
                viewpaper = (ViewPager)ViewGroup.GetChildAt(0);

                var container = (LinearLayout)activity.LayoutInflater.Inflate(Resource.Layout.Stepper, null);
                container.SetBackgroundColor(Android.Graphics.Color.ParseColor("#ffae38"));

                tabNew = container.FindViewById<Stepper.StepperIndicator>(Resource.Id.stepper);

                var parameter = (LinearLayout.LayoutParams)tabNew.LayoutParameters;
                parameter.SetMargins(DpToPixels(20), DpToPixels(30), DpToPixels(20), DpToPixels(30));

                tabNew.LayoutParameters = parameter;

                tabNew.SetViewPager(viewpaper);
                layout = container;

                AddView(layout);
            }

            if (e.NewElement != null)
            {

            }
        }
        public int DpToPixels(float valueInDp)
        {
            Android.Util.DisplayMetrics metrics = Context.Resources.DisplayMetrics;
            var pixels = Android.Util.TypedValue.ApplyDimension(Android.Util.ComplexUnitType.Dip, valueInDp, metrics);
            return Convert.ToInt32(Math.Round(pixels));
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            var width = r - l;
            var height = b - t;

            AView tabs = layout;

            tabs.Measure(MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
                         MeasureSpec.MakeMeasureSpec(height, MeasureSpecMode.AtMost));

            var tabsHeight = 0;
            if ((int)Build.VERSION.SdkInt >= 16)
                tabsHeight = Math.Min(height, Math.Max(tabs.MeasuredHeight, tabs.MinimumHeight));
            else
                tabsHeight = Math.Min(height, tabs.MeasuredHeight);


            if (tabs.Visibility != ViewStates.Gone)
            {
                base.OnLayout(changed, l, t, r, b);

                base.OnLayout
                    (changed,
                    l,
                    t + (int)tabsHeight - GetChildAt(1).MeasuredHeight,
                    r,
                    b
                    );

                viewpaper.Layout(
                    l,
                    t + (int)tabsHeight - GetChildAt(1).MeasuredHeight,
                    r,
                    b);
                tabs.Layout(0, 0, width, tabsHeight);
            }
        }
    }
}
