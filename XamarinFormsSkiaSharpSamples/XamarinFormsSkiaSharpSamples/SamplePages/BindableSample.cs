using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace XamarinFormsSkiaSharpSamples.SamplePages
{
    public class BindableSample : SKCanvasView
    {

        // The command
        public static readonly BindableProperty TouchCommandProperty =
            BindableProperty.Create(nameof(TouchCommand),
                typeof(ICommand),
                typeof(BindableSample),
                null);

        public ICommand TouchCommand
        {
            get => (ICommand)GetValue(TouchCommandProperty);
            set => SetValue(TouchCommandProperty, value);
        }

        public event EventHandler Touched;

        // Border color
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(propertyName: nameof(BorderColor),
                returnType:typeof(Color),
                declaringType:typeof(BindableSample),
                defaultValue: Color.Accent,
                validateValue: (_, value) => value != null,
                propertyChanged: InvalidateSurfaceOnChange);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        private static void InvalidateSurfaceOnChange(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (BindableSample)bindable;

            // Avoid unnecessary invalidation
            if (oldValue != newValue)
                control.InvalidateSurface();
        }

        public BindableSample()
        {
            EnableTouchEvents = true;
        }

        protected override void OnTouch(SKTouchEventArgs e)
        {
            var position = new SKRect(e.Location.X, e.Location.Y, e.Location.X, e.Location.Y);

            // Check if touch is a press action and is in the rectangle
            if (e.ActionType == SKTouchAction.Pressed
                && InputRect().IntersectsWith(position))
            {
                InvokeTouch();
            }
            e.Handled = true;
        }

        private void InvokeTouch()
        {
            Touched?.Invoke(this, EventArgs.Empty);
            TouchCommand?.Execute(null);
        }

        private SKRect InputRect()
        {
            return new SKRect(CanvasSize.Width / 2 - 200, 300, CanvasSize.Width / 2 + 200, 450);
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;

            var borderColor = new SKPaint
            {
                Color = BorderColor.ToSKColor(),
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 20
            };

            canvas.DrawRoundRect(InputRect(), 10, 10, borderColor);

            var xamarinInput = 20;
            var scale = CanvasSize.Width / Width;
            var scaledInput = xamarinInput * scale;
        }
    }

    public class BindableSampleViewModel : INotifyPropertyChanged
    {
        private Random rnd = new Random();

        public ICommand TouchedCommand => new Command(x =>
        {
            BorderColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            OnPropertyChanged(nameof(BorderColor));
        });

        public Color BorderColor { get; set; } = Color.Blue;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}