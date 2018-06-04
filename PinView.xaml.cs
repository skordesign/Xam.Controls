﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Medial.Core.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PinView : ContentView
    {
        private const int GRID_WIDTH = 32;
        private const int NODE_WIDTH = 24;
        private List<Frame> frames;
        public static readonly BindableProperty FinishCommandProperty =
            BindableProperty.Create(nameof(FinishCommand), typeof(ICommand), typeof(PinView), null);

        public ICommand FinishCommand
        {
            get { return (ICommand)GetValue(FinishCommandProperty); }
            set { SetValue(FinishCommandProperty, value); }
        }
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value),
            typeof(string), typeof(PinView), string.Empty, BindingMode.OneWay, propertyChanged: OnPinChanged);
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly BindableProperty PinLengthProperty = BindableProperty.Create(nameof(PinLength),
           typeof(int), typeof(PinView), 6);
        public int PinLength
        {
            get { return (int)GetValue(PinLengthProperty); }
            set { SetValue(PinLengthProperty, value); }
        }
        public static readonly BindableProperty IsCheckingProperty = BindableProperty.Create(nameof(IsChecking),
           typeof(bool), typeof(PinView), false, BindingMode.OneWay, propertyChanged: OnIsCheckingChanged);

        private static void OnIsCheckingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            PinView pinView = bindable as PinView;
            if (newValue is bool isCheck)
            {
                pinView.ShowChecking(isCheck);
            }
        }

        public bool IsChecking
        {
            get { return (bool)GetValue(IsCheckingProperty); }
            set { SetValue(IsCheckingProperty, value); }
        }
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
           typeof(string), typeof(PinView), string.Empty, propertyChanged: OnTextChanged);

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            PinView pinView = bindable as PinView;
            if (newValue is string label)
            {
                pinView.CheckingLabel.Text = label;
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        private static void OnPinChanged(BindableObject bindable, object oldValue, object newValue)
        {
            PinView pinView = bindable as PinView;
            if (newValue != null)
            {
                int index = newValue.ToString().Length - 1;
                if (index >= 0)
                {
                    pinView.SetPinColor(index);
                }
                else
                {
                    pinView.Clear();
                }
            }
            pinView.DisableClearBackspace(newValue==null || string.IsNullOrEmpty(newValue.ToString()));
            pinView.ActivateNumberButtons(!(newValue != null && newValue.ToString().Length==pinView.PinLength));
        }
        public PinView()
        {
            InitializeComponent();
            CreatePinGrid();
            InitLabel();
        }
        public void DisableClearBackspace(bool isDisabled)
        {
            BackspaceBtn.IsEnabled = CleatBtn.IsEnabled = !isDisabled;
        }
        public void ShowChecking(bool isShow = true)
        {
            CheckingPanel.IsVisible = isShow;
        }
        public void SetPinColor(int index)
        {
            frames[index].BackgroundColor = Application.Current.Resources["MainColor"]!=null?(Color)Application.Current.Resources["MainColor"]:Color.Red;
        }
        private void InitLabel()
        {
            CheckingLabel.Text = Text;
        }
        private void CreatePinGrid()
        {
            frames = new List<Frame>();
            int pinLength = PinLength;
            for (int i = 0; i < pinLength; i++)
            {
                PinGrid.WidthRequest += GRID_WIDTH;
                PinGrid.ColumnDefinitions.Add(new ColumnDefinition());
                var node = new Frame
                {
                    WidthRequest = NODE_WIDTH,
                    HeightRequest = NODE_WIDTH,
                    BackgroundColor = Color.White,
                    CornerRadius = NODE_WIDTH / 2,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Padding = 0,
                    HasShadow = false,
                    BorderColor = (Color)Application.Current.Resources["MainColor"]
                };
                PinGrid.Children.Add(node, i, 0);
                frames.Add(node);
            }
        }
        private void SendKey1(object sender, System.EventArgs e)
        {
            SetPin(1);
        }

        private void SendKey2(object sender, System.EventArgs e)
        {
            SetPin(2);
        }

        private void SendKey3(object sender, System.EventArgs e)
        {
            SetPin(3);
        }

        private void SendKey4(object sender, System.EventArgs e)
        {
            SetPin(4);
        }

        private void SendKey5(object sender, System.EventArgs e)
        {
            SetPin(5);
        }

        private void SendKey6(object sender, System.EventArgs e)
        {
            SetPin(6);
        }

        private void SendKey7(object sender, System.EventArgs e)
        {
            SetPin(7);
        }

        private void SendKey8(object sender, System.EventArgs e)
        {
            SetPin(8);
        }

        private void SendKey9(object sender, System.EventArgs e)
        {
            SetPin(9);
        }

        private void SendKey0(object sender, System.EventArgs e)
        {
            SetPin(0);
        }
        private void SetPin(int val)
        {
            if (string.IsNullOrEmpty(Value))
            {
                Value = $"{val}";
            }
            else
            {
                Value = $"{Value}{val}";
                if (Value.Length == PinLength)
                {
                    Finish();
                }
            }

        }
        private void Clear()
        {
            foreach (Frame f in frames)
            {
                f.BackgroundColor = Color.White;
            }
            Value = string.Empty;
        }
        private void Finish()
        {
            if (FinishCommand != null && FinishCommand.CanExecute(null))
            {
                FinishCommand.Execute(null);
            }
        }
        public void ActivateNumberButtons(bool isEnabled)
        {
            Number0.IsEnabled = Number1.IsEnabled = Number2.IsEnabled
                = Number9.IsEnabled = Number3.IsEnabled = Number4.IsEnabled
                = Number5.IsEnabled = Number6.IsEnabled = Number7.IsEnabled
                = Number8.IsEnabled = isEnabled;
        }
        private void Clear(object sender, System.EventArgs e)
        {
            Clear();
        }

        private void Backspace(object sender, System.EventArgs e)
        {
            Value =  Value.Remove(Value.Length - 1);
            frames[Value.Length].BackgroundColor = Color.White;
        }
    }
}