using System;
using Xamarin.Forms;

namespace Alme.Controls
{
    public class AlmeButton:Button
    {
        public static readonly BindableProperty StartColorProperty = BindableProperty.Create(
        nameof(StartColor),
        typeof(Color),
        typeof(AlmeButton),
        Color.Accent);
        public Color StartColor
        {
            get { return (Color)GetValue(StartColorProperty); }
            set { SetValue(StartColorProperty, value); }
        }
        public static readonly BindableProperty EndColorProperty = BindableProperty.Create(
        nameof(EndColor),
        typeof(Color),
        typeof(AlmeButton),
        Color.Accent);
        public Color EndColor
        {
            get { return (Color)GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }
        public static readonly BindableProperty CenterColorProperty = BindableProperty.Create(
        nameof(CenterColor),
        typeof(Color),
        typeof(AlmeButton),
        Color.Transparent);
        public Color CenterColor
        {
            get { return (Color)GetValue(CenterColorProperty); }
            set { SetValue(CenterColorProperty, value); }
        }

        public static readonly BindableProperty AngleProperty = BindableProperty.Create(
        nameof(Angle),
        typeof(int),
        typeof(AlmeButton),
        0);
        public int Angle
        {
            get { return (int)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }
        public AlmeButton()
        {
            this.Clicked += this.AlmeButton_Clicked;
        }

        private void AlmeButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                Animation animateForClick = new Animation
                {
                    { 0, 1, new Animation(f => this.Scale = f, 1, 0.96, Easing.SinIn, null) }
                };
                Animation animateForClicked = new Animation
                {
                    { 0, 1, new Animation(f => this.Scale = f, 0.96, 1.02, Easing.SinOut, null) }
                };
                Animation animateForClickedReturn = new Animation
                {
                    { 0, 1, new Animation(f => this.Scale = f, 1.02, 1, Easing.SinIn, null) }
                };
                animateForClick.Commit(this, "BtnClick", 100, 100, 
                    finished: (x, y) => animateForClicked.Commit(this, "BtnClicked", 100, 100,
                    finished: (k,z)=> animateForClickedReturn.Commit(this,"Return",100,40)));

            }
            catch { }
        }
    }
}
