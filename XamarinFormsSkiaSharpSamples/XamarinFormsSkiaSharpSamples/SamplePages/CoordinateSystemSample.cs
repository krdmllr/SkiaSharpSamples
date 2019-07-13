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
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            // Top left
            var radius = 20;
            canvas.DrawCircle(new SKPoint(0, 0), radius, pointsColor);
            // Top right
            canvas.DrawCircle(new SKPoint(size.Width, 0), radius, pointsColor);
            // Bottom left
            canvas.DrawCircle(new SKPoint(0, size.Height), radius, pointsColor);
            // Bottom right
            canvas.DrawCircle(new SKPoint(size.Width, size.Height), radius, pointsColor);

            var smallerSize = size.Width > size.Height ? size.Height : size.Width;
            var centeredRect = new SKRect(0, 0, smallerSize, smallerSize);
            centeredRect.Offset((size.Width - smallerSize) / 2, (size.Height - smallerSize) / 2);

            var rectColor = new SKPaint
            {
                Color = Color.Green.ToSKColor(), 
                Style = SKPaintStyle.Stroke,
                IsAntialias = true,
                StrokeWidth = 20
            }; 

            canvas.DrawRect(centeredRect, rectColor);

            var redArcPaint = new SKPaint
            {
                Color = Color.Red.ToSKColor(), 
                Style = SKPaintStyle.Stroke,
                IsAntialias = true,
                StrokeWidth = 20
            };

            using (var path = new SKPath())
            {
                path.AddArc(centeredRect, 0, 45);
                canvas.DrawPath(path, redArcPaint);
            }

            //canvas.Save(); 
            using (new SKAutoCanvasRestore(canvas))
            {
                canvas.RotateDegrees(-90, centeredRect.MidX, centeredRect.MidY);
                var orangeArcPaint = new SKPaint
                {
                    Color = Color.Orange.ToSKColor(),
                    StrokeWidth = 20,
                    Style = SKPaintStyle.Stroke,
                    IsAntialias = true
                };

                using (var path = new SKPath())
                {
                    path.AddArc(centeredRect, 0, 45);
                    canvas.DrawPath(path, orangeArcPaint);
                }
            }
            //canvas.Restore();
        }
    }
}
