using Xamarin.Forms.Platform.Android;
using Alme.Droid.Renderers;
using Alme.Controls;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using Android.Graphics;

[assembly: ExportRenderer(typeof(AlmeButton), typeof(AlmeButtonRenderer))]
namespace Alme.Droid.Renderers
{
    public class AlmeButtonRenderer:ButtonRenderer
    {
        private AlmeButton almeButton;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                try
                {
                    almeButton = e.NewElement as AlmeButton;
                    GradientDrawable gradientDrawable = new GradientDrawable();
                    gradientDrawable.SetShape(ShapeType.Rectangle);
                    gradientDrawable.SetCornerRadius(almeButton.BorderRadius * 2);
                    if (almeButton.CenterColor != Xamarin.Forms.Color.Transparent)
                    {
                        gradientDrawable.SetColors(new int[] { almeButton.EndColor.ToAndroid(),
                almeButton.CenterColor.ToAndroid(), almeButton.StartColor.ToAndroid()});
                    }
                    else
                    {
                        gradientDrawable.SetColors(new int[] { almeButton.EndColor.ToAndroid(),
                        almeButton.StartColor.ToAndroid()});
                    }
                    gradientDrawable.SetGradientType(GradientType.LinearGradient);
                    gradientDrawable.SetOrientation(GradientDrawable.Orientation.RightLeft);
#pragma warning disable CS0618 // Type or member is obsolete
                    Control.SetBackgroundDrawable(gradientDrawable);
#pragma warning restore CS0618 // Type or member is obsolete
                    Control.SetTextColor(almeButton.TextColor.ToAndroid());
                    if (almeButton.FontFamily != null)
                    {
                        Control.Typeface = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, almeButton.FontFamily.Split('#')[0]);
                    }
                }
                catch { }
            }
        }
    }
}