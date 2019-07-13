using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinFormsSkiaSharpSamples.SamplePages
{
    public class CoordinateSystemSample : SKCanvasView
    {
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var size = CanvasSize;
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            var pointsColor = new SKPaint
            {
                Color = Color.Blue.ToSKColor(),
                StrokeWidth = 5,
                Style = SKPaintStyle.Stroke,
                IsAntialias = true
            };

            // Top left
            canvas.DrawCircle(new SKPoint(0, 0), 10, pointsColor);
            // Top right
            canvas.DrawCircle(new SKPoint(size.Width, 0), 10, pointsColor);
            // Bottom left
            canvas.DrawCircle(new SKPoint(0, size.Height), 10, pointsColor);
            // Bottom right
            canvas.DrawCircle(new SKPoint(size.Width, size.Height), 10, pointsColor);

            var smallerSize = size.Width > size.Height ? size.Height : size.Width;
            var centeredRect = new SKRect(0, 0, smallerSize, smallerSize);
            centeredRect.Offset((size.Width - smallerSize) / 2, (size.Height - smallerSize) / 2);

            var rectColor = new SKPaint
            {
                Color = Color.Green.ToSKColor(),
                StrokeWidth = 5,
                Style = SKPaintStyle.Stroke,
                IsAntialias = true
            }; 

            canvas.DrawRect(centeredRect, rectColor);

            var arcPaint = new SKPaint
            {
                Color = Color.Red.ToSKColor(),
                StrokeWidth = 5,
                Style = SKPaintStyle.Stroke,
                IsAntialias = true
            };

            using (var path = new SKPath())
            {
                path.AddArc(centeredRect, 0, 45);
                canvas.DrawPath(path, arcPaint);
            }

            canvas.Save();
            canvas.RotateDegrees(-90, centeredRect.MidX, centeredRect.MidY);
            
            var rotatedArcPaint = new SKPaint
            {
                Color = Color.Orange.ToSKColor(),
                StrokeWidth = 5,
                Style = SKPaintStyle.Stroke,
                IsAntialias = true
            };

            using (var path = new SKPath())
            {
                path.AddArc(centeredRect, 0, 45);
                canvas.DrawPath(path, rotatedArcPaint);
            }

            canvas.Restore();
        }
    }
}
