using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace XamarinFormsSkiaSharpSamples.SamplePages
{
    public class TouchSample : SKCanvasView
    {
        private SKPoint _touchPoint;

        public TouchSample()
        {
            EnableTouchEvents = true;
        }

        protected override void OnTouch(SKTouchEventArgs e)
        {
            if (e.ActionType == SKTouchAction.Entered
                || e.ActionType == SKTouchAction.Pressed
                || e.ActionType == SKTouchAction.Moved)
            {
                _touchPoint = e.Location;
                InvalidateSurface();
            }
            e.Handled = true;
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            if (_touchPoint != null)
            {
                canvas.DrawCircle(_touchPoint, 10, new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = Color.Red.ToSKColor()
                });
            } 
        }
    }
}