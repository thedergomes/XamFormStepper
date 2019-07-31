//using System;
//using Android.Content;
//using Android.Graphics;
//using Android.Graphics.Drawables;
//using Android.Support.V4.Content;
//using CustomTab.Droid.Renderer;
//using CustomTab.Helpers.UIComponents;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRenderer))]
//namespace CustomTab.Droid.Renderer
//{
//    public class RoundedEntryRenderer : EntryRenderer
//    {
//        RoundedEntry element;

//        public RoundedEntryRenderer(Context context) : base(context)
//        {
//        }

//        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
//        {
//            base.OnElementChanged(e);

//            if (e.OldElement == null)
//            {
//                element = (RoundedEntry)this.Element;

//                //Control.SetBackgroundResource(Resource.Layout.rounded_shape);
//                var gradientDrawable = new GradientDrawable();
//                gradientDrawable.SetCornerRadius(60f);
//                //gradientDrawable.SetStroke(5, Android.Graphics.Color.DeepPink);
//                gradientDrawable.SetColor(Android.Graphics.Color.White);
//                Control.SetBackground(gradientDrawable);

//                //Control.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.Image), null, null, null);
//                Control.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable("lupa"), null, null, null);
//                //Control.SetCompoundDrawablesWithIntrinsicBounds(GetDrawableFromVector("lupa"), null, null, null);
//                //Control.CompoundDrawablePadding = convertDpToPixel(10);

//                //Control.SetPadding(10, 
//                //Control.PaddingTop, 
//                //Control.PaddingRight,
//                //Control.PaddingBottom);

//                Control.SetPadding(30,
//                    (int)DpToPixels(5),
//                    (int)DpToPixels(15),
//                    (int)DpToPixels(5));

//                //Control.Gravity = Android.Views.GravityFlags.CenterHorizontal;

//                //Control.SetPadding(convertDpToPixel(10),
//                //convertDpToPixel(10),
//                //convertDpToPixel(10),
//                //convertDpToPixel(10));
//            }
//        }

//        public float DpToPixels(float valueInDp)
//        {
//            Android.Util.DisplayMetrics metrics = Context.Resources.DisplayMetrics;
//            return Android.Util.TypedValue.ApplyDimension(Android.Util.ComplexUnitType.Dip, valueInDp, metrics);
//        }

       
//        private Drawable GetDrawableFromVector(string imageEntryImage)
//        {
//            int resID = Context.Resources.GetIdentifier(
//                       imageEntryImage,
//                       "drawable",
//                       Context.PackageName);

//            Drawable drawable = ContextCompat.GetDrawable(this.Context, resID);
//            if (Android.OS.Build.VERSION.SdkInt < Android.OS.Build.VERSION_CODES.Lollipop)
//            {
//                drawable = (Android.Support.V4.Graphics.Drawable.DrawableCompat.Wrap(drawable)).Mutate();
//            }

//            Bitmap bitmap = Bitmap.CreateBitmap(
//                (int)(element.HeightRequest - 5),
//                (int)(element.HeightRequest - 5),
//                //convertDpToPixel(50),
//                //convertDpToPixel((float)(element.HeightRequest - 5)),
//                //convertDpToPixel((float)(element.HeightRequest - 5)),
//                //convertDpToPixel(50),
//                //convertDpToPixel(50),
//                //40 * 2,
//                //40 * 2, 
//                Bitmap.Config.Argb8888);
//            Canvas canvas = new Canvas(bitmap);
//            //drawable.SetBounds(0, 0, canvas.Width, canvas.Height);
//            drawable.SetBounds(0, 0, canvas.Width, canvas.Height);
//            drawable.Draw(canvas);

//            Drawable d = new BitmapDrawable(bitmap);
//            return d;
//        }

//        private BitmapDrawable GetDrawable(string imageEntryImage)
//        {
//            int resID = Context.Resources.GetIdentifier(
//                        imageEntryImage,
//                        "drawable",
//                        Context.PackageName);

//            var drawable = ContextCompat.GetDrawable(this.Context, resID);
//            var bitmap = ((BitmapDrawable)drawable).Bitmap;

//            //return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth * 2, element.ImageHeight * 2, true));
//            return new BitmapDrawable(Resources, 
//                Bitmap.CreateScaledBitmap(bitmap,
//                (int)DpToPixels((float)element.HeightRequest - 10),
//                (int)DpToPixels((float)element.HeightRequest - 10),
//                //40 * 2, 
//                //40 * 2, 
//                true));
//        }
//    }
//}
